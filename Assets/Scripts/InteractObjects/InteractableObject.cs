using EventBusSystem;
using Events.CameraEvents;
using UnityEngine;

namespace InteractObjects
{
    public class InteractableObject : MonoBehaviour, IInteractable
    {
        public float xAngle;
        public float distance;
        
        public void Interact()
        {
            EventBus.Raise<ICameraLookAt>(h => h.LookAt(this));
        }
    }
}
