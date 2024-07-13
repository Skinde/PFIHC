using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMenu : MonoBehaviour
{
    public OVRInput.Controller leftcontroller = OVRInput.Controller.None; // Controller to use (either Left or Right)
    public OVRInput.Controller rightcontroller = OVRInput.Controller.None; // Controller to use (either Left or Right)
    [SerializeField] public GameObject The_canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.IsControllerConnected(OVRInput.Controller.LTouch))
        {
            leftcontroller = OVRInput.Controller.LTouch; // Left controller
        }
        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTouch))
        {
            rightcontroller = OVRInput.Controller.RTouch; // Right controller
        }


        if (OVRInput.GetDown(OVRInput.Button.Two, rightcontroller))
        {
            Debug.Log("Felt Click");
            if (The_canvas.activeSelf)
            {
                The_canvas.SetActive(false);
                Debug.Log("Set active to false");
            }
            else { The_canvas.SetActive(true);
                Debug.Log("Set active to true");
            }

        }
    }
}
