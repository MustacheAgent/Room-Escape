using System;
using InteractObjects;
using UnityEngine;

namespace Rooms
{
    public class Sector : MonoBehaviour
    {
        private Dissolve[] _dissolvables;
        private IInteractable[] _interactables;
        
        private void Awake()
        {
            _dissolvables = GetComponentsInChildren<Dissolve>();
            _interactables = GetComponentsInChildren<IInteractable>();
        }

        /// <summary>
        /// Switches sector visibility.
        /// </summary>
        /// <param name="hide">True if sector must dissolve, otherwise false.</param>
        public void SwitchObjects(bool hide)
        {
            try
            {
                foreach (var obj in _dissolvables)
                {
                    obj.Switch(hide);
                }
            
                foreach (var interactable in _interactables)
                {
                    interactable.Enabled = !hide;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e + " ==== " + gameObject);
            }
        }
    }
}