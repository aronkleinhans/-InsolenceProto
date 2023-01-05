using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Insolence.AIBrain;
using Unity.VisualScripting;

namespace Insolence.AIBrain.Actions
{
    [CreateAssetMenu(fileName = "Interact", menuName = "Insolence/AIBrain/Actions/Interact", order = 1)]
    public class AIInteraction : Action
    {
        [SerializeField] GameObject targetInteractable;
        public override void Execute(NPCAIController npc)
        {
            if (npc.GetInteractable())
            {
                npc.DoInteract();
            }
        }
    }
}