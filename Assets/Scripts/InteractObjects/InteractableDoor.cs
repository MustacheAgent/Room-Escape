using EventBusSystem;
using Events.CameraEvents;
using Events.RoomEvents;
using Rooms;
using UnityEngine;

namespace InteractObjects
{
    public class InteractableDoor : MonoBehaviour, IInteractable
    {
        [SerializeField] private Room roomToMove;

        public bool Enabled { get; set; } = true;

        [SerializeField] private bool loop;
        [SerializeField] private Transform loopPosition;

        public void Interact()
        {
            if (loop)
            {
                EventBus.Raise<ICameraLoopPosition>(h => h.LoopPosition(loopPosition));
            }
            EventBus.Raise<IRoomChanged>(h => h.OnRoomChanged(roomToMove));
        }
    }
}