using System;
using UnityEngine;

namespace Insolence.core
{
    public class CharacterState : MonoBehaviour
    {
        [Header("Player Stats")]
        [SerializeField] public int level = 1;
        [SerializeField] public int maxHealth = 50;
        [SerializeField] public int currentHealth = 50;
        [SerializeField] public int maxStamina = 100; //also used as energy for labor
        [SerializeField] public int currentStamina = 100;
        [SerializeField] public int hunger = 0;
        [SerializeField] public int money = 0;
        [SerializeField] public new string name = "";


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
            objectState.genericValues[name + "Stats.health"] = maxHealth;
            objectState.genericValues[name + "Stats.currentHealth"] = currentHealth;
            objectState.genericValues[name + "Stats.maxStamina"] = maxStamina;
            objectState.genericValues[name + "Stats.currentStamina"] = currentStamina;
            objectState.genericValues[name + "Stats.hunger"] = hunger;
            objectState.genericValues[name + "Stats.money"] = money;
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
            maxHealth = Convert.ToInt32(objectState.genericValues[name + "Stats.health"]);
            currentHealth = Convert.ToInt32(objectState.genericValues[name + "Stats.currentHealth"]);
            maxStamina = Convert.ToInt32(objectState.genericValues[name + "Stats.maxStamina"]);
            currentStamina = Convert.ToInt32(objectState.genericValues[name + "Stats.currentStamina"]);
            hunger = Convert.ToInt32(objectState.genericValues[name + "Stats.hunger"]);
            money = Convert.ToInt32(objectState.genericValues[name + "Stats.money"]);
            name = Convert.ToString(objectState.genericValues[name + "Stats.name"]);
            currentScene = Convert.ToString(objectState.genericValues["savedLevel"]);

        }
    }
}