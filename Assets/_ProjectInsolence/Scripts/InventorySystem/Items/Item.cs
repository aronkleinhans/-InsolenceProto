using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insolence.Core
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Insolence/Inventory/Items/Item")]
    public class Item : ScriptableObject
    {
        [Header("Item Info")]
        [SerializeField] string name;
        [SerializeField] string description;
        [SerializeField] int value;
        [SerializeField] public ItemEnums.ItemType type;
        [SerializeField] public ItemEnums.ItemSize size;
        [SerializeField] public ItemEnums.WeaponType weaponType;
        [SerializeField] public ItemEnums.ArmorType armorType;
        

        [Header("Item Properties")]
        [SerializeField] public GameObject itemPrefab;
        [SerializeField] public GameObject itemEquipPrefab;
        [SerializeField] bool isTwoHanded;
        
        [Header("Item Stats & Bonuses")]
        [SerializeField] int damage;        
        [SerializeField] int attackSpeed;
        [SerializeField] int defense;
        
        [SerializeField] int armor;
        [SerializeField] int health;
        [SerializeField] int stamina;

        [SerializeField] SpecialEffect specialEffect;
    }
}
