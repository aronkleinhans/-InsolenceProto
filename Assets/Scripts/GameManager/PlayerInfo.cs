using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insolence.core
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

        public Vector3 playerPosition;
        public CharacterState currentState;
        public GameObject spawnPoint;

        void Update()
        {

        }

        public void GetPlayerInfo()
        {

            currentState = SaveUtils.GetPlayer().GetComponent<CharacterState>();

            playerName = currentState.name;
            level = currentState.level;
            maxHealth = currentState.maxHealth;
            currentHealth = currentState.currentHealth;
            maxStamina = currentState.maxStamina;
            currentStamina = currentState.currentStamina;
        }
        public void UpdateCharacterState(GameObject player)
        {

            currentState = player.GetComponent<CharacterState>();

            currentState.level = level;
            currentState.maxHealth = maxHealth;
            currentState.currentHealth = currentHealth;
            currentState.maxStamina = maxStamina;
            currentState.currentStamina = currentStamina;
            currentState.name = playerName;
        }
    }
}