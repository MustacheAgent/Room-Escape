using EventBusSystem;
using Events;
using Rooms;
using UnityEngine;

public class RoomManager : MonoBehaviour, IRoomChanged
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

    public void OnRoomChanged(Room newRoom)
    {
        _currentRoom = newRoom;
    }
}