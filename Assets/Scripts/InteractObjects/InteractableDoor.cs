using EventBusSystem;
using Events;
using UnityEngine;

namespace Rooms
{
    public class InteractableDoor : MonoBehaviour, IInteractable
    {
        [SerializeField] private Room roomToMove;
        
        public void Interact()
        {
            EventBus.Raise<IRoomChanged>(h => h.OnRoomChanged(roomToMove));
        }
    }
}