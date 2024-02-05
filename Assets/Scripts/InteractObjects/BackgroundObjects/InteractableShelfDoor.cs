using System;
using DG.Tweening;
using UnityEngine;

namespace InteractObjects.BackgroundObjects
{
    public class InteractableShelfDoor : InteractableObject
    {
        [SerializeField] private float rotateAngle;
        private int _side = 1;
        private Transform _pivot;

        private void Start()
        {
            _pivot = transform.parent;
        }

        public override void Interact()
        {
            var eulerAngles = _pivot.localRotation.eulerAngles;
            var newAngle = new Vector3(eulerAngles.x, eulerAngles.y + rotateAngle * _side, eulerAngles.z);
            _pivot.DOLocalRotate(newAngle, 1f);
            _side = -_side;
        }
    }
}