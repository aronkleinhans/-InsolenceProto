using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insolence.Core
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Insolence/Inventory/Items/Item")]
    public class Item : ScriptableObject
    {
        [Header("Item Info")]
        [SerializeField] public string itemID;
        [SerializeField] public string name;
        [SerializeField] public string description;
        [SerializeField] public int value;
        [SerializeField] public ItemEnums.ItemType type;
        [SerializeField] public ItemEnums.ItemSize size;
        [SerializeField] public ItemEnums.WeaponType weaponType;
        [SerializeField] public ItemEnums.ArmorType armorType;
        

        [Header("Item Properties")]
        [SerializeField] public GameObject itemPrefab;
        [SerializeField] public GameObject itemEquipPrefab;
        [SerializeField] public bool isTwoHanded;
        [SerializeField] public bool pairedWeapon; //like DS3 dual wield

        [Header("Item Stats & Bonuses")]
        [SerializeField] public int damage;        
        [SerializeField] public int attackSpeed;
        [SerializeField] public int defense;
        
        [SerializeField] public int armor;
        [SerializeField] public int health;
        [SerializeField] public int stamina;

        [SerializeField] public SpecialEffect specialEffect;
    }
}
