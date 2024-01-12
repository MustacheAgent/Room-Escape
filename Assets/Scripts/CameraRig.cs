using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRig : MonoBehaviour
{
    [Header("Rotation")]
    public float angle = 90f;
    private bool _canRotate = true;
    
    private float _rawRotationInput, _smoothRotationInput;
    private float _rotationAngle;
    
    private Transform _swivel; 
    private float _swivelDefaultXAngle;
    
    private Transform _stick;
    private float _stickDefaultZPos;
    
    private PlayerInput _playerIput;

    private Vector3 _rigDefaultPosition;

    private void Awake()
    {
        _swivel = transform.GetChild(0);
        _stick = _swivel.GetChild(0);
        
        _rotationAngle = transform.rotation.eulerAngles.y;
        _rigDefaultPosition = transform.position;

        _swivelDefaultXAngle = _swivel.rotation.eulerAngles.x;
        _stickDefaultZPos = _stick.localPosition.z;

        _playerIput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerIput.CameraControls.Rotate.performed += OnRotate;
        _playerIput.CameraControls.Enable();
    }

    private void OnDisable()
    {
        _playerIput.CameraControls.Rotate.performed -= OnRotate;
        _playerIput.CameraControls.Disable();
    }

    #region main control

    private void OnRotate(InputAction.CallbackContext input)
    {
        var delta = input.ReadValue<float>();
        HandleRotation(delta);
    }

    private void HandleRotation(float delta)
    {
        if (!_canRotate) return;
        _rotationAngle += delta * angle;
        transform.DORotate(new Vector3(0, _rotationAngle, 0), 1);
    }

    /// <summary>
    /// Look at clicked interactable object.
    /// </summary>
    /// <param name="obj">Interactable object.</param>
    public void LookAt(Interactable obj)
    {
        transform.DOMove(obj.transform.position, 1);
        _swivel.DOLocalRotate(new Vector3(obj.xAngle, 0, 0), 1);
        _stick.DOLocalMove(new Vector3(0, 0, -obj.distance), 1);
        
        _canRotate = false;
    }

    /// <summary>
    /// Reset camera to default position.
    /// </summary>
    public void ResetRig()
    {
        transform.DOMove(_rigDefaultPosition, 1);
        _swivel.DOLocalRotate(new Vector3(_swivelDefaultXAngle, 0, 0), 1);
        _stick.DOLocalMove(new Vector3(0, 0, _stickDefaultZPos), 1);
        _canRotate = true;
    }

    #endregion

    private Vector3 CameraDirection(Vector3 movementDirection)
    {
        var cameraForward = transform.forward;
        var cameraRight = transform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        var direction = cameraForward * movementDirection.z + cameraRight * movementDirection.x;
        direction.y = movementDirection.y;

        return direction;
    }
}