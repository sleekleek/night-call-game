using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTopDownController : MonoBehaviour
{
    public Transform trackedObject;
	public Vector3 offset = new Vector3(0, 20, 0);

    public float updateSpeed = 45;

    // Start is called before the first frame update
    void Start()
    {

    }

    // LateUpdate() for smoother camera movements
    void LateUpdate() 
    {   
        // Position
	    transform.position = Vector3.MoveTowards(transform.position, trackedObject.position + offset, updateSpeed);
    }
}
