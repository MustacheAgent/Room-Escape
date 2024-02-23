using Managers;
using UnityEngine;

public class ServiceLocatorMain : MonoBehaviour 
{
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private RoomManager _roomManager;
    private void Awake()
    {
        ServiceLocator.Init();

        ServiceLocator.Current.Register<InventoryManager>(_inventoryManager);
        ServiceLocator.Current.Register<RoomManager>(_roomManager);
    }
}