using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Insolence.UI;

namespace Insolence.Core
{
    public class PlayerInteraction : MonoBehaviour
    {
        [Header("Interactables")]
        [SerializeField] GameObject targetInteractable;
        [SerializeField] InGameUIController uiControl;

        private void Start()
        {
            uiControl = GameObject.Find("InGameUI").GetComponent<InGameUIController>();
        }
        void Update()
        {
            HandleInteraction();
        }
        private void HandleInteraction()
        {
            if (targetInteractable != null)
            {
                uiControl.InteractPopUp(targetInteractable);

                if (Input.GetButtonDown("Interact"))
                {
                    if (targetInteractable)
                    {
                        Debug.Log("Interacting.");

                        targetInteractable.GetComponent<Interactable>().Interaction(transform);
                    }
                    else Debug.Log("can't interact");
                }

            }
            else
                uiControl.closeInteractPopUp();

        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Interactable")
            {
                targetInteractable = other.gameObject;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Interactable" && other.gameObject.name == targetInteractable.name)
            {
                targetInteractable = null;
            }
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