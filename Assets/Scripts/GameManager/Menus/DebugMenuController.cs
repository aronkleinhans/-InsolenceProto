using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugMenuController : MonoBehaviour
{
    [Header ("UI Elements")]
    [SerializeField] Button saveButton;
    [SerializeField] Button loadButton;
    [SerializeField] bool showDebug = false;

    private void Start()
    {
        saveButton.onClick.AddListener(SaveUtils.DoSave);

        loadButton.onClick.AddListener(SaveUtils.DoLoad);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            gameObject.GetComponent<Canvas>().enabled = !showDebug;
            showDebug = !showDebug;
        }
    }
}
