using Insolence.KinematicCharacterController.AI;
using Insolence.core;
using UnityEngine;
using System.Collections;


namespace Insolence.AIBrain
{
    public class NPCAIController : MonoBehaviour
    {
        public NpcKinematicController mover { get; set; }
        public AIBrain brain { get; set; }
        public Action[] availableActions;

        public void Start()
        {
            mover = GetComponent<NpcKinematicController>();
            brain = GetComponent<AIBrain>();
        }

        public void Update()
        {
            if (brain.finishedDeciding) 
            { 
                if (brain.bestAction != null)
                {
                    brain.bestAction.Execute(this);
                    brain.finishedDeciding = false;
                }
            }
        }
        public void OnFinishedAction()
        {
            brain.ChooseBestAction(availableActions);
        }
        
        #region Coroutines

        public void DoWork(int time, string name)
        {
            Debug.Log("Doing work");
            StartCoroutine(WorkCoroutine(time, name));
        }
        public void DoSleep(int time)
        {
            Debug.Log("Sleeping");
            StartCoroutine(SleepCoroutine(time));
        }
        public void DoEat(int time)
        {
            Debug.Log("Eating");
            StartCoroutine(EatCoroutine(time));
        }
        private IEnumerator WorkCoroutine(int time, string name)
        {
            int counter = time;
            while (counter > 0)
            {
                yield return new WaitForSeconds(1);
                counter--;

            }
            //logic to update things involved with work
            Debug.Log(name + " done gathered x resource!");
            //decide new best action 
            OnFinishedAction();
        }
        private IEnumerator SleepCoroutine(int time)
        {
            int counter = time;
            while (counter > 0)
            {
                yield return new WaitForSeconds(1);
                counter--;
                
            }
            //logic to update max stamina
            Debug.Log("Sleep done regenerated x maxStamina!");
            OnFinishedAction();
        }
        private IEnumerator EatCoroutine(int time)
        {
            int counter = time;
            while (counter > 0)
            {
                yield return new WaitForSeconds(1);
                this.gameObject.GetComponent<CharacterState>().hunger -= 1;

            }
            //determine hunger regen based on food type etc
            //logic to update max health
            Debug.Log("Eating done regenerated x health, x maxStamina and x hunger!");
            OnFinishedAction();
        }

        #endregion
    }
}