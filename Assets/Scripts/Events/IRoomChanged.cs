using EventBusSystem;
using Rooms;

namespace Events
{
    public interface IRoomChanged : IGlobalSubscriber
    {
        public void OnRoomChanged(Room newRoom);
    }
}