using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Insolence.core;

public class DebugMenuController : MonoBehaviour
{
    [Header ("UI Elements")]
    [SerializeField] Button saveButton;
    [SerializeField] Button loadButton;
    [SerializeField] GameObject loadScreen;
    [SerializeField] bool showDebug = false;

    private void Start()
    {
        saveButton.onClick.AddListener(() => {
            SaveUtils.DoSave(GameManager.GetCurrentSceneName());
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            gameObject.GetComponent<Canvas>().enabled = !showDebug;
            showDebug = !showDebug;
        }
    }

    public void debugLoad()
    {
        SaveUtils.DoLoad(GameObject.Find("GameManager").GetComponent<GameManager>().playerPath, "", true, GameManager.GetCurrentSceneName());
    }
}
