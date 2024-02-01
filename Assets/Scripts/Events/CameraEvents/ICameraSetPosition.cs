using EventBusSystem;
using UnityEngine;

namespace Events.CameraEvents
{
    public interface ICameraLoopPosition : IGlobalSubscriber
    {
        public void LoopPosition(Transform newPosition);
    }
}