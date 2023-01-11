using System;
using UnityEngine;
using Insolence.SaveUtility;
using System.Collections.Generic;
using UnityEditor;

namespace Insolence.Core
{
    public class CharacterStatus : MonoBehaviour
    {
        [Header("Character Stats")]
        [SerializeField] private int level = 1;
        [SerializeField] private int maxHealth = 50;
        [SerializeField] private int currentHealth = 50;
        [SerializeField] private int maxStamina = 100; //also used as energy for labor
        [SerializeField] private int currentStamina = 100;
        [SerializeField] private int hunger = 0;
        [SerializeField] private int gold = 0;
        [SerializeField] private new string name = "";
        [SerializeField] private AllItemsDB database;

        [SerializeField] Inventory inv;
        List<string> invList = new List<string>();

        [SerializeField] private string currentScene;

        private void Start()
        {
            DynamicObject dynamicObject = GetComponent<DynamicObject>();
            dynamicObject.prepareToSaveDelegates += PrepareToSaveObjectState;
            dynamicObject.loadObjectStateDelegates += LoadObjectState;

            string[] guids = AssetDatabase.FindAssets("t:AllItemsDB");
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
            database = AssetDatabase.LoadAssetAtPath<AllItemsDB>(assetPath);

            inv = GetComponent<Inventory>();
        }

        private void Update()
        {
            currentScene = gameObject.scene.name;
        }

        public Dictionary<string, string> GetStatus()
        {
            Dictionary<string, string> status = new Dictionary<string, string>();

            status.Add("name", name);
            status.Add("level", level.ToString());
            status.Add("maxHealth", maxHealth.ToString());
            status.Add("currentHealth", currentHealth.ToString());
            status.Add("maxStamina", maxStamina.ToString());
            status.Add("currentStamina", currentStamina.ToString());
            status.Add("hunger", hunger.ToString());
            status.Add("gold", gold.ToString());

            return status;
        }
        public void SetStatus(Dictionary<string, string> status)
        {
            Debug.Log("setting status after scene load");

            name = status["name"];
            level = Convert.ToInt32(status["level"]);
            maxHealth = Convert.ToInt32(status["maxHealth"]);
            currentHealth = Convert.ToInt32(status["currentHealth"]);
            maxStamina = Convert.ToInt32(status["maxStamina"]);
            currentStamina = Convert.ToInt32(status["currentStamina"]);
            hunger = Convert.ToInt32(status["hunger"]);
            gold = Convert.ToInt32(status["gold"]);

            Debug.Log("Done setting player status");
        }

        public string GetScene()
        {
            return currentScene;
        }

        public void Heal(int amount)
        {
            currentHealth += amount;
        }

        public void Damage(int amount)
        {
            currentHealth -= amount;
        }

        public void ChangeHunger(int amount)
        {
            hunger += amount;
        }


        
        private void PrepareToSaveObjectState(ObjectState objectState)
        {
            objectState.genericValues[name + ".Stats.level"] = level;
            objectState.genericValues[name + ".Stats.health"] = maxHealth;
            objectState.genericValues[name + ".Stats.currentHealth"] = currentHealth;
            objectState.genericValues[name + ".Stats.maxStamina"] = maxStamina;
            objectState.genericValues[name + ".Stats.currentStamina"] = currentStamina;
            objectState.genericValues[name + ".Stats.hunger"] = hunger;
            objectState.genericValues[name + ".Stats.name"] = name;
            objectState.genericValues["savedLevel"] = currentScene;

            objectState.genericValues[name + ".Inventory"] = inv.CreateItemIDList();
            objectState.genericValues[name + ".Inventory.gold"] = gold;
        }
        private void LoadObjectState(ObjectState objectState)
        {
            // Load the player's position & rotation
            transform.position = SaveUtils.ConvertToVector3(objectState.position);
            transform.rotation = SaveUtils.ConvertToQuaternion(objectState.rotation);
            // Load the reference to the stats
            level = Convert.ToInt32(objectState.genericValues[name + ".Stats.level"]);
            maxHealth = Convert.ToInt32(objectState.genericValues[name + ".Stats.health"]);
            currentHealth = Convert.ToInt32(objectState.genericValues[name + ".Stats.currentHealth"]);
            maxStamina = Convert.ToInt32(objectState.genericValues[name + ".Stats.maxStamina"]);
            currentStamina = Convert.ToInt32(objectState.genericValues[name + ".Stats.currentStamina"]);
            hunger = Convert.ToInt32(objectState.genericValues[name + ".Stats.hunger"]);
            name = Convert.ToString(objectState.genericValues[name + ".Stats.name"]);
            currentScene = Convert.ToString(objectState.genericValues["savedLevel"]);

            Debug.Log("Loading inventory");
            invList = (List<string>)objectState.genericValues[name + ".Inventory"];
            database.CollectItems();
            invList.Reverse();
            foreach (string itemID in invList)
            {
                Debug.Log("Loading item: " + itemID);
                inv.AddItem(database.items[itemID]);
            }
            
            gold = Convert.ToInt32(objectState.genericValues[name + ".Inventory.gold"]);

        }
    }
}