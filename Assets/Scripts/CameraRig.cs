using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRig : MonoBehaviour
{
    [Header("Zoom")] 
    public float stickMinZoomHeight = -250f;
    public float stickMaxZoomHeight = -45f;
    public float swivelMinZoomAngle = 90f;
    public float swivelMaxZoomAngle = 45f;
    public float zoomSmoothingSpeed = 10f;
    
    private float _rawZoomInput, _smoothZoomInput;
    private float _currentZoom = 1f;

    [Header("Movement")] 
    public float moveSpeedMinZoom = 100f;
    public float moveSpeedMaxZoom = 25f;
    public float movementSmoothingSpeed = 10f;
    
    private Vector3 _rawMovementInput, _smoothMovementInput;
        
    [Header("Rotation")]
    public float rotationSpeed = 100f;
    public float rotationSmoothingSpeed = 10f;
    private float _rawRotationInput, _smoothRotationInput;
    private float _rotationAngle;
    
    // unput handling
    private Transform _swivel, _stick, _camera;
    private PlayerInput _playerIput;
    private InputAction _movement;
    private InputAction _rotation;
    private InputAction _zoom;

    private void Awake()
    {
        _swivel = transform.GetChild(0);
        _stick = _swivel.GetChild(0);
        _camera = _stick.GetChild(0);

        _playerIput = new PlayerInput();
    }

    private void OnEnable()
    {
        _movement = _playerIput.CameraControls.Movement;
        _rotation = _playerIput.CameraControls.Rotate;
        _zoom = _playerIput.CameraControls.Zoom;
        _playerIput.CameraControls.Enable();
    }

    private void OnDisable()
    {
        _playerIput.CameraControls.Disable();
    }

    private void Update()
    {
        GetInput();
        
        AdjustPosition(_smoothMovementInput);
        AdjustRotation(_smoothRotationInput);
        AdjustZoom(_rawZoomInput);
    }

    private void GetInput()
    {
        // movement input
        var move = _movement.ReadValue<Vector2>();
        _rawMovementInput = new Vector3(move.x, 0, move.y);
        _smoothMovementInput = Vector3.Lerp(_smoothMovementInput, _rawMovementInput, Time.deltaTime * movementSmoothingSpeed);
        
        // rotate input
        _rawRotationInput = _rotation.ReadValue<float>();
        _smoothRotationInput = Mathf.Lerp(_smoothRotationInput, _rawRotationInput, Time.deltaTime * rotationSmoothingSpeed);

        // zoom input
        _rawZoomInput = _zoom.ReadValue<Vector2>().y;
        Debug.Log("raw zoom: " + _rawZoomInput);
        _rawZoomInput = Mathf.Clamp(_rawZoomInput, -1, 1);
        Debug.Log("clamped zoom: " + _rawZoomInput);
        _smoothZoomInput = Mathf.Lerp(_smoothZoomInput, _rawZoomInput, Time.deltaTime * zoomSmoothingSpeed);
    }

    private void AdjustZoom(float delta)
    {
        _currentZoom = Mathf.Clamp01(_currentZoom + delta);
            
        var distance = Mathf.Lerp(stickMinZoomHeight, stickMaxZoomHeight, _currentZoom);
        _stick.localPosition = new Vector3(0f, 0f, distance);
            
        var angle = Mathf.Lerp(swivelMinZoomAngle, swivelMaxZoomAngle, _currentZoom);
        _swivel.localRotation = Quaternion.Euler(angle, 0f, 0f);
    }

    private void AdjustPosition(Vector3 movementInput)
    {
        float xDelta = movementInput.x, zDelta = movementInput.z;
        
        var damping = Mathf.Max(Mathf.Abs(xDelta), Mathf.Abs(zDelta));
        var distance = Mathf.Lerp(moveSpeedMinZoom, moveSpeedMaxZoom, _currentZoom) * Time.deltaTime * damping;
        var direction = transform.localRotation * new Vector3(xDelta, 0f, zDelta).normalized;
            
        var position = transform.localPosition;
        position += direction * distance;
        transform.localPosition = position;
    }
    
    private void AdjustRotation(float delta) 
    {
        _rotationAngle += delta * rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0f, _rotationAngle, 0f);
    }
    
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