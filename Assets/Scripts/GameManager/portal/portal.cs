using KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal : MonoBehaviour
{
    [SerializeField] string targetScene;
    [SerializeField] string targetSpawn;
    [SerializeField] GameObject spawnPoint;

    [SerializeField] MenuController menuController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerCharacterController>())
        {
            Debug.Log("Player entered portal: " + other.name);
            
            GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();

            other.GetComponent<KinematicCharacterMotor>().SetPosition(gameObject.GetComponentInChildren<SpawnPlayer>().transform.position);

            GM.SaveOnPortal();
            GM.LoadOnPortal(targetScene, targetSpawn);
        }
    }
}
