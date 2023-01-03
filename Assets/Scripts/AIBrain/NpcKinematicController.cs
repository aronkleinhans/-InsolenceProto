using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
    
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Insolence.KinematicCharacterController.AI
{
    public class NpcKinematicController : MonoBehaviour
    {
        public KineCharacterController character;
        
        private Transform target;
        private NavMeshAgent agent;

        // Use this for initialization
        void OnEnable()
        {
            agent = GetComponent<NavMeshAgent>();
            character = GetComponent<KineCharacterController>();
        }

        void Update()
        {          
            ApplyInputs(agent.velocity);
        }
        
        private void ApplyInputs(Vector3 target)
        {
            AICharacterInputs inputs = new AICharacterInputs();

            //set the KKC inputs from navmesh agent velocity
            
            inputs.MoveVector = target;
            inputs.LookVector = target;

            character.SetInputs(ref inputs);
            
        }
        public void MoveTo(Vector3 target)
        {
            agent.SetDestination(target);
        }

    }
}