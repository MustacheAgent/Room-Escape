using EventBusSystem;

namespace Events.CameraEvents
{
    public interface ICameraReset : IGlobalSubscriber
    {
        public void ResetRig();
    }
}