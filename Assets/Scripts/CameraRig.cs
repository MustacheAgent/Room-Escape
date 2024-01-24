using System;
using DG.Tweening;
using EventBusSystem;
using Events;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRig : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] public float angle = 90f;
    private bool _canRotate = true;
    
    private float _rotationAngle;
    
    private Transform _swivel; 
    private float _swivelDefaultXAngle;
    
    private Transform _stick;
    private float _stickDefaultZPos;
    
    private Vector3 _rigDefaultPosition;
    
    private PlayerInput _playerInput;

    private void Awake()
    {
        _swivel = transform.GetChild(0);
        _stick = _swivel.GetChild(0);
        
        _rotationAngle = transform.rotation.eulerAngles.y;
        _rigDefaultPosition = transform.position;

        _swivelDefaultXAngle = _swivel.rotation.eulerAngles.x;
        _stickDefaultZPos = _stick.localPosition.z;

        _playerInput = new PlayerInput();
    }

    #region Enable/Disable

    private void OnEnable()
    {
        _playerInput.CameraControls.Rotate.performed += OnRotate;
        _playerInput.CameraControls.Enable();
    }

    private void OnDisable()
    {
        _playerInput.CameraControls.Rotate.performed -= OnRotate;
        _playerInput.CameraControls.Disable();
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
        EventBus.Raise<ICameraRotate>(h => h.HandleRotation(_rotationAngle, angle));
        
        transform.DORotate(new Vector3(0, _rotationAngle, 0), 1);
    }

    /// <summary>
    /// Look at clicked interactable object.
    /// </summary>
    /// <param name="obj">Interactable object.</param>
    public void LookAt(Interactable obj)
    {
        // move rig root to object position
        transform.DOMove(obj.transform.position, 1);
        
        // rotate swivel x axis to look approximately on object
        _swivel.DOLocalRotate(new Vector3(obj.xAngle, 0, 0), 1);
        
        // move stick z axis to zoom in on object
        _stick.DOLocalMove(new Vector3(0, 0, -obj.distance), 1);
        
        // disable rig rotation
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
}