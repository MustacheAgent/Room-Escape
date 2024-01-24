using System.Collections.Generic;
using EventBusSystem;
using Events;
using UnityEngine;

namespace Rooms
{
    public class Room : MonoBehaviour, ICameraRotate
    {
        [SerializeField] private List<Sector> sectors;
        
        private void OnEnable()
        {
            EventBus.Subscribe(this);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(this);
        }

        public void HandleRotation(float currentAngle, float angleStep)
        {
            foreach (var sector in sectors)
            {
                var difference = Mathf.Abs(Mathf.DeltaAngle(sector.angle, currentAngle));
                sector.SwitchObjects(difference <= angleStep / 2);
            }
        }
    }
}