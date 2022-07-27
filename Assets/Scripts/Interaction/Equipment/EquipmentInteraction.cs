using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EquipmentInteraction : MonoBehaviour
{
    //Array of images to cater for different patient conditions, i.e. healthy / abnormal readings
    public Image[] PatientMonitorScreen;
    public GameObject[] medicationVials;
    public Button closeButtonX;
    public Text instructions;
    public Animator anim;
    private Image activeImage;
    private Vector3 cameraPos;
    private GameObject target;
    private Button fluidAmt1, fluidAmt2, fluidAmt3, fluidAmt4;
    private string selectedObj;
    public string ongoingTask;
    private int fluidAmtSelected;

    //list of tasks/states, this is to enable player to have different interactions with the same equipment depending on the different states
    //  [IVbagInjection, FinishedSelectingVial]


    void Start()
    {
        fluidAmt1 = GameObject.Find("250mL").GetComponent<Button>();
        fluidAmt2 = GameObject.Find("750mL").GetComponent<Button>();
        fluidAmt3 = GameObject.Find("1500mL").GetComponent<Button>();
        fluidAmt4 = GameObject.Find("2500mL").GetComponent<Button>();

        fluidAmtButtonSetInactive();

        fluidAmt1.onClick.AddListener(fluidAmt1Clicked);
        fluidAmt2.onClick.AddListener(fluidAmt2Clicked);
        fluidAmt3.onClick.AddListener(fluidAmt3Clicked);
        fluidAmt4.onClick.AddListener(fluidAmt4Clicked);


        foreach (Image i in PatientMonitorScreen)
        {
            i.enabled = false;
        }

        activeImage = null;

        instructions.enabled = false;

        anim = GameObject.Find("Syringe").GetComponent<Animator>();

        closeButtonX.gameObject.SetActive(false);
        closeButtonX.onClick.AddListener(closeCanvas);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.gameObject.CompareTag("PatientMonitor")) { showPatientMonitorReadings(); }

                if (hit.transform.gameObject.CompareTag("IV Stand"))
                {
                    IVbagInjection();
                }

                if (hit.transform.gameObject.CompareTag("Medication Vial") && ongoingTask == "IVbagInjection")
                {
                    selectedObj = hit.transform.gameObject.name;
                    IVbagInjectionCont(selectedObj);
                }

                if (hit.transform.gameObject.name == "Syringe" && ongoingTask == "FinishedSelectingVial")
                {
                    anim.Play("SyringeAnim");
                    //Camera.main.transform.position = cameraPos;
                }

                Debug.Log(hit.transform.gameObject.name);
            }
        }
    }

    void showPatientMonitorReadings()
    {
        PatientMonitorScreen[0].enabled = true;
        activeImage = PatientMonitorScreen[0];
        closeButtonX.gameObject.SetActive(true);
    }

    void closeCanvas()
    {
        if (activeImage != null)
        {
            closeButtonX.gameObject.SetActive(false);
            activeImage.enabled = false;
            activeImage = null;
        }

        fluidAmtButtonSetInactive();

    }

    void IVbagInjection()
    {
        //this is to transition to the scene where you choose medication
        /*
        cameraPos = Camera.main.transform.position;
        target = GameObject.Find("MedicationVials");
        changeCamPos(target);

        instructions.text = "Choose the medication to inject into the IV drip bag.";
        instructions.enabled = true;

        ongoingTask = "IVbagInjection";
        */
        
        
        

        //this is to display buttons for the amt of fluid to administer
        fluidAmtButtonSetActive();
        closeButtonX.gameObject.SetActive(true);

        
        
        

    }

    void IVbagInjectionCont(string selectedObj)
    {
        GameObject currentVial;

        //remove all vials that were not selected, shift selected vial to center of table animation

        if (selectedObj == "Medication Vial 1")
        {
            currentVial = medicationVials[0];

            foreach (GameObject v in medicationVials[1..])
            {
                v.SetActive(false);
            }
            StartCoroutine(MoveFunction(currentVial));
        }

        if (selectedObj == "Medication Vial 2")
        {
            currentVial = medicationVials[1];

            medicationVials[0].SetActive(false);
            medicationVials[2].SetActive(false);
            medicationVials[3].SetActive(false);
            StartCoroutine(MoveFunction(currentVial));
        }

        if (selectedObj == "Medication Vial 3")
        {
            currentVial = medicationVials[2];

            medicationVials[0].SetActive(false);
            medicationVials[1].SetActive(false);
            medicationVials[3].SetActive(false);
            StartCoroutine(MoveFunction(currentVial));
        }

        if (selectedObj == "Medication Vial 4")
        {
            currentVial = medicationVials[3];

            foreach (GameObject v in medicationVials[..3])
            {
                v.SetActive(false);
            }
            StartCoroutine(MoveFunction(currentVial));
        }

        ongoingTask = "FinishedSelectingVial";
    }

    IEnumerator MoveFunction(GameObject currentVial)
    {
        while (true)
        {
            Vector3 startPos = currentVial.transform.position;
            Vector3 newPos = target.transform.position + new Vector3(0, -0.042f, 0.075f);

            currentVial.transform.position = Vector3.Lerp(startPos, newPos, 2 * Time.deltaTime);
            Debug.Log("I am enumerating");
            Debug.Log("Current pos: " + currentVial.transform.position + ", New pos: " + newPos);

            // If the object has arrived, stop the coroutine
            if (currentVial.transform.position == newPos)
            {
                yield break;
            }

            // Otherwise, continue next frame
            yield return null;
        }
    }

    //to "switch scene"
    void changeCamPos(GameObject target)
    {
        Camera.main.transform.position = target.transform.position + new Vector3(0.8f, 0.3f, 0);
        Camera.main.transform.LookAt(target.transform);
    }

    void fluidAmtButtonSetInactive()
    {
        fluidAmt1.gameObject.SetActive(false);
        fluidAmt2.gameObject.SetActive(false);
        fluidAmt3.gameObject.SetActive(false);
        fluidAmt4.gameObject.SetActive(false);
    }

    void fluidAmtButtonSetActive()
    {
        fluidAmt1.gameObject.SetActive(true);
        fluidAmt2.gameObject.SetActive(true);
        fluidAmt3.gameObject.SetActive(true);
        fluidAmt4.gameObject.SetActive(true);
    }

    //the amount of fluid selected for the IV drip bag

    void fluidAmt1Clicked()
    {
        fluidAmtSelected = 250;
        fluidAmtButtonSetInactive();
        closeButtonX.gameObject.SetActive(false);
    }

    void fluidAmt2Clicked()
    {
        fluidAmtSelected = 750;
        fluidAmtButtonSetInactive();
        closeButtonX.gameObject.SetActive(false);
    }

    void fluidAmt3Clicked()
    {
        fluidAmtSelected = 1500;
        fluidAmtButtonSetInactive();
        closeButtonX.gameObject.SetActive(false);
    }

    void fluidAmt4Clicked()
    {
        fluidAmtSelected = 2500;
        fluidAmtButtonSetInactive();
        closeButtonX.gameObject.SetActive(false);
    }
}