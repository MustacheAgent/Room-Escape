using EventBusSystem;
using Events;
using Items;
using UnityEngine;

namespace InteractObjects
{
    public class InteractablePickup : InteractableObject
    {
        [SerializeField] private Item item;
        
        public override void Interact()
        {
            EventBus.Raise<IInventoryItem>(h => h.OnItemAdded(item));
            Destroy(gameObject);
        }
    }
}