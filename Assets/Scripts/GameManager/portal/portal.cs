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
        if(other.gameObject.tag == "Player")
        {
            menuController = GameObject.Find("GameManager").GetComponent<MenuController>();

            other.transform.position = gameObject.GetComponentInChildren<SpawnPlayer>().transform.position;

            menuController.SaveOnPortal();
            menuController.LoadOnPortal(targetScene, targetSpawn);
        }
    }
}
