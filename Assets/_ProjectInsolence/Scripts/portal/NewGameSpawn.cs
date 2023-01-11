using Insolence.Core;
using Insolence.SaveUtility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameSpawn : MonoBehaviour
{
    public SpawnPlayer spawner;
    private void Start()
    {
        Debug.Log("New Game, Spawning Player");
        spawner.Spawn();
        Destroy(gameObject);
    }
}
