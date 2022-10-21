using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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

    [Header("Save Paths")]
    [SerializeField] string playerPath;
    [SerializeField] string objectPath;

    [SerializeField] PlayerInfo playerInfo;

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(newGameLevel);
    }
    public void LoadGameDialog()
    {
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


                if (s.Contains(".meta"))
                {
                    continue;
                }
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
                }
            }
        }      
    }
    public void LoadGameDialogYes(string s)
    {
        GameObject.Find("MainMenuCanvas").SetActive(false);
        playerPath = s;
        objectPath = s.Substring(0, s.IndexOf(".")) + ".inso";

        if (!SceneManager.GetSceneByName(SaveUtils.GetSavedSceneName(playerPath)).isLoaded)
        {
            Debug.Log("Loading scene: " + SaveUtils.GetSavedSceneName(playerPath));
            SceneManager.LoadScene(SaveUtils.GetSavedSceneName(playerPath));
            StartCoroutine(waitForSceneLoad(playerPath, objectPath, "", ""));
        }
        else
        {
            StartCoroutine(waitForSceneLoad(playerPath, objectPath, "", ""));
        }
        
        
    }
    public static string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    public void SaveOnPortal()
    {
        SaveUtils.DoSave(GetCurrentSceneName());
    }
    public void LoadOnPortal(string sceneName, string spawn)
    {
        playerInfo = gameObject.GetComponent<PlayerInfo>();
        playerInfo.GetPlayerInfo();

        SceneManager.LoadScene(sceneName);

        StartCoroutine(waitForSceneLoad("", "", sceneName, spawn));
    }

    private IEnumerator waitForSceneLoad(string pp, string op, string sceneName, string spawn)
    {
        if(sceneName == "")
        {
            sceneName = SaveUtils.GetSavedSceneName(playerPath);

            while (!SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                yield return null;
            }
            if (SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                SaveUtils.DoLoad(pp, op, true, sceneName);
            }
        }
        else
        {
            while (!SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                yield return null;
            }
            if (SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                GameObject player = GameObject.Find(spawn).GetComponentInChildren<SpawnPlayer>().Spawn();

                SaveUtils.DoLoad(pp, op, false, sceneName);

                playerInfo.UpdatePlayerState(player);
            }
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
