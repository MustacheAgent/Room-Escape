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

        public bool Enabled => _collider.enabled;

        [SerializeField] private bool loop;
        [SerializeField] private Transform loopPosition;
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        public void Interact()
        {
            if (loop)
            {
                EventBus.Raise<ICameraLoopPosition>(h => h.LoopPosition(loopPosition));
            }
            EventBus.Raise<IRoomChanged>(h => h.OnRoomChanged(roomToMove));
        }

        public void SetActive(bool active)
        {
            _collider.enabled = active;
        }
    }
}