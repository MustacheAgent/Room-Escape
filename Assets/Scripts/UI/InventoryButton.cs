using EventBusSystem;
using Events.InventoryEvents;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class InventoryButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI buttonText;
        [SerializeField] private GameObject selectedPanel;
        
        public Item AssociatedItem { get; private set; }

        private bool _selected;

        public void Init(Item associatedItem)
        {
            AssociatedItem = associatedItem;
            buttonText.text = AssociatedItem.name;
            _selected = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            EventBus.Raise<IInventoryItem>(h => h.OnItemSelected(_selected ? null : AssociatedItem));
            _selected = !_selected;
        }

        public void SetSelected(bool isSelected)
        {
            selectedPanel.SetActive(isSelected);
        }
    }
}