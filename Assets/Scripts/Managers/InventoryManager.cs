using System;
using System.Collections.Generic;
using EventBusSystem;
using Events;
using Items;
using UnityEngine;

namespace Managers
{
    public class InventoryManager : MonoBehaviour, IInventoryItem
    {
        //[SerializeField] private Inventory inventory;
        [SerializeField] private List<Item> items;

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
            Debug.Log("added item: " + addedItem);
        }

        public void OnItemRemoved(Item removedItem)
        {
            items.Remove(removedItem);
        }
    }
}