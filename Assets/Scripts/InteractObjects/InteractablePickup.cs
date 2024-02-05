using EventBusSystem;
using Events;
using UnityEngine;

namespace InteractObjects
{
    [RequireComponent(typeof(Item))]
    public class InteractablePickup : MonoBehaviour, IInteractable
    {
        public bool Enabled => _collider.enabled;
        
        private Collider _collider;
        private Item _item;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _item = GetComponent<Item>();
        }
        
        public void Interact()
        {
            EventBus.Raise<IInventoryItem>(h => h.OnItemAdded(_item));
            Destroy(gameObject);
        }

        public void SetEnabled(bool isEnabled)
        {
            _collider.enabled = isEnabled;
        }
    }
}