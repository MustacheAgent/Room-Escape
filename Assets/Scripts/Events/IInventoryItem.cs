using EventBusSystem;
using Items;

namespace Events
{
    public interface IInventoryItem : IGlobalSubscriber
    {
        public void OnItemAdded(Item addedItem);
        public void OnItemRemoved(Item removedItem);
        public void OnItemSelected(Item selectedItem);
    }
}