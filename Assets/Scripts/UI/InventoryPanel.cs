using System;
using System.Collections.Generic;
using Events;
using Items;
using UnityEngine;

namespace UI
{
    public class InventoryPanel : MonoBehaviour, IInventoryItem
    {
        [SerializeField] private InventoryButton buttonPrefab;
        private List<InventoryButton> _buttons;

        private void Start()
        {
            _buttons = new List<InventoryButton>();
        }

        public void OnItemAdded(Item addedItem)
        {
            _buttons.Add(Instantiate(buttonPrefab, transform));
        }

        public void OnItemRemoved(Item removedItem)
        {
            var button = _buttons.Find(t => t.AssociatedItem == removedItem);
            _buttons.Remove(button);
            Destroy(button);
        }
    }
}
