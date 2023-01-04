using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insolence.AIBrain
{
    [CreateAssetMenu(fileName = "New Action", menuName = "InsolenceAI/Action")]
    public abstract class Action : ScriptableObject
    {
        [SerializeField]private string name;
        private float _score;

        public float score
        {
            get { return _score; }
            set 
            {
                this._score = Mathf.Clamp01(value);
            }
        }

        public Consideration[] considerations;

        public virtual void Awake()
        {
            score = 0;
            
        }

        public abstract void Execute();
    }
}