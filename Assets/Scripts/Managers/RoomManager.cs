using EventBusSystem;
using Events;
using Events.CameraEvents;
using Rooms;
using UnityEngine;

namespace Managers
{
    public class RoomManager : MonoBehaviour, IRoomChanged, ICameraRotate
    {
        [SerializeField] private Room firstRoom;
    
        private Room _currentRoom;

        private void OnEnable()
        {
            EventBus.Subscribe(this);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(this);
        }

        private void Start()
        {
            _currentRoom = firstRoom;
            EventBus.Raise<IRoomChanged>(h => h.OnRoomChanged(firstRoom));
        }

        public void OnRoomChanged(Room newRoom)
        {
            _currentRoom.gameObject.SetActive(false);
            _currentRoom = newRoom;
            _currentRoom.gameObject.SetActive(true);
        }

        public void OnCameraRotation(float currentAngle, float angleStep)
        {
            _currentRoom.DissolveSectors(currentAngle, angleStep);
        }
    }
}