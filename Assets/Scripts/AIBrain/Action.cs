using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Insolence.core;

namespace Insolence.AIBrain
{
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

        public abstract void Execute(NPCAIController npc);
    }
}