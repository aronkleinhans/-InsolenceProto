using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerInteraction : MonoBehaviour
{
    [Header("Interactables")]
    [SerializeField] Canvas uiCanvas;
    [SerializeField] GameObject targetInteractable;

    private void Start()
    {
        uiCanvas = GameObject.Find("PopUpCanvas").GetComponent<Canvas>();
    }
    void Update()
    {
        HandleInteraction();
    }
    private void HandleInteraction()
    {
        if (targetInteractable != null)
        {
            PopUp();

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
        else uiCanvas.enabled = false;

    }
    private void PopUp()
    {
        uiCanvas.enabled = true;
        uiCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "[F] " + targetInteractable.GetComponent<Interactable>().interactionType;
        uiCanvas.GetComponentInChildren<TextMeshProUGUI>().text += " " + targetInteractable.GetComponent<Interactable>().interactableName;
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
