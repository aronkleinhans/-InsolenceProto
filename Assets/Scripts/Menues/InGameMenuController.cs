using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Insolence.KinematicCharacterController;
using System;

public class InGameMenuController : MonoBehaviour
{
    public GameObject menu;

    private bool menuOpen = false;

    private void Start()
    {
        menu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuOpen)
            {
                menu.SetActive(false);
                menuOpen = false;
            }
            else
            {
                menu.SetActive(true);
                menuOpen = true;
            }
        }
        if (menuOpen)
        {
            Debug.Log("in game menu open");

        }
        


    }
}
