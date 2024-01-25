using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rooms
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private List<Sector> sectors;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void DissolveSectors(float currentAngle, float angleStep)
        {
            foreach (var sector in sectors)
            {
                var difference = Mathf.Abs(Mathf.DeltaAngle(sector.angle, currentAngle));
                sector.SwitchObjects(difference <= angleStep / 2);
            }
        }

        public void SwitchRoom(bool hide)
        {
            foreach (var sector in sectors)
            {
                sector.SwitchObjects(hide);
            }
        }
    }
}