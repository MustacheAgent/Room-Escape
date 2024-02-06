using EventBusSystem;
using Events.CameraEvents;
using Events.RoomEvents;
using Rooms;
using UnityEngine;

namespace InteractObjects
{
    public class InteractableMoveToRoom : InteractableObject
    {
        [SerializeField] private Room roomToMove;

        [SerializeField] private bool loop;
        [SerializeField] private Transform loopPosition;

        public override void Interact()
        {
            if (loop)
            {
                EventBus.Raise<ICameraLoopPosition>(h => h.LoopPosition(loopPosition));
            }
            EventBus.Raise<IRoomChanged>(h => h.OnRoomChanged(roomToMove));
        }
    }
}