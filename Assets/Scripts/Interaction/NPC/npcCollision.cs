using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcCollision : MonoBehaviour
{
    public GameObject canvas;
    private Dictionary<string, string> textDatabase = new Dictionary<string, string>();
    private Dictionary<string, string> textHBPDatabase = new Dictionary<string, string>();
    private Dictionary<string, string> textPneumoniaDatabase = new Dictionary<string, string>();
    private Dictionary<int, string> helpHBP = new Dictionary<int, string>();
    private Dictionary<int, string> helpPneumonia = new Dictionary<int, string>();
    private bool keyStart; // history
    private bool keyOptionOne; // history
    private bool keyOptionTwo; // condition
    private bool keyOptionThree; // treatment choice
    private bool keyOptionFour; // treatment 1
    private bool keyOptionFive; // treatment 2
    private bool keyOptionSix; // treatment 3
    private bool keyHelp; // help
    private bool keySwitch; // switch status
    private int helpCount = 2;
    private string patientStatus = "High Blood Pressure";

    // Start is called before the first frame update
    void Start()
    {
        canvas.gameObject.SetActive(false);
        // canvas.transform.GetChild(0).gameObject.SetActive(true);
        // canvas.transform.GetChild(1).gameObject.SetActive(false);

        // Set up general text database
        textDatabase.Add("initial", "Press Spacebar to start chat");
        textDatabase.Add("start", "Hello. How would you like to treat the patient? 1. Check history 2. Patient condition 3. Treat patient H. Help C. Change patient");
        textDatabase.Add("history", "Checking patient history...");
        textDatabase.Add("treatmentChoice", "What treatment would you like to administer? 4. Nifedipine 5. Oxygen 6. Fluid");
        textDatabase.Add("drugNifedipine60mg", "Administering Nifedipine 60mg");
        textDatabase.Add("oxygen3l", "Administering oxygen 3 litres");
        textDatabase.Add("fluid250ml", "Administering fluid 250ml");
        
        // Set up High Blood Pressure text database
        textHBPDatabase.Add("condition", "Patient has cold sweat");
        helpHBP.Add(0, "No hints left!");
        helpHBP.Add(2, "Hint 1: Patient may have high blood pressure");
        helpHBP.Add(1, "Hint 2: Administer Nifedipine 60mg");

        // Set up Pneumonia text database
        textPneumoniaDatabase.Add("condition", "Patient has breathlessness");
        helpPneumonia.Add(0, "No hints left!");
        helpPneumonia.Add(2, "Hint 1: Patient may have pneumonia");
        helpPneumonia.Add(1, "Hint 2: Administer Oxygen 3 litres and fluid 250ml");
    }

    // Update is called once per frame
    void Update()
    {   
        // Set up input keys
        keyStart = Input.GetKeyDown(KeyCode.Space); // start chat
        keyOptionOne = Input.GetKeyDown(KeyCode.Alpha1); // history
        keyOptionTwo = Input.GetKeyDown(KeyCode.Alpha2); // condition
        keyOptionThree = Input.GetKeyDown(KeyCode.Alpha3); // treatment choice
        keyOptionFour = Input.GetKeyDown(KeyCode.Alpha4); // treatment 1
        keyOptionFive = Input.GetKeyDown(KeyCode.Alpha5); // treatment 2
        keyOptionSix = Input.GetKeyDown(KeyCode.Alpha6); // treatment 3
        keyHelp = Input.GetKeyDown(KeyCode.H); // help
        keySwitch = Input.GetKeyDown(KeyCode.C); // switch status

        ShowTextOption(keyStart, keyOptionOne, keyOptionTwo, keyOptionThree, keyOptionFour, keyOptionFive, keyOptionSix, keyHelp, keySwitch);
    }

    private void OnTriggerEnter(Collider other) {
            canvas.gameObject.SetActive(true);
            canvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = textDatabase["initial"];
            // if (other.CompareTag("Doctor")) {
            //     Debug.Log("Open chat");
            //     canvas.gameObject.SetActive(true);
            // }
        }

    private void OnTriggerExit(Collider other) {
            canvas.gameObject.SetActive(false);
            // if (other.CompareTag("Doctor")) {
            //     canvas.gameObject.SetActive(false);
            //     // canvas.transform.GetChild(0).gameObject.SetActive(true);
            //     // canvas.transform.GetChild(1).gameObject.SetActive(false);
            // }
        }
    
    private void ShowTextOption(bool keyStart, bool keyOptionOne, bool keyOptionTwo, bool keyOptionThree, bool keyOptionFour, bool keyOptionFive, bool keyOptionSix, bool keyHelp, bool keySwitch) {
            // Cases for text to appear
            if (keyStart)
                {
                    canvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = textDatabase["start"];
                }
            if (keyOptionOne)
                {
                    canvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = textDatabase["history"];
                }
            if (keyOptionThree)
                {
                    canvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = textDatabase["treatmentChoice"];
                }
            if (keyOptionFour)
                {
                    canvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = textDatabase["drugNifedipine60mg"];
                }
            if (keyOptionFive)
                {
                    canvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = textDatabase["oxygen3l"];
                }
            if (keyOptionSix)
                {
                    canvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = textDatabase["fluid250ml"];
                }
            if (keySwitch)
                {
                    helpCount = 2;
                    if (patientStatus == "High Blood Pressure") {
                        patientStatus = "Pneumonia";
                        canvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = "Switched to pneumonia case";
                    }
                    else if (patientStatus == "Pneumonia") {
                        patientStatus = "High Blood Pressure";
                        canvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = "Switched to high blood pressure case";
                    }
                }

            if (patientStatus == "High Blood Pressure") {
                if (keyOptionTwo)
                    {
                        canvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = textHBPDatabase["condition"];
                    }
                if (keyHelp)
                    {
                        canvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = helpHBP[helpCount];
                        if (helpCount > 0) {
                            helpCount -= 1;
                        }
                    }
            }

            if (patientStatus == "Pneumonia") {
                if (keyOptionTwo)
                    {
                        canvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = textPneumoniaDatabase["condition"];
                    }
                if (keyHelp)
                    {
                        canvas.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = helpPneumonia[helpCount];
                        if (helpCount > 0) {
                            helpCount -= 1;
                        }
                    }
            }
    }
}
