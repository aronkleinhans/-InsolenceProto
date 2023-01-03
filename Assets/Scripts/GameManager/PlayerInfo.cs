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
        [SerializeField] int health;

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
            health = currentState.health;
        }
        public void UpdateCharacterState(GameObject player)
        {

            currentState = player.GetComponent<CharacterState>();

            currentState.level = level;
            currentState.health = health;
            currentState.name = playerName;
        }
    }
}