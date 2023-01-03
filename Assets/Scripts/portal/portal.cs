using KinematicCharacterController.Examples;
using Insolence.KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using KinematicCharacterController.Insolence;
using KinematicCharacterController;

namespace Insolence.core
{

    //fix this script so it works like Teleporter.cs


    public class portal : MonoBehaviour
    {
        [SerializeField] string targetScene;
        [SerializeField] string targetSpawn;
        [SerializeField] public GameObject spawnPoint;
        public portal TeleportTo;

        public UnityAction<KineCharacterController> OnCharacterTeleport;

        public bool isBeingTeleportedTo { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Debug.Log("Player entered portal: " + other.name);
                KineCharacterController player = other.GetComponent<KineCharacterController>();
                if (player != null)
                {
                    //check if scene loading is required, then load
                    if (SceneManager.GetActiveScene().name != targetScene)
                    {
                        Debug.Log("Teleported (" + other.name + ") to " + targetScene + " at " + targetSpawn + " spawn.");

                        GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();

                        GM.SaveOnPortal();
                        GM.LoadOnPortal(targetScene, targetSpawn);
                    }
                    //otherwise move player to target spawn point
                    else
                    {
                        Debug.Log("Teleported (" + other.name + ") to " + targetSpawn + " in the scene.");

                        TeleportPlayer();
                        
                        if (OnCharacterTeleport != null)
                        {
                            OnCharacterTeleport(player);
                        }
                        TeleportTo.isBeingTeleportedTo = true;
                    }
                }
            }
        }
        public void TeleportPlayer()
        {
            KinematicCharacterMotor player = SaveUtils.GetPlayer().GetComponent<KinematicCharacterMotor>();
            player = SaveUtils.GetPlayer().GetComponent<KinematicCharacterMotor>();
            player.SetPositionAndRotation(TeleportTo.GetComponentInChildren<SpawnPlayer>().transform.position, TeleportTo.GetComponentInChildren<SpawnPlayer>().transform.rotation);
        }
    }
}