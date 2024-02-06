using System;
using UnityEngine;

namespace InteractObjects
{
    public abstract class InteractableObject : MonoBehaviour, IInteractable
    {
        public bool Enabled => Collider.enabled;

        protected Collider Collider;

        protected void Awake()
        {
            Collider = GetComponent<Collider>();
        }

        private void OnMouseDown()
        {
            if (enabled) Interact();
        }

        public abstract void Interact();

        public virtual void SetEnabled(bool isEnabled)
        {
            Collider.enabled = isEnabled;
        }
    }
}
