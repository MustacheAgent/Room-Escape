using DG.Tweening;
using EventBusSystem;
using Events.InventoryEvents;
using Items;
using Managers;
using UnityEngine;

namespace InteractObjects
{
    [RequireComponent(typeof(InteractableMoveToRoom))]
    public class InteractableDoor : InteractableObject, IUsable
    {
        [SerializeField] private string keyString;
        private InteractableMoveToRoom _moveToRoom;
        private Transform _doorPivot;
        [SerializeField] private float openAngle = 15f;

        [field: SerializeField] public bool DestroyItem { get; private set; }

        private void Start()
        {
            _moveToRoom = GetComponent<InteractableMoveToRoom>();
            _moveToRoom.enabled = false;
            _doorPivot = transform.parent;
        }

        public override void Interact()
        {
            UseItem(InventoryManager.SelectedItem);
        }

        public void UseItem(Item item)
        {
            if (item == null)
            {
                Debug.Log("No item!");
            }
            else if (item.keyString == keyString)
            {
                var angle = _doorPivot.localRotation.eulerAngles;
                angle.y += openAngle;
                _doorPivot.DOLocalRotate(angle, .5f);
                _moveToRoom.enabled = true;
                if (DestroyItem) EventBus.Raise<IInventoryItem>(h => h.OnItemRemoved(item));
                Destroy(this);
            }
            else
            {
                Debug.Log("Wrong key!");
            }
        }
    }
}