using Insolence.KinematicCharacterController.AI;
using UnityEngine;

namespace Insolence.AIBrain
{
    public class NPCAIController : MonoBehaviour
    {
        public NpcKinematicController mover { get; set; }
        public AIBrain brain { get; set; }
        public Action[] availableActions;

        public void Start()
        {
            mover = GetComponent<NpcKinematicController>();
            brain = GetComponent<AIBrain>();
        }

        public void Update()
        {
            
        }
    }
}