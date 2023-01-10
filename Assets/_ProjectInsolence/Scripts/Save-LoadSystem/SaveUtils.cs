using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Insolence.KinematicCharacterController;
using Insolence.Core;

namespace Insolence.SaveUtility
{
    public class SaveUtils
    {
        // The directory under Resources that the dynamic objects' prefabs can be loaded from
        //private static string ENVIRONMENT_PREFABS_PATH = "Prefabs/Environment/DynamicObjects";
        //private static string CHARACTERS_PREFABS_PATH = "Prefabs/Characters";
        private static string PREFABS_PATH = "Prefabs";
        // A dictionary of prefab guid to prefab
        //public static Dictionary<string, GameObject> envPrefabs = LoadPrefabs(ENVIRONMENT_PREFABS_PATH);
        //public static Dictionary<string, GameObject> charPrefabs = LoadPrefabs(CHARACTERS_PREFABS_PATH);
        public static Dictionary<string, GameObject> allPrefabs = LoadPrefabs(PREFABS_PATH);

        //public static Dictionary<string, GameObject>[] prefabCollection = new Dictionary<string, GameObject>[] { envPrefabs, charPrefabs, itemsPrefabs };

        //public static Dictionary<string, GameObject> mergedPrefabs = mergeDictionaries(prefabCollection);


        public static string SAVE_OBJECTS_PATH = Application.persistentDataPath + "/savegames/default.inso";
        public static string SAVE_PLAYER_PATH = Application.persistentDataPath + "/savegames/default.insp";

        private static Dictionary<string, GameObject> mergeDictionaries(Dictionary<string, GameObject>[] dictionaries)
        {
            Dictionary<string, GameObject> result = new Dictionary<string, GameObject>();
            foreach (var dict in dictionaries)
            {
                foreach (var item in dict)
                {
                    result.Add(item.Key, item.Value);
                }
            }
            return result;
        }

        private static Dictionary<string, GameObject> LoadPrefabs(string prefabsPath)
        {
            Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

            GameObject[] allPrefabs = Resources.LoadAll<GameObject>(prefabsPath);
            foreach (GameObject prefab in allPrefabs)
            {
                DynamicObject dynamicObject = prefab.GetComponent<DynamicObject>();
                if (dynamicObject == null)
                {
                    //log error then continue
                    Debug.LogError("Skipping Prefab: " + prefab.name + " does not have a DynamicObject component!");
                    
                    continue;
                }
                if (!dynamicObject.objectState.isPrefab)
                {
                    throw new InvalidOperationException("Prefab's ObjectState isPrefab = false");
                }
                prefabs.Add(dynamicObject.objectState.prefabGuid, prefab);
                Debug.Log(prefab.name);
            }

            Debug.Log("Loaded " + prefabs.Count + " saveable prefabs.");



            return prefabs;
        }
        public static void DoSave(string sceneName)
        {
            SavePlayer(SAVE_PLAYER_PATH);
            SaveDynamicObjects(SAVE_OBJECTS_PATH, sceneName);

            MenuController mc = GameObject.FindGameObjectWithTag("GameManager").GetComponentInChildren<MenuController>();
            CharacterStatus ps = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CharacterStatus>();
            mc.levelToLoad = ps.currentScene;
        }

        public static void DoLoad(string playerPath, string objPath, bool menuLoad, string sceneName)
        {
            if (playerPath == "") playerPath = SAVE_PLAYER_PATH;
            if (objPath == "") objPath = SAVE_OBJECTS_PATH;

            if (menuLoad)
            {
                LoadDynamicObjects(objPath, sceneName);
                LoadPlayer(playerPath);
            }
            else
            {
                LoadDynamicObjects(objPath, sceneName);
            }

        }
        public static GameObject GetPlayer()
        {
            return GameObject.FindGameObjectWithTag("Player");
        }

        private static GameObject GetRootDynamicObject()
        {
            foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("DynamicRoot"))
            {
                if (gameObject.activeSelf)
                {
                    return gameObject;
                }
            }
            throw new InvalidOperationException("Cannot find root of dynamic objects");
        }
        public static string GetSavedSceneName(string path)
        {
            if (File.Exists(path))
            {
                ObjectState objectState = ReadFile<ObjectState>(path);
                return objectState.genericValues["savedLevel"].ToString();
            }
            else
            {
                throw new InvalidOperationException("cannot find scene name!");
            }

        }
        public static void SavePlayer(string path)
        {
            GameObject player = GetPlayer();
            if (player == null)
            {
                throw new InvalidOperationException("Cannot find the Player");
            }
            //check if path exists if not create folder
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Debug.Log("Creating save folder");
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            path = Application.persistentDataPath + "/savegames/" + player.GetComponent<CharacterStatus>().name;

            path = IteratePath(path, "player", 1);


            DynamicObject dynamicObject = player.GetComponent<DynamicObject>();
            List<ObjectState> objectStates = dynamicObject.objectState.Save(player);

            if (objectStates.Count != 1)
            {
                throw new InvalidOperationException("Expected only 1 object state for the Player");
            }
            WriteFile(path, objectStates[0]);

            string json = JsonConvert.SerializeObject(objectStates[0], Formatting.Indented); File.WriteAllText(path + ".txt", json);

            Debug.Log("Saved Player to: " + path);
        }

