using System;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [Header ("Player Stats")]
    [SerializeField] int level = 1;
    [SerializeField] int health = 50;
    [SerializeField ]public new string name = "Bob";

    [Header ("Player Info")]
    [SerializeField] string currentScene;

    private void Start()
    {
        DynamicObject dynamicObject = GetComponent<DynamicObject>();
        dynamicObject.prepareToSaveDelegates += PrepareToSaveObjectState;
        dynamicObject.loadObjectStateDelegates += LoadObjectState;
    }

    private void Update()
    {
        currentScene = gameObject.scene.name;
    }

    private void PrepareToSaveObjectState(ObjectState objectState)
    {
        objectState.genericValues["PlayerStats.level"] = level;
        objectState.genericValues["PlayerStats.health"] = health;
        objectState.genericValues["PlayerStats.name"] = name;
        objectState.genericValues["SavedLevel"] = currentScene;
    }
    private void LoadObjectState(ObjectState objectState)
    {
        // Load the player's position & rotation
        transform.position = SaveUtils.ConvertToVector3(objectState.position);
        transform.rotation = SaveUtils.ConvertToQuaternion(objectState.rotation);
        // Load the reference to the stats
        level = Convert.ToInt32 (objectState.genericValues["PlayerStats.level"]);
        health = Convert.ToInt32(objectState.genericValues["PlayerStats.health"]);
        name = Convert.ToString(objectState.genericValues["PlayerStats.name"]);


    }
}


