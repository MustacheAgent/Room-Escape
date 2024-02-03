using UnityEngine;

namespace InteractObjects
{
    public class InteractableObject : MonoBehaviour, IInteractable
    {
        public bool Enabled => _collider.enabled;

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public void Interact()
        {
            //EventBus.Raise<ICameraLookAt>(h => h.LookAt(this));
        }

        public void SetEnabled(bool active)
        {
            _collider.enabled = active;
        }
    }
}
