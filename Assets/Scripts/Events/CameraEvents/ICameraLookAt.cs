using EventBusSystem;
using InteractObjects;

namespace Events.CameraEvents
{
    public interface ICameraLookAt : IGlobalSubscriber
    {
        public void LookAt(InteractableObject obj);
    }
}