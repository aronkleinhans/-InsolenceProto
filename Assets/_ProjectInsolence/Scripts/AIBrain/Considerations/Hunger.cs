using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insolence.AIBrain.Considerations
{
    [CreateAssetMenu(fileName = "Hunger", menuName = "Insolence/AIBrain/Considerations/Hunger")]
    public class Hunger : Consideration
    {
        public override float ScoreConsideration()
        {
            //logic to score hunger
            return 0.2f;
        }
    }
}