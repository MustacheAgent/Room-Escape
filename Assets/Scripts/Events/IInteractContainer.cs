using EventBusSystem;
using InteractObjects;

namespace Events
{
    public interface IInteractContainer : IGlobalSubscriber
    {
        void OnInteractContainer(InteractableContainer container);
    }
}