using Events.CameraEvents;
using UnityEngine;
using EventBus = EventBusSystem.EventBus;

namespace InteractObjects
{
    public class InteractableContainer : MonoBehaviour, IInteractable
    {
        [field:SerializeField] public float Angle { get; private set; }
        [field:SerializeField] public float Distance { get; private set; }
        
        public bool Enabled => _collider.enabled;
        
        private Collider _collider;

        private IInteractable[] _childrenObjects;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void Start()
        {
            _childrenObjects = GetComponentsInChildren<IInteractable>();
        }

        public void Interact()
        {
            SetChildrenActive(true);
            SetEnabled(false);
            EventBus.Raise<ICameraLookAt>(h=> h.LookAt(this));
        }

        public void SetChildrenActive(bool isActive)
        {
            foreach (var child in _childrenObjects)
            {
                child.SetEnabled(isActive);
            }
        }

        public void SetEnabled(bool isEnabled)
        {
            _collider.enabled = isEnabled;
        }
    }
}