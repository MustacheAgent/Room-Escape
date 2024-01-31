using EventBusSystem;
using Events;
using Rooms;
using UnityEngine;

namespace InteractObjects
{
    public class InteractableDoor : MonoBehaviour, IInteractable
    {
        [SerializeField] private Room roomToMove;

        public bool Enabled { get; set; } = true;

        public void Interact()
        {
            EventBus.Raise<IRoomChanged>(h => h.OnRoomChanged(roomToMove));
        }
    }
}