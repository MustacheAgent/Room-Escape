using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rooms
{
    public class Sector : MonoBehaviour
    {
        public float angle;
        public List<Dissolve> dissolveObjects;

        public void SwitchObjects(bool hide)
        {
            foreach (var obj in dissolveObjects)
            {
                obj.Switch(hide);
            }
        }
    }
}