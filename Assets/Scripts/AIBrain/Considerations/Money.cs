using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insolence.AIBrain.Considerations
{
    [CreateAssetMenu(fileName = "Money", menuName = "Insolence/AIBrain/Considerations/Money")]
    public class Money : Consideration
    {
        public override float ScoreConsideration()
        {
            //logic to score money
            return 0.9f;
        }
    }
}
