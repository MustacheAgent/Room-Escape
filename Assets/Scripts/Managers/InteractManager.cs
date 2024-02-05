using System.Collections.Generic;
using EventBusSystem;
using Events;
using Events.CameraEvents;
using InteractObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class InteractManager : MonoBehaviour, IInteractContainer
    {
        public LayerMask layer;

        private PlayerInput _playerInput;
        private InputAction _return;
    
        private Camera _mainCamera;

        private Stack<InteractableContainer> _containers;
        private InteractableContainer _currentContainer;
    
        private void Awake()
        {
            _playerInput = new PlayerInput();
            _return = _playerInput.InteractControls.Return;

            _mainCamera = Camera.main;

            _containers = new Stack<InteractableContainer>();
        }

        private void OnEnable()
        {
            _return.performed += OnReturn;
            _playerInput.InteractControls.Enable();
            
            EventBus.Subscribe(this);
        }

        private void OnDisable()
        {
            _return.performed -= OnReturn;
            _playerInput.InteractControls.Disable();
            
            EventBus.Unsubscribe(this);
        }

        public void OnInteractContainer(InteractableContainer container)
        {
            if (_currentContainer != null)
            {
                _containers.Push(_currentContainer);
            }
            
            _currentContainer = container;
        }

        private void OnReturn(InputAction.CallbackContext input)
        {
            _currentContainer.SetChildrenActive(false);
            _currentContainer.SetEnabled(true);
            if (_containers.Count > 0)
            {
                var obj = _containers.Pop();
                if (obj != null)
                {
                    _currentContainer = obj;
                    EventBus.Raise<ICameraLookAt>(h => h.LookAt(_currentContainer));
                }
            }
            else
            {
                _currentContainer = null;
                EventBus.Raise<ICameraReset>(h => h.ResetRig());
            }
        }
    }
}