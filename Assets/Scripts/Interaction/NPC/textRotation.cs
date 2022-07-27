using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textRotation : MonoBehaviour
{
    public Transform camera1;
    public Transform camera2;
    void Update()
    {
        if (Display.activeEditorGameViewTarget == 0) {
            transform.rotation =camera1.rotation;
        }
        else if (Display.activeEditorGameViewTarget == 1) {
            transform.rotation =camera2.rotation;
        }
        
    }
}
