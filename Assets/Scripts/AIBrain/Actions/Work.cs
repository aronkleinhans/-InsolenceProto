using UnityEngine;


namespace Insolence.AIBrain.Actions
{
    [CreateAssetMenu(fileName = "Work", menuName = "Insolence/AI/Actions/Work", order = 1)]
    public class Work : Action
    {
        public override void Execute(NPCAIController npc)
        {
            npc.DoWork(3, this.name);
        }
    }
}