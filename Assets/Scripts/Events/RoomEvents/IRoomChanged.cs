using EventBusSystem;
using Rooms;

namespace Events.RoomEvents
{
    public interface IRoomChanged : IGlobalSubscriber
    {
        public void OnRoomChanged(Room newRoom);
    }
}