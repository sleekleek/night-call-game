using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Energy : MonoBehaviour
{
    private TextMeshProUGUI textEnergy;
    public bool isEnergyUpdated;
    public bool isSpeedNormal;
    public bool isPlayerFainted;
    public int energyLevel;
    GameObject clock;
    GameObject player;
    GameObject prompt;

    // Start is called before the first frame update
    void Start()
    {
        textEnergy = GetComponent<TextMeshProUGUI>();
        textEnergy.text = energyLevel.ToString() + "/100";
        clock = GameObject.FindGameObjectWithTag("Clock");
        player = GameObject.FindGameObjectWithTag("Player");
        prompt = GameObject.FindGameObjectWithTag("Prompt");
        isEnergyUpdated = true;
        isSpeedNormal = true;
        isPlayerFainted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (clock.GetComponent<Clock>().minusHealth) {
            if (energyLevel != 0)
            {
                energyLevel -= 1;
            }

            if (energyLevel <= 100 && energyLevel > 10 && isSpeedNormal)
            {
                player.GetComponent<PlayerController>().speed = player.GetComponent<PlayerController>().initialSpeed;
            }
            else if (energyLevel <= 10 && energyLevel >= 1 && isSpeedNormal)
            {
                player.GetComponent<PlayerController>().speed = player.GetComponent<PlayerController>().initialSpeed / 10;
                isSpeedNormal = false;
            }
            else if (energyLevel == 0 && !isPlayerFainted)
            {
                player.GetComponent<PlayerController>().speed = 0;
                prompt.GetComponent<Prompt>().promptText = "Player fainted!";
                prompt.GetComponent<Prompt>().isPromptUpdated = false;
                isPlayerFainted = true;
            }

            textEnergy.text = energyLevel.ToString() + "/100";
            clock.GetComponent<Clock>().minusHealth = false;
        }
        if (!isEnergyUpdated)
        {
            if (energyLevel > 100)
            {
                energyLevel = 100;
            }
            textEnergy.text = energyLevel.ToString() + "/100";
            isEnergyUpdated = true;
            isSpeedNormal = true;
            isPlayerFainted = false;
        }
    }
}
