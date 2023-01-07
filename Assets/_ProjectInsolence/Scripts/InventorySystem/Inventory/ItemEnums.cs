using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insolence.Core
{
    public class ItemEnums : MonoBehaviour
    {
        public enum ItemType
        {
            Weapon,
            Armor,
            Consumable,
            Quest,
            Key,
            Misc
        }

        public enum WeaponType
        {
            None,
            Sword,
            Axe,
            Hammer,
            Bow,
            Staff,
            Dagger,
            Shield,
            AncientScroll,
            Tome
        }
        public enum ArmorType
        {
            None,
            Head,
            Chest,
            Legs,
            Feet,
            Hands,
            Back,
            Neck,
            Ring
        }
        public enum ItemSize
        {
            None,
            Big,
            Medium,
            Small
        }
    }
}
