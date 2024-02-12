using EventBusSystem;
using Events;
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

        public void Init(Item associatedItem)
        {
            AssociatedItem = associatedItem;
            buttonText.text = AssociatedItem.name;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            EventBus.Raise<IInventoryItem>(h => h.OnItemSelected(selectedPanel.activeInHierarchy ? null : AssociatedItem));
            SetSelected(!selectedPanel.activeInHierarchy);
        }

        public void SetSelected(bool isSelected)
        {
            selectedPanel.SetActive(isSelected);
        }
    }
}