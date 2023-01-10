using Insolence.SaveUtility;
using System.Collections.Generic;
using UnityEngine;

namespace Insolence.Core
{
    public class PlayerInfo : MonoBehaviour
    {
        [Header("Player Stats")]
        [SerializeField] string playerName;
        [SerializeField] int level;
        [SerializeField] int maxHealth;
        [SerializeField] int currentHealth;
        [SerializeField] int maxStamina;
        [SerializeField] int currentStamina;

        [SerializeField] Inventory inv;
        List<string> invList = new List<string>();
        [SerializeField] AllItemsDB database;

        public Vector3 playerPosition;
        public CharacterStatus currentState;
        public GameObject spawnPoint;

        void Update()
        {

        }

        public void GetPlayerInfo()
        {

            currentState = SaveUtils.GetPlayer().GetComponent<CharacterStatus>();

            playerName = currentState.name;
            level = currentState.level;
            maxHealth = currentState.maxHealth;
            currentHealth = currentState.currentHealth;
            maxStamina = currentState.maxStamina;
            currentStamina = currentState.currentStamina;
            
            inv = currentState.GetComponent<Inventory>();
            invList = inv.CreateItemIDList();
            database = currentState.database;
            database.CollectItems();
            invList.Reverse();


            foreach (string itemID in invList)
            {
                Debug.Log("Adding ItemID to inventory list: " + database.items[itemID].itemID);
            }
        }
        public void UpdateCharacterState(GameObject player)
        {

            currentState = player.GetComponent<CharacterStatus>();

            currentState.level = level;
            currentState.maxHealth = maxHealth;
            currentState.currentHealth = currentHealth;
            currentState.maxStamina = maxStamina;
            currentState.currentStamina = currentStamina;
            currentState.name = playerName;

            inv = currentState.GetComponent<Inventory>();

            foreach (string itemID in invList)
            {
                Debug.Log("Adding Item back to inventory by ID: " + itemID);
                inv.AddItem(database.items[itemID]);
            }
        }
    }
}