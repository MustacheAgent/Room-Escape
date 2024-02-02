using EventBusSystem;
using Events.CameraEvents;
using UnityEngine;

namespace InteractObjects
{
    public class InteractableObject : MonoBehaviour, IInteractable
    {
        public float xAngle;
        public float distance;

        public bool Enabled => _collider.enabled;

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public void Interact()
        {
            EventBus.Raise<ICameraLookAt>(h => h.LookAt(this));
        }

        public void SetActive(bool active)
        {
            _collider.enabled = active;
        }
    }
}
