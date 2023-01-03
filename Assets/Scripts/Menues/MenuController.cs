using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Insolence.core;

public class MenuController : MonoBehaviour
{
    [Header("Levels to Load")]
    [SerializeField] string newGameLevel;

    public string levelToLoad;
    public GameObject noSaveDialog;

    [Header("UI elements")]
    [SerializeField] GameObject savedGameButtonPrefab;
    [SerializeField] GameObject loadMenuContent;
    [SerializeField] float sGBP_Y = 300f;


    //the start method here just starts a new game and hides the main menu(comment for menu testing)
    private void Start()
    {
        GameObject.Find("MainMenuCanvas").SetActive(false);
        NewGameDialogYes();
    }
    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(newGameLevel);
    }
    public void LoadGameDialog()
    {
        //get existing save files
        string[] saves = SaveUtils.GetSaves();
        if(saves.Length == 0)
        {
            GameObject noSave = GameObject.FindObjectOfType<MenuController>().GetNoSaveDialog();
            noSave.SetActive(true);
        }
        else
        {
            foreach (string s in saves)
            {

                //skip meta & txt files 
                if (s.Contains(".meta") || s.Contains(".txt"))
                {
                    continue;
                }
                // create a list of saved games by player name
                if (s.Contains(".insp"))
                {
                    int startpos = s.LastIndexOf("/") + 1;
                    int length = s.Length - s.Substring(s.IndexOf(".")).Length - s.Substring(0, startpos).Length;
                    string playerName = s.Substring(startpos, length);
                    GameObject button = Instantiate(savedGameButtonPrefab);
                    RectTransform rectTransform = button.GetComponent<RectTransform>();

                    rectTransform.SetParent(loadMenuContent.transform);
                    rectTransform.anchoredPosition = new Vector2(0, sGBP_Y);
                    sGBP_Y -= 150f;

                    button.GetComponentInChildren<TextMeshProUGUI>().text = playerName;
                    button.GetComponent<Button>().onClick.AddListener(() => { LoadGameDialogYes(s); });
                    // gotta fix clamping later...
                }
            }
        }      
    }
    public void LoadGameDialogYes(string s)
    {
        GameManager GM = GetComponent<GameManager>();
        GameObject.Find("MainMenuCanvas").SetActive(false);
        GM.playerPath = s;
        GM.objectPath = s.Substring(0, s.IndexOf(".")) + ".inso";


        if (!SceneManager.GetSceneByName(SaveUtils.GetSavedSceneName(GM.playerPath)).isLoaded)
        {
            Debug.Log("Loading scene: " + SaveUtils.GetSavedSceneName(GM.playerPath));
            SceneManager.LoadScene(SaveUtils.GetSavedSceneName(GM.playerPath));
            StartCoroutine(GM.waitForSceneLoad(GM.playerPath, GM.objectPath, "", ""));
        }
        else
        {
            StartCoroutine(GM.waitForSceneLoad(GM.playerPath, GM.objectPath, "", ""));
        }
        
        
    }

    public void ExitButton()
    {
        Debug.Log("quitting...");
        Application.Quit();
    }

    public GameObject GetNoSaveDialog()
    {
        return noSaveDialog;
    }
}
