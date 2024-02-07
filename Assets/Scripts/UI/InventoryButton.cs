using Items;
using UnityEngine;

namespace UI
{
    public class InventoryButton : MonoBehaviour
    {
        public Item AssociatedItem { get; private set; }

        public void Init(Item associatedItem)
        {
            AssociatedItem = associatedItem;
        }
    }
}