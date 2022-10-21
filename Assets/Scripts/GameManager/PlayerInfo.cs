using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] string playerName;
    [SerializeField] int level;
    [SerializeField] int health;

    public Vector3 playerPosition;
    public PlayerState currentState;
    public GameObject spawnPoint;

    void Update()
    {
        
    }

    public void GetPlayerInfo()
    {

        currentState = SaveUtils.GetPlayer().GetComponent<PlayerState>();

        playerName = currentState.name;
        level = currentState.level;
        health = currentState.health;
    }
    public void UpdatePlayerState(GameObject player)
    {

        currentState = player.GetComponent<PlayerState>();

        currentState.level = level;
        currentState.health = health;
        currentState.name = playerName;
    }
}
