using DG.Tweening;
using UnityEngine;

namespace InteractObjects
{
    public class InteractableShelfDoor : MonoBehaviour, IInteractable
    {
        public bool Enabled => _collider.enabled;
        private Collider _collider;

        [SerializeField] private float rotateAngle;
        private int _side = 1;
        private Transform _pivot;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _pivot = transform.parent;
        }
        
        public void Interact()
        {
            var eulerAngles = _pivot.localRotation.eulerAngles;
            var newAngle = new Vector3(eulerAngles.x, eulerAngles.y + rotateAngle * _side, eulerAngles.z);
            _pivot.DOLocalRotate(newAngle, 1f);
            _side = -_side;
        }

        public void SetEnabled(bool isEnabled)
        {
            _collider.enabled = isEnabled;
        }
    }
}