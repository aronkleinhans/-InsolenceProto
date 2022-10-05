using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Levels to Load")]
    [SerializeField] string newGameLevel;

    public string levelToLoad;
    public GameObject noSaveDialog;

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void LoadGameDialogYes(ObjectState objectState)
    {
        levelToLoad = objectState.genericValues["savedLevel"].ToString();
        SceneManager.LoadScene(levelToLoad);
    }

    public GameObject getNoSaveDialog()
    {
        return noSaveDialog;
    }
}
