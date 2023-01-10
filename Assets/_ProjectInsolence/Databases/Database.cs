using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Insolence.Core
{
    public class Database : ScriptableObject
    {
        [SerializeField] public string itemType;
        
        [SerializeField] public Dictionary<string, Item> items = new Dictionary<string, Item>();

        [SerializeField] public List<Item> contents = new List<Item>();

        [ContextMenu("Collect Items")]
        
        public virtual void CollectItems()
        {
            items.Clear();

            string[] guids = AssetDatabase.FindAssets("t:Item");
            contents.Clear();
            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                Item item = AssetDatabase.LoadAssetAtPath<Item>(assetPath);
                
                if (item.type.ToString() == itemType)
                {
                    items[item.itemID] = item; 
                    contents.Add(item);
                }
            }
        }

    }

}
