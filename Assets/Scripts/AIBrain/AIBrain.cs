using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insolence.AIBrain
{
    public class AIBrain : MonoBehaviour
    {
        //AIBrain is responsible for scoring a list of Actions and choosing the best one to perform and puts it out for all other scripst to perform that Action
        //AIBrain is also responsible for updating the Action's score based on the current state of the world and the NPC
        public Action bestAction { get; set; }
        private NPCAIController npc;


        // Start is called before the first frame update
        void Start()
        {
            npc = GetComponent<NPCAIController>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void ScoreAction(Action action)
        {
            //score the action based on the current state of the world and the NPC
        }

        public void ChooseBestAction(Action[] actions)
        {
            //choose the best action from the list of available actions
        }
    }
}