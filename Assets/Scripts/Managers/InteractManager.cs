using System.Collections.Generic;
using EventBusSystem;
using Events.CameraEvents;
using InteractObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class InteractManager : MonoBehaviour
    {
        public LayerMask layer;

        private PlayerInput _playerInput;
        private InputAction _interaction;
        private InputAction _return;
    
        private Camera _mainCamera;

        private Stack<InteractableContainer> _containers;
        private InteractableContainer _currentContainer;
    
        private void Awake()
        {
            _playerInput = new PlayerInput();
            _interaction = _playerInput.InteractControls.Select;
            _return = _playerInput.InteractControls.Return;

            _mainCamera = Camera.main;

            _containers = new Stack<InteractableContainer>();
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
            
            if (!Physics.Raycast(ray, out var hit, float.MaxValue, layer))
            {
                Debug.Log("No hit");
                return;
            }
            
            var obj = hit.transform.GetComponent<IInteractable>();
            if (obj != null && obj.Enabled)
            {
                var container = hit.transform.GetComponent<InteractableContainer>();
                if (container)
                {
                    _currentContainer = container;
                    _containers.Push(_currentContainer);
                }
                obj.Interact();
            }
        }

        private void OnReturn(InputAction.CallbackContext input)
        {
            if (_containers.Count > 1)
            {
                var obj = _containers.Pop();
                if (obj != null)
                {
                    _currentContainer.SetChildrenActive(false);
                    _currentContainer.SetEnabled(true);
                    _currentContainer = obj;
                    EventBus.Raise<ICameraLookAt>(h => h.LookAt(obj));
                }
            }
            else
            {
                _currentContainer.SetChildrenActive(false);
                _currentContainer.SetEnabled(true);
                _currentContainer = null;
                _containers.Clear();
                EventBus.Raise<ICameraReset>(h => h.ResetRig());
            }
        }
    }
}