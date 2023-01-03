using KinematicCharacterController.Examples;
using Insolence.KinematicCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using KinematicCharacterController.Insolence;

namespace Insolence.core
{

    //fix this script so it works like Teleporter.cs


    public class portal : MonoBehaviour
    {
        [SerializeField] string targetScene;
        [SerializeField] string targetSpawn;
        [SerializeField] public GameObject spawnPoint;

        public bool isBeingTeleportedTo { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                Debug.Log("Player entered portal: " + other.name);
                KineCharacterController player = other.GetComponent<KineCharacterController>();
                if (player != null)
                {
                    
                    Debug.Log("Teleported (" + other.name + ") to " + targetScene + " at " + targetSpawn);

                    GameManager GM = GameObject.Find("GameManager").GetComponent<GameManager>();

                    GM.SaveOnPortal();
                    GM.LoadOnPortal(targetScene, targetSpawn);


                }
            }
        }
    }
}