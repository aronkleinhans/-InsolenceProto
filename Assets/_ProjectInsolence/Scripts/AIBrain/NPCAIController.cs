using Insolence.AIBrain.KCC;
using Insolence.Core;
using UnityEngine;
using System.Collections;


namespace Insolence.AIBrain
{
    public class NPCAIController : MonoBehaviour
    {
        public NpcKinematicController mover { get; set; }
        public AIBrain brain { get; set; }
        public Action[] availableActions;
        public GameObject targetInteractable;

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

        public bool GetInteractable()
        {
            return targetInteractable != null;
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
        public void DoInteract()
        {
            Debug.Log("Interacting");
            StartCoroutine(InteractCoroutine());
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
                this.gameObject.GetComponent<CharacterStatus>().ChangeHunger(-1);

            }
            //determine hunger regen based on food type etc
            //logic to update max health
            Debug.Log("Eating done regenerated x health, x maxStamina and x hunger!");
            OnFinishedAction();
        }

        private IEnumerator InteractCoroutine()
        {
            if (targetInteractable)
            {
                yield return new WaitForSeconds(1);

                targetInteractable.GetComponent<Interactable>().Interaction(transform);
                targetInteractable = null;
                brain.bestAction = null;
                OnFinishedAction();
            }

        }

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Interactable")
            {
                Debug.Log("Interactable detected");
                //get interactable
                targetInteractable = other.gameObject;

            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Interactable" && other.gameObject.name == targetInteractable.name)
            {
                Debug.Log("Interactable lost");
                targetInteractable = null;
            }
            OnFinishedAction();
        }

        private void OnTriggerStay(Collider other)
        {
            if (targetInteractable == null)
            {
                if (other.gameObject.tag == "Interactable")
                {
                    targetInteractable = other.gameObject;
                }
            }
        }
    }
}