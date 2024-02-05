using System.Collections.Generic;
using EventBusSystem;
using Events;
using UnityEngine;

namespace Managers
{
    public class InventoryManager : MonoBehaviour, IInventoryItem
    {
        [SerializeField] private List<Item> _items;

        private void Awake()
        {
            _items = new List<Item>();
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
            _items.Add(addedItem);
            Debug.Log("added item: " + addedItem.gameObject);
        }

        public void OnItemRemoved(Item removedItem)
        {
            _items.Remove(removedItem);
        }
    }
}