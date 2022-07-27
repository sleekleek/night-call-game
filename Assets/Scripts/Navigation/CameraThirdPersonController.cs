using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraThirdPersonController : MonoBehaviour
{
    public Transform trackedObject;
	public Vector3 offset = new Vector3(3, 2, 0);
    
    public float updateSpeed = 45;
    public float mouseSensitivity = 0.5f;

    public Vector2 turn;
    public Vector3 deltaMove;

    // Start is called before the first frame update
    void Start()
    {
        // Lock cursor in the middle and make invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

    // LateUpdate() for smoother camera movements
    void LateUpdate() 
    {   
        // Position
	    transform.position = Vector3.MoveTowards(transform.position, trackedObject.position + offset, updateSpeed);
        
        // Rotation
        turn.x += Input.GetAxis("Mouse X") * mouseSensitivity;
        turn.y += Input.GetAxis("Mouse Y") * mouseSensitivity;
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }
}
