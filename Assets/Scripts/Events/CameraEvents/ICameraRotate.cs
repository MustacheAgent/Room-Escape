using EventBusSystem;

namespace Events.CameraEvents
{
    public interface ICameraRotate : IGlobalSubscriber
    {
        public void OnCameraRotation(float currentAngle, float angleStep);
    }
}