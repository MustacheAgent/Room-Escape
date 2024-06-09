using EventBusSystem;
using Events.CameraEvents;
using Events.RoomEvents;
using Rooms;
using UnityEngine;

namespace Managers
{
    public class RoomManager : MonoBehaviour, IService, IRoomChanged, ICameraRotate
    {
        [SerializeField] private Room firstRoom;
    
        private Room _currentRoom;

        private float _currentAngle, _angleStep;

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
            _currentRoom.DissolveSectors(false);
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