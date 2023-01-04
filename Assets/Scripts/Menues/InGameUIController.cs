using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Insolence.KinematicCharacterController;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

namespace Insolence.UI
{
    public class InGameUIController : MonoBehaviour
    {
        [SerializeField] public Canvas popUpCanvas;
        [SerializeField] public Canvas inGameMenuCanvas;

        private bool menuOpen = false;

        private void Start()
        {
            inGameMenuCanvas.enabled = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (menuOpen)
                {
                    inGameMenuCanvas.enabled = false;
                    menuOpen = false;
                }
                else
                {
                    inGameMenuCanvas.enabled = true;
                    menuOpen = true;
                }
            }
            if (menuOpen)
            {
                Debug.Log("in game menu open");

            }
        }

        //handle interactable pop up message
        public void InteractPopUp(GameObject targetInteractable)
        {
            if (targetInteractable)
            {
                popUpCanvas.enabled = true;
                popUpCanvas.GetComponentInChildren<TextMeshProUGUI>().text = "[F] " + targetInteractable.GetComponent<Interactable>().interactionType;
                popUpCanvas.GetComponentInChildren<TextMeshProUGUI>().text += " " + targetInteractable.GetComponent<Interactable>().interactableName;
            }
        }
        public void closeInteractPopUp()
        {
            popUpCanvas.enabled = false;
        }
    }
}