using Insolence.SaveUtility;
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
        }
    }
}