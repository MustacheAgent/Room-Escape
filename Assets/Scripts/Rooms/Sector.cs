using UnityEngine;

namespace Rooms
{
    public class Sector : MonoBehaviour
    {
        //public float angle;

        public Dissolve[] _children;

        private void Awake()
        {
            _children = GetComponentsInChildren<Dissolve>();
        }

        public void SwitchObjects(bool hide)
        {
            foreach (var obj in _children)
            {
                obj.Switch(hide);
            }
        }
    }
}