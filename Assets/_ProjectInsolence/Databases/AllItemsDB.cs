using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Insolence.Core
{
    public class AllItemsDB : Database
    {
        [ContextMenu("Collect All Items")]
        public override void CollectItems()
        {
            items.Clear();

            string[] guids = AssetDatabase.FindAssets("t:Item");
            contents.Clear();
            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                Item item = AssetDatabase.LoadAssetAtPath<Item>(assetPath);

                if (itemType == "All Items")
                {
                    items[item.itemID] = item;
                    contents.Add(item);
                }
            }
        }
    }
}
