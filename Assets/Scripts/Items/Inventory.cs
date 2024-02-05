using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Scriptable Objects/Inventory", order = 0)]
    public class Inventory : ScriptableObject
    {
        public List<Item> items = new List<Item>();
    }
}