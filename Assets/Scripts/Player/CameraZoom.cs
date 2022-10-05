using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [Header("Zoom Settings")]
    [SerializeField] Cinemachine.CinemachineFreeLook cam;
    [SerializeField]  float[] minCameraDistance = {1f, 4f, 1f};
    [SerializeField] float[] maxCameraDistance = { 10f, 13f, 10f };
    [SerializeField] float[] currentCameraDistance = new float[3];
    [SerializeField] float sensitivity = 10f;

    private void Update()
    {
        //check if cam exists, else log no cam
        if (cam == null)
        {
            Debug.Log("No zoomable camera attached!");
        }
        else { 
            //get current distances
            for (int i = 0; i < 3; i++)
            {
                currentCameraDistance[i] = cam.m_Orbits[i].m_Radius;
            }

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    //change rig radii on scroll
                    cam.m_Orbits[i].m_Radius -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;


                    //reset cam to min/max
                    if (cam.m_Orbits[i].m_Radius < minCameraDistance[i])
                    {
                        cam.m_Orbits[i].m_Radius = minCameraDistance[i];
                    }
                    else if (cam.m_Orbits[i].m_Radius > maxCameraDistance[i])
                    {
                        cam.m_Orbits[i].m_Radius = maxCameraDistance[i];
                    }
                }
            }
        }
    }
}
