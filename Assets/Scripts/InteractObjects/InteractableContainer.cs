using Events;
using Events.CameraEvents;
using UnityEngine;
using EventBus = EventBusSystem.EventBus;

namespace InteractObjects
{
    public class InteractableContainer : InteractableObject
    {
        [field:SerializeField] public float xAngle { get; private set; }
        [field:SerializeField] public float distance { get; private set; }

        private IInteractable[] _childrenObjects;

        private void Start()
        {
            _childrenObjects = GetComponentsInChildren<IInteractable>();
        }

        public override void Interact()
        {
            SetChildrenActive(true);
            SetEnabled(false);
            EventBus.Raise<ICameraLookAt>(h=> h.LookAt(this));
            EventBus.Raise<IInteractContainer>(h=> h.OnInteractContainer(this));
        }

        public void SetChildrenActive(bool isActive)
        {
            foreach (var child in _childrenObjects)
            {
                child.SetEnabled(isActive);
            }
        }
    }
}