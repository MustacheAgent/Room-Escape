namespace InteractObjects
{
    public interface IInteractable
    {
        public bool Enabled { get; set; }
        public void Interact();
    }
}