        public static void LoadPlayer(string path)
        {
            if (File.Exists(path))
            {
                ObjectState objectState = ReadFile<ObjectState>(path);

                GameObject player = GetPlayer();
                if (player == null)
                {

                    throw new InvalidOperationException("Cannot find the Player...spawning clone");

                }

                DynamicObject dynamicObject = player.GetComponent<DynamicObject>();

                player.GetComponent<KineCharacterController>().Motor.SetPositionAndRotation(ConvertToVector3(objectState.position), ConvertToQuaternion(objectState.rotation));

                dynamicObject.Load(objectState);

                SAVE_PLAYER_PATH = path;
                Debug.Log("Loaded Player from: " + path);
            }
            else
            {

                GameObject noSave = UnityEngine.Object.FindObjectOfType<MenuController>().GetNoSaveDialog();
                noSave.SetActive(true);
                Debug.LogError("Save file not found in " + SAVE_PLAYER_PATH);
            }
        }

        private static void SaveDynamicObjects(string path, string sceneName)
        {
            GameObject player = GetPlayer();
            if (player == null)
            {
                throw new InvalidOperationException("Cannot find the Player");
            }
            path = Application.persistentDataPath + "/savegames/" + player.GetComponent<CharacterStatus>().name;

            path = IteratePath(path, "obj", 1);

            if (File.Exists(path))
            {
                List<ObjectState> objectStates = ReadFile<List<ObjectState>>(path);

                Debug.Log("save path in SaveDynamicObjects(): " + path);

                foreach (ObjectState objectState in objectStates.ToArray())
                {
                    if (objectState.sceneName == sceneName)
                    {
                        objectStates.Remove(objectState);
                    }
                }

                objectStates.AddRange(ObjectState.SaveObjects(GetRootDynamicObject()));
                string json = JsonConvert.SerializeObject(objectStates, Formatting.Indented); File.WriteAllText(path + ".txt", json);

                WriteFile(path, objectStates);
                Debug.Log("Updated objects in: " + path);
            }
            else
            {

                List<ObjectState> objectStates = ObjectState.SaveObjects(GetRootDynamicObject());

                string json = JsonConvert.SerializeObject(objectStates, Formatting.Indented); File.WriteAllText(path + ".txt", json);
                WriteFile(path, objectStates);
                Debug.Log("Saved objects to: " + path);
            }
        }

        private static void LoadDynamicObjects(string path, string sceneName)
        {
            SAVE_OBJECTS_PATH = path;

            List<ObjectState> objectStates = ReadFile<List<ObjectState>>(path);

            ObjectState.LoadObjects(allPrefabs, objectStates, GetRootDynamicObject(), sceneName);


            Debug.Log("Loaded objects from: " + path);
        }

        private static string IteratePath(string originalPath, string type, int i)
        {
            string path = originalPath;

            GameObject player = GetPlayer();

            if (player == null)
            {
                throw new InvalidOperationException("Cannot find the Player");
            }


            if (type == "obj")
            {
                if (File.Exists(originalPath + ".inso") && SAVE_OBJECTS_PATH != originalPath + ".inso")
                {
                    path = Application.persistentDataPath + "/savegames/" + player.GetComponent<CharacterStatus>().name + "[" + i + "]";
                    i += 1;
                    path = IteratePath(path, type, i);

                }
                else
                {
                    path += ".inso";
                }
                SAVE_OBJECTS_PATH = path;
                return path;

            }
            else if (type == "player")
            {
                if (File.Exists(originalPath + ".insp") && SAVE_PLAYER_PATH != originalPath + ".insp")
                {
                    path = Application.persistentDataPath + "/savegames/" + player.GetComponent<CharacterStatus>().name + "[" + i + "]";
                    i += 1;
                    path = IteratePath(path, type, i);
                }
                else
                {

                    path += ".insp";
                }

                SAVE_PLAYER_PATH = path;
                return path;

            }
            return null;
        }

        public static string[] GetSaves()
        {
            string[] saves = Directory.GetFiles(Application.persistentDataPath + "/savegames/");

            return saves;
        }
        private static void WriteFile<T>(string path, T obj)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, obj);
            stream.Close();
        }

        private static T ReadFile<T>(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            T objectState = (T)formatter.Deserialize(stream);

            stream.Close();

            return objectState;
        }

        public static float[] ConvertFromQuaternion(Quaternion quaternion)
        {
            float[] value = { quaternion.eulerAngles.x, quaternion.eulerAngles.y, quaternion.eulerAngles.z };

            return value;
        }
        public static Quaternion ConvertToQuaternion(float[] value)
        {
            return Quaternion.Euler(value[0], value[1], value[2]);
        }
        public static float[] ConvertFromVector3(Vector3 vector3)
        {
            float[] values = { vector3.x, vector3.y, vector3.z };

            return values;
        }

        public static Vector3 ConvertToVector3(float[] values)
        {
            return new Vector3(values[0], values[1], values[2]);
        }

        public static float[,] ConvertFromVector3Array(Vector3[] vector3)
        {
            if (vector3 == null)
            {
                return new float[0, 3];
            }

            float[,] values = new float[vector3.Length, 3];

            for (int i = 0; i < vector3.Length; i++)
            {
                values[i, 0] = vector3[i].x;
                values[i, 1] = vector3[i].y;
                values[i, 2] = vector3[i].z;
            }
            return values;
        }

        public static Vector3[] ConvertToVector3Array(float[,] array)
        {
            if (array.Length == 0)
            {
                return null;
            }

            Vector3[] vector3 = new Vector3[array.GetUpperBound(0) + 1];
            for (int i = 0; i < vector3.Length; i++)
            {
                vector3[i] = new Vector3(array[i, 0], array[i, 1], array[i, 2]);
            }
            return vector3;
        }
    }
}