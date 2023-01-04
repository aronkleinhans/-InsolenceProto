using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insolence.AIBrain
{   
    [CreateAssetMenu(fileName = "New Consideration", menuName = "InsolenceAI/Consideration")]
    public abstract class Consideration : ScriptableObject
    {
        public string name;
        
        private float _score;
        public float score
        {
            get { return _score; }
            set
            {
                this._score = Mathf.Clamp01(value);
            }
        }
        public virtual void Awake()
        {
            score = 0;
        }
        public abstract void ScoreConsideration();
    }
}