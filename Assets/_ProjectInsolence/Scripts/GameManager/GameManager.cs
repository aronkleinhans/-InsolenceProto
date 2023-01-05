using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Insolence.SaveUtility;

namespace Insolence.Core
{
    public class GameManager : MonoBehaviour
    {
        [Header("Save Paths")]
        [SerializeField] public string playerPath;
        [SerializeField] public string objectPath;

        [SerializeField] PlayerInfo playerInfo;
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

        public IEnumerator waitForSceneLoad(string pp, string op, string sceneName, string spawn)
        {
            if (sceneName == "")
            {
                sceneName = SaveUtils.GetSavedSceneName(playerPath);

                while (!SceneManager.GetSceneByName(sceneName).isLoaded)
                {
                    yield return null;
                }
                if (SceneManager.GetSceneByName(sceneName).isLoaded)
                {
                    GameObject.Find(spawn).GetComponentInChildren<SpawnPlayer>().Spawn();
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

                    playerInfo.UpdateCharacterState(player);
                }
            }

        }
    }
}