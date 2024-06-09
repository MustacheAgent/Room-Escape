using UnityEngine;

namespace Rooms
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private Transform viewPoint;

        public Transform ViewPoint => viewPoint;
        
        private Sector[] _sectors;

        private void Awake()
        {
            _sectors = GetComponentsInChildren<Sector>();
            gameObject.SetActive(false);
        }

        public void DissolveSectors(float currentAngle, float angleStep)
        {
            foreach (var sector in _sectors)
            {
                var angleBetween = Mathf.Abs(Mathf.DeltaAngle(sector.transform.eulerAngles.y, currentAngle));
                sector.SetVisibleObjects(angleBetween <= angleStep / 2);
            }
        }

        public void DissolveSectors(bool hide)
        {
            foreach (var sector in _sectors)
            {
                sector.SetVisibleObjects(hide);
            }
        }
    }
}