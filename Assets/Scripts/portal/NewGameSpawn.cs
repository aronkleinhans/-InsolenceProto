using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameSpawn : MonoBehaviour
{
    public SpawnPlayer spawner;
    private void Awake()
    {
        spawner.Spawn();
        Destroy(gameObject);
    }
}
