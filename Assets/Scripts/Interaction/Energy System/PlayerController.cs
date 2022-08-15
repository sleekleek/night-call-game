using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    GameObject energy;
    public EnergyBar energyBar;
    // GameObject alertness;
    // public EnergyBar alertnessBar;
    GameObject prompt;
    public int initialSpeed;
    public int speed;
    private bool isDrank;
    Vector3 Vec;

    public float updateSpeed = 45;
    public float mouseSensitivity = 0.5f;
    public Vector2 turn;
    public Vector3 deltaMove;

    Animator animator;
    int isWalkingHash;
    KeyCode keyinput;

    // Start is called before the first frame update
    void Start()
    {
        energy = GameObject.FindGameObjectWithTag("EnergyLevel");
        energyBar.SetMaxEnergy(100);
        // alertness = GameObject.FindGameObjectWithTag("AlertnessLevel");
        // alertnessBar.SetMaxEnergy(400);
        prompt = GameObject.FindGameObjectWithTag("Prompt");
        isDrank = false;

        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        keyinput = KeyCode.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) 
        {
            transform.position += transform.forward * Time.deltaTime * speed;
            keyinput = KeyCode.W;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Time.deltaTime * speed;
            keyinput = KeyCode.S;
        }

        if (Input.GetKey(KeyCode.A)) 
        {
            transform.position -= transform.right * Time.deltaTime * speed;
            keyinput = KeyCode.A;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * Time.deltaTime * speed;
            keyinput = KeyCode.D;
        }

        bool isWalking = animator.GetBool(isWalkingHash);
        bool Pressed = Input.GetKey(keyinput); 

        if (!isWalking && Pressed)
        {
            animator.SetBool(isWalkingHash, true);

        }

        if (isWalking && !Pressed)
        {
            animator.SetBool(isWalkingHash, false);

        }
    }

    void LateUpdate() 
    {
        // Rotation
        turn.x += Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Vending")
        {
            prompt.GetComponent<Prompt>().promptText = "Press E to snack!";
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
        if (Input.GetKey(KeyCode.E) && other.tag == "Vending" && !isDrank)
        {
            prompt.GetComponent<Prompt>().promptText = "Snack eaten! \nGained 40 Energy!";
            prompt.GetComponent<Prompt>().isPromptUpdated = false;

            if (energy.GetComponent<Energy>().energyLevel + 40 <= 100) 
            {
                energy.GetComponent<Energy>().energyLevel += 40;
                energyBar.SetEnergy(energy.GetComponent<Energy>().energyLevel);
            } else {
                energy.GetComponent<Energy>().energyLevel = 100;
                energyBar.SetEnergy(100);
            }

            energy.GetComponent<Energy>().isEnergyUpdated = false;
            isDrank = true;
        }
        if (Input.GetKey(KeyCode.E) && other.tag == "Bed")
        {
            prompt.GetComponent<Prompt>().promptText = "Resting! \nHold down 'E' to regain Alertness!";
            prompt.GetComponent<Prompt>().isPromptUpdated = false;
            energy.GetComponent<Energy>().alertnessLevel += 1;
            energy.GetComponent<Energy>().isAlertnessUpdated = false;
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
