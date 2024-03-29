﻿using System;
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
            _interactables = transform.GetComponentsInFirstChildren<IInteractable>();
        }

        /// <summary>
        /// Sets sector visibility.
        /// </summary>
        /// <param name="hide">True if sector must dissolve, otherwise false.</param>
        public void SetVisibleObjects(bool hide)
        {
            try
            {
                foreach (var obj in _dissolvables)
                {
                    obj.SetVisible(hide);
                }
            
                foreach (var interactable in _interactables)
                {
                    interactable.SetEnabled(!hide);
                }
            }
            catch (Exception)
            {
                //Debug.LogError(e + " ==== " + gameObject);
            }
        }
    }
}