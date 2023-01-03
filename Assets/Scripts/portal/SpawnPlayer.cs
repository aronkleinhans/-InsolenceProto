using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using Insolence.core;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject spawned;
    public KinematicCharacterMotor kcc;
    public GameObject Spawn()
    {
        Destroy(SaveUtils.GetPlayer());
        spawned = Instantiate(player);
        kcc = spawned.GetComponent<KinematicCharacterMotor>();
        kcc.SetPosition(transform.position);
        kcc.SetRotation(transform.rotation);

        return spawned;
    }
}
