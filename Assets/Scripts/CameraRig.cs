using DG.Tweening;
using EventBusSystem;
using Events.CameraEvents;
using Events.RoomEvents;
using InteractObjects;
using Rooms;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRig : MonoBehaviour, ICameraReset, IRoomChanged, ICameraLookAt, ICameraLoopPosition
{
    [Header("Rotation")]
    [SerializeField] private float angle = 90f;
    
    private bool _canRotate = true;
    private float _rotationAngle;
    
    private Transform _swivel;
    private float _swivelDefaultXAngle;
    
    private Transform _stick;
    private float _stickDefaultZPos;

    private Transform _transform;
    private Vector3 _rigDefaultPosition;

    private float _orthographicSizeDefault;

    private Camera _cam; 
    
    private PlayerInput _playerInput;

    private void Awake()
    {
        _transform = transform;
        _swivel = _transform.GetChild(0);
        _stick = _swivel.GetChild(0);
        
        _rotationAngle = _transform.rotation.eulerAngles.y;
        _rigDefaultPosition = _transform.position;

        _swivelDefaultXAngle = _swivel.rotation.eulerAngles.x;
        _stickDefaultZPos = _stick.localPosition.z;

        _cam = Camera.main;

        if (_cam.orthographic)
        {
            _orthographicSizeDefault = _cam.orthographicSize;
        }

        _playerInput = new PlayerInput();
    }

    private void Start()
    {
        EventBus.Raise<ICameraRotate>(h => h.OnCameraRotation(_rotationAngle, angle));
    }

    #region Enable/Disable

    private void OnEnable()
    {
        _playerInput.CameraControls.Rotate.performed += OnRotate;
        _playerInput.CameraControls.Enable();
        
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        _playerInput.CameraControls.Rotate.performed -= OnRotate;
        _playerInput.CameraControls.Disable();

        EventBus.Unsubscribe(this);
    }

    #endregion

    #region Main control

    private void OnRotate(InputAction.CallbackContext input)
    {
        var delta = input.ReadValue<float>();
        HandleRotation(delta);
    }

    private void HandleRotation(float delta)
    {
        if (!_canRotate) return;
        _rotationAngle += delta * angle;
        _rotationAngle = Mathf.Repeat(_rotationAngle, 360);
        EventBus.Raise<ICameraRotate>(h => h.OnCameraRotation(_rotationAngle, angle));
        
        _transform.DORotate(new Vector3(0, _rotationAngle, 0), 1f);
    }

    /// <summary>
    /// Look at clicked interactable container.
    /// </summary>
    /// <param name="obj">Interactable container.</param>
    public void LookAt(InteractableContainer obj)
    {
        // move rig root to object position
        _transform.DOMove(obj.transform.position, 1);

        // rotate rig root opposite of target rotation
        var deltaAngle = Mathf.DeltaAngle(_transform.eulerAngles.y, obj.transform.eulerAngles.y);
        var y = Mathf.Repeat(obj.transform.eulerAngles.y + 180 + obj.yAngle * Mathf.Sign(deltaAngle), 360);
        _transform.DORotate(new Vector3(0f, y, 0f), 1f);
        
        // rotate swivel x axis to look approximately on object
        _swivel.DOLocalRotate(new Vector3(obj.xAngle, 0, 0), 1);

        if (_cam.orthographic)
        {
            _cam.DOOrthoSize(obj.distance, 1);
        }
        else
        {
            // move stick z axis to zoom in on object
            _stick.DOLocalMove(new Vector3(0, 0, -obj.distance), 1);
        }
        
        // disable rig rotation
        _canRotate = false;
    }

    /// <summary>
    /// Reset camera to default position.
    /// </summary>
    public void ResetRig()
    {
        _transform.DOMove(_rigDefaultPosition, 1);
        _transform.DORotate(new Vector3(0f, _rotationAngle, 0f), 1f);
        _swivel.DOLocalRotate(new Vector3(_swivelDefaultXAngle, 0, 0), 1);

        if (_cam.orthographic)
        {
            _cam.DOOrthoSize(_orthographicSizeDefault, 1);
        }
        else
        {
            _stick.DOLocalMove(new Vector3(0, 0, _stickDefaultZPos), 1);
        }
        
        _canRotate = true;
    }

    public void OnRoomChanged(Room newRoom)
    {
        _rigDefaultPosition = newRoom.ViewPoint.position;
        ResetRig();
    }

    public void LoopPosition(Transform newPosition)
    {
        _transform.position = newPosition.position;
        var diff = _transform.eulerAngles.y < 0 ? angle : -angle;
        var eylerAngles = _transform.rotation.eulerAngles;
        eylerAngles.y += diff;
        _rotationAngle = eylerAngles.y;
        _transform.eulerAngles = eylerAngles;
        
        EventBus.Raise<ICameraRotate>(h => h.OnCameraRotation(_rotationAngle, angle));
    }

    #endregion
}