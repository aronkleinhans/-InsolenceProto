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
        public bool finishedDeciding { get; set; }

        private NPCAIController npc;


        // Start is called before the first frame update
        void Start()
        {
            npc = GetComponent<NPCAIController>();
        }

        // Update is called once per frame
        void Update()
        {
            if (bestAction == null)
            {
                ChooseBestAction(npc.availableActions);
            }
        }
        public float ScoreAction(Action action)
        {
            //score the action based on the current state of the world and the NPC
            float score = 1f;
            for (int i = 0; i < action.considerations.Length; i++)
            {
                float considerationScore = action.considerations[i].ScoreConsideration();
                score *= considerationScore;

                if (score == 0)
                {
                    action.score = 0;
                    return action.score; //no point computing further if score hits 0
                }
            }
            //averaging scheme of overall score (by Dave Mark & friend :D)
            float originalScore = score;
            float modFactor = 1 - (1 / action.considerations.Length);
            float makeupValue = (1 - originalScore) * modFactor;
            action.score = originalScore + (originalScore * makeupValue);
            return action.score;
        }

        public void ChooseBestAction(Action[] actions)
        {
            //choose the best action from the list of available actions
            float bestScore = 0;
            int nextBestActionIndex = 0;
            for (int i = 0; i < actions.Length; i++)
            {
                float score = ScoreAction(actions[i]);
                if (score > bestScore)
                {
                    bestScore = score;
                    nextBestActionIndex = i;
                }
            }
            bestAction = actions[nextBestActionIndex];
            finishedDeciding = true;
        }
    }
}