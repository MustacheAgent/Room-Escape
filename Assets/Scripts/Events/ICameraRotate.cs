using EventBusSystem;

namespace Events
{
    public interface ICameraRotate : IGlobalSubscriber
    {
        public void HandleRotation(float currentAngle, float angleStep);
    }
}