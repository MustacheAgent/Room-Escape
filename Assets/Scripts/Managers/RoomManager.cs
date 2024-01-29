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
    
        public Room _currentRoom;

        public float _currentAngle, _angleStep;

        private void OnEnable()
        {
            EventBus.Subscribe(this);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(this);
        }

        private void Awake()
        {
            _currentRoom = firstRoom;
        }

        private void Start()
        {
            EventBus.Raise<IRoomChanged>(h => h.OnRoomChanged(firstRoom));
        }

        public void OnRoomChanged(Room newRoom)
        {
            _currentRoom.gameObject.SetActive(false);
            _currentRoom = newRoom;
            _currentRoom.gameObject.SetActive(true);
            _currentRoom.DissolveSectors(_currentAngle, _angleStep);
        }

        public void OnCameraRotation(float currentAngle, float angleStep)
        {
            _currentAngle = currentAngle;
            _angleStep = angleStep;
            _currentRoom.DissolveSectors(_currentAngle, _angleStep);
        }
    }
}