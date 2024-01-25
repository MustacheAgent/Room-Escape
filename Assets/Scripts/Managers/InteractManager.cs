using EventBusSystem;
using Events.CameraEvents;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class InteractManager : MonoBehaviour
    {
        public LayerMask layer;
        public CameraRig cameraRig;
    
        private PlayerInput _playerInput;
        private InputAction _interaction;
        private InputAction _return;
    
        private Camera _mainCamera;
    
        private void Awake()
        {
            _playerInput = new PlayerInput();
            _interaction = _playerInput.InteractControls.Select;
            _return = _playerInput.InteractControls.Return;

            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            _interaction.performed += OnSelect;
            _return.performed += OnReturn;
            _playerInput.InteractControls.Enable();
        }

        private void OnDisable()
        {
            _interaction.performed -= OnSelect;
            _return.performed -= OnReturn;
            _playerInput.InteractControls.Disable();
        }

        private void OnSelect(InputAction.CallbackContext input)
        {
            var ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out var hit, float.MaxValue, layer))
            {
                HandleInteractable(hit.transform.GetComponent<IInteractable>());
            }
        }

        private void OnReturn(InputAction.CallbackContext input)
        {
            EventBus.Raise<ICameraReset>(h => h.ResetRig());
        }

        private void HandleInteractable(IInteractable obj)
        {
            obj?.Interact();
        }
    }
}