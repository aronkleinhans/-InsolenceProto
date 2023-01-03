using System;
using UnityEngine;

namespace Insolence.core
{
    public class CharacterState : MonoBehaviour
    {
        [Header("Player Stats")]
        [SerializeField] public int level = 1;
        [SerializeField] public int health = 50;
        [SerializeField] public new string name = "Bob";


        [SerializeField] public string currentScene;

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
            objectState.genericValues[name + "Stats.level"] = level;
            objectState.genericValues[name + "Stats.health"] = health;
            objectState.genericValues[name + "Stats.name"] = name;
            objectState.genericValues["savedLevel"] = currentScene;
        }
        private void LoadObjectState(ObjectState objectState)
        {
            // Load the player's position & rotation
            transform.position = SaveUtils.ConvertToVector3(objectState.position);
            transform.rotation = SaveUtils.ConvertToQuaternion(objectState.rotation);
            // Load the reference to the stats
            level = Convert.ToInt32(objectState.genericValues[name + "Stats.level"]);
            health = Convert.ToInt32(objectState.genericValues[name + "Stats.health"]);
            name = Convert.ToString(objectState.genericValues[name + "Stats.name"]);
            currentScene = Convert.ToString(objectState.genericValues["savedLevel"]);

        }
    }
}