using DG.Tweening;
using UnityEngine;

namespace InteractObjects.BackgroundObjects
{
    public class InteractableRotate : MonoBehaviour, IInteractable
    {
        public bool Enabled => _collider.enabled;

        [SerializeField] private float rotateAngle = 90f;

        private float _currentAngle;
        private Transform _transform;
        private Collider _collider;

        private void Awake()
        {
            _transform = transform;
            _collider = GetComponent<Collider>();

            _currentAngle = _transform.localRotation.eulerAngles.y;
        }

        public void Interact()
        {
            _currentAngle = Mathf.Repeat(_currentAngle + rotateAngle, 360);
            var eulerAngles = _transform.localRotation.eulerAngles;
            eulerAngles.y = _currentAngle;

            SetEnabled(false);
            _transform.DOLocalRotate(eulerAngles, 1f).OnComplete(() => SetEnabled(true));
        }

        public void SetEnabled(bool isEnabled)
        {
            _collider.enabled = isEnabled;
        }
    }
}