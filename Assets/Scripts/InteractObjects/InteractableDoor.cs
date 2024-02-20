using DG.Tweening;
using UnityEngine;

namespace InteractObjects
{
    [RequireComponent(typeof(InteractableMoveToRoom))]
    public class InteractableDoor : InteractableObject
    {
        [SerializeField] private string keyString;
        private InteractableMoveToRoom _moveToRoom;
        private Transform _doorPivot;
        [SerializeField] private float openAngle = 15f;

        private void Start()
        {
            _moveToRoom = GetComponent<InteractableMoveToRoom>();
            _moveToRoom.enabled = false;
            _doorPivot = transform.parent;
        }

        public override void Interact()
        {
            var angle = _doorPivot.localRotation.eulerAngles;
            angle.y += openAngle;
            _doorPivot.DOLocalRotate(angle, .5f);
            _moveToRoom.enabled = true;
            Destroy(this);
        }
    }
}