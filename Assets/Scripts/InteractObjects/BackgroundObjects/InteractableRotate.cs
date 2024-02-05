using DG.Tweening;
using UnityEngine;

namespace InteractObjects.BackgroundObjects
{
    public class InteractableRotate : InteractableObject
    {
        [SerializeField] private float rotateAngle = 90f;

        private float _currentAngle;
        private Transform _transform;

        private void Start()
        {
            _transform = transform;
            _currentAngle = _transform.localRotation.eulerAngles.y;
        }

        public override void Interact()
        {
            _currentAngle = Mathf.Repeat(_currentAngle + rotateAngle, 360);
            var eulerAngles = _transform.localRotation.eulerAngles;
            eulerAngles.y = _currentAngle;

            SetEnabled(false);
            _transform.DOLocalRotate(eulerAngles, 1f).OnComplete(() => SetEnabled(true));
        }
    }
}