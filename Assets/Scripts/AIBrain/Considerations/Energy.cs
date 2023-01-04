using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Insolence.AIBrain.Considerations
{
    [CreateAssetMenu(fileName = "Energy", menuName = "Insolence/AIBrain/Considerations/Energy")]
    public class Energy : Consideration
    {
        public override float ScoreConsideration()
        {
            //logic to score energy(maxStamina)
            return 0.1f;
        }
    }
}