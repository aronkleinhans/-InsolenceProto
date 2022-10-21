using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject spawned;
    public GameObject Spawn()
    {
        Destroy(SaveUtils.GetPlayer());
        spawned = Instantiate(player);

        spawned.transform.position = transform.position;
        spawned.transform.rotation = transform.rotation;

        return spawned;
    }
}
