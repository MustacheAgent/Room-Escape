using System;
using System.Collections.Generic;
using EventBusSystem;
using Events;
using Items;
using UnityEngine;

namespace UI
{
    public class InventoryPanel : MonoBehaviour, IInventoryItem
    {
        [SerializeField] private InventoryButton buttonPrefab;
        private List<InventoryButton> _buttons;

        private InventoryButton _selectedButton;

        private void Start()
        {
            _buttons = new List<InventoryButton>();
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
            var itemButton = Instantiate(buttonPrefab, transform);
            itemButton.Init(addedItem);
            _buttons.Add(itemButton);
        }

        public void OnItemRemoved(Item removedItem)
        {
            var button = _buttons.Find(t => t.AssociatedItem == removedItem);
            _buttons.Remove(button);
            Destroy(button);
        }

        public void OnItemSelected(Item selectedItem)
        {
            foreach (var button in _buttons)
            {
                button.SetSelected(false);
            }
        }
    }
}
