using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject energy;
    GameObject prompt;
    public int initialSpeed;
    public int speed;
    private bool isDrank;
    Vector3 Vec;

    // Start is called before the first frame update
    void Start()
    {
        energy = GameObject.FindGameObjectWithTag("EnergyLevel");
        prompt = GameObject.FindGameObjectWithTag("Prompt");
        isDrank = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vec = transform.localPosition;
        Vec.x += Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        Vec.z += Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.localPosition = Vec;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Food")
        {
            prompt.GetComponent<Prompt>().promptText = "Press E to eat food";
            prompt.GetComponent<Prompt>().isPromptUpdated = false;
        }
        if (other.tag == "Vending")
        {
            prompt.GetComponent<Prompt>().promptText = "Press E to buy and drink beverage!";
            prompt.GetComponent<Prompt>().isPromptUpdated = false;
        }
        if (other.tag == "Bed")
        {
            prompt.GetComponent<Prompt>().promptText = "Press E to rest!";
            prompt.GetComponent<Prompt>().isPromptUpdated = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E) && other.tag == "Food")
        {
            prompt.GetComponent<Prompt>().promptText = "Food eaten! Gain 10 Energy!";
            prompt.GetComponent<Prompt>().isPromptUpdated = false;
            energy.GetComponent<Energy>().energyLevel += 10;
            energy.GetComponent<Energy>().isEnergyUpdated = false;
            Destroy(other.gameObject);
        }
        if (Input.GetKey(KeyCode.E) && other.tag == "Vending" && !isDrank)
        {
            prompt.GetComponent<Prompt>().promptText = "Drank Beverage! Gain 5 Energy!";
            prompt.GetComponent<Prompt>().isPromptUpdated = false;
            energy.GetComponent<Energy>().energyLevel += 5;
            energy.GetComponent<Energy>().isEnergyUpdated = false;
            isDrank = true;
        }
        if (Input.GetKey(KeyCode.E) && other.tag == "Bed")
        {
            prompt.GetComponent<Prompt>().promptText = "Resting. Hold down 'E' to regain more health!";
            prompt.GetComponent<Prompt>().isPromptUpdated = false;
            energy.GetComponent<Energy>().energyLevel += 1;
            energy.GetComponent<Energy>().isEnergyUpdated = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Vending")
        {
            isDrank = false;
        }
    }
}
