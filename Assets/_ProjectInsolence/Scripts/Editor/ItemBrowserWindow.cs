using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UIElements;
using static Insolence.Core.ItemEnums;
using static UnityEditor.Progress;
using static UnityEngine.EventSystems.EventTrigger;

namespace Insolence.Core
{
    public class ItemBrowserWindow : EditorWindow
    {
        private string searchString = "";
        private Vector2 scrollPos;
        private string[] itemTypes;
        private int selectedIndex;
        private Dictionary<string, Database> databases;

        static int e = 1;
        bool[] foldout = new bool[e];
        
        [MenuItem("Insolence Tools/Item Browser")]
        public static void ShowWindow()
        {
            GetWindow<ItemBrowserWindow>("Item Browser");
        }

        private void OnEnable()
        {
            databases = new Dictionary<string, Database>();
            itemTypes = GetItemTypes();
            selectedIndex = 0;
        }

        private void OnGUI()
        {
            selectedIndex = EditorGUILayout.Popup("Type", selectedIndex, itemTypes);
            string itemType = itemTypes[selectedIndex];

            searchString = EditorGUILayout.TextField("Search", searchString);

            if (GUILayout.Button("Create Item"))
            {
                ItemCreationWindow.ShowWindow();
            }
            
            if (GUILayout.Button("Collect Items"))
            {
                itemTypes = GetItemTypes();

                databases[itemType].CollectItems();
                   
            }
            if (!databases.ContainsKey(itemType))
            {

                databases[itemType] = CreateDatabase(itemType);

            }
                Database database = databases[itemType];
                EditorGUILayout.LabelField("Items", EditorStyles.boldLabel);
                e = database.items.Count;
                scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
                
                int i = 0;
                
                foreach (KeyValuePair<string, Item> entry in database.items)
                {
                    if (entry.Value.name.ToLower().Contains(searchString.ToLower()) || entry.Value.itemID.ToLower().Contains(searchString.ToLower()))
                    {
                        foldout[i] = EditorGUILayout.Foldout(foldout[i], entry.Value.name);

                        if (foldout[i])
                        {
                            EditorGUILayout.ObjectField(entry.Value, typeof(Item), false);
                            entry.Value.itemID = EditorGUILayout.TextField("ID", entry.Value.itemID);
                        }
                        EditorGUILayout.EndFoldoutHeaderGroup();
                    }
                    i++;
                }
            EditorGUILayout.EndScrollView();
        }

        private string[] GetItemTypes()
        {
            HashSet<string> types = new HashSet<string>();
            string[] guids = AssetDatabase.FindAssets("t:Item");
            foreach (string guid in guids)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                Item item = AssetDatabase.LoadAssetAtPath<Item>(assetPath);
                types.Add(item.type.ToString());
            }
            types.Add("All Items");
            return new List<string>(types).ToArray();
        }

        private Database CreateDatabase(string itemType)
        {
            string path = "Assets/_ProjectInsolence/Databases/";
            if (!AssetDatabase.IsValidFolder(path))
            {
                AssetDatabase.CreateFolder("_ProjectInsolence", "Databases");
            }
            
            if (itemType == "All Items")
            {
                Database AllItemsDB = ScriptableObject.CreateInstance<AllItemsDB>();
                AllItemsDB.itemType = itemType;
                AllItemsDB.CollectItems();
                AssetDatabase.CreateAsset(AllItemsDB, "Assets/_ProjectInsolence/Databases/AllItemsDatabase.asset");
                AssetDatabase.SaveAssets();
                return AllItemsDB;
            }
            else
            {
                path += itemType + "Database.asset";
                Database database = ScriptableObject.CreateInstance<Database>();
                database.itemType = itemType;
                database.CollectItems();
                AssetDatabase.CreateAsset(database, path);
                AssetDatabase.SaveAssets();
                return database;
            }
            

            
        }
        
        public Item CreateItem()
        {
            string path = "Assets/_ProjectInsolence/Scripts/InventorySystem/Items/Items/ScriptableObjects/";

            path += "New Item.asset";
            Item item = ScriptableObject.CreateInstance<Item>();
            AssetDatabase.CreateAsset(item, path);
            AssetDatabase.SaveAssets();
            return item;
        }
    }
}
