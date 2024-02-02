using UnityEngine;

namespace InteractObjects
{
    public interface IInteractable
    {
        public bool Enabled { get; }

        public void Interact();
        public void SetActive(bool active);
    }
}