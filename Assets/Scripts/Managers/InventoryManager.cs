using System.Collections.Generic;
using EventBusSystem;
using Events.InventoryEvents;
using Items;
using UnityEngine;

namespace Managers
{
    public class InventoryManager : MonoBehaviour, IInventoryItem, IService
    {
        //[SerializeField] private Inventory inventory;
        [SerializeField] private List<Item> items;
        
        public Item SelectedItem { get; private set; }

        private void Start()
        {
            items = new List<Item>();
        }

        private void OnEnable()
        {
            EventBus.Subscribe(this);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(this);
        }

        public void OnItemAdded(Item addedItem)
        {
            if (addedItem != null) items.Add(addedItem);
        }

        public void OnItemRemoved(Item removedItem)
        {
            items.Remove(removedItem);
        }

        public void OnItemSelected(Item selectedItem)
        {
            SelectedItem = selectedItem;
        }
    }
}