using Items;

namespace InteractObjects
{
    public interface IInteractable
    {
        public bool Enabled { get; }

        public void Interact();
        public void SetEnabled(bool isEnabled);
    }

    public interface IUsable
    {
        public void UseItem(Item item);

        public bool DestroyItem { get; }
    }
}