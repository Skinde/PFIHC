using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using Oculus.Platform.Models;

public class ScriptedMovement : MonoBehaviour
{
    public float moveSpeed = 3f; // Adjust this to control movement speed
    public float rotationSpeed = 90f; // Adjust this to control rotation speed
    public float scaleSpeed = 1f; // Adjust this to control scaling speed
    public float minScale = 0.1f; // Minimum scale factor
    public float maxScale = 4f; // Maximum scale factor
    public OVRInput.Controller leftcontroller = OVRInput.Controller.None; // Controller to use (either Left or Right)
    public OVRInput.Controller rightcontroller = OVRInput.Controller.None; // Controller to use (either Left or Right)
    void Start()
    {
        // Automatically determine the controller to use based on whether Oculus Quest 2 is connected
        if (OVRInput.IsControllerConnected(OVRInput.Controller.LTouch))
        {
            leftcontroller = OVRInput.Controller.LTouch; // Left controller
        }
        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTouch))
        {
            rightcontroller = OVRInput.Controller.RTouch; // Right controller
        }
        else
        {
            Debug.LogWarning("Oculus Touch controllers not detected.");
        }

    }


    // Update is called once per frame
    void Update()
    {

        //Movimiento
        Vector2 joystickInput = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
        Vector3 moveDirection = new Vector3(joystickInput.x, 0f, joystickInput.y);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        transform.parent.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        //Rotacion
        Vector2 joystickRotateInput = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick); // Assuming SecondaryThumbstick for rotation
        float rotateAmount = joystickRotateInput.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up, rotateAmount);
        transform.parent.transform.Rotate(Vector3.up, rotateAmount);

        // Scaling
        if (OVRInput.GetDown(OVRInput.Button.One, leftcontroller)) // Button A on left controller
        {
            ScaleObject(2f);
        }
        else if (OVRInput.GetDown(OVRInput.Button.Two, leftcontroller)) // Button B on left controller
        {
            ScaleObject(0.5f);
        }

        //Deleting
        if (OVRInput.GetDown(OVRInput.Button.One, rightcontroller))
        {
            Destroy(gameObject);
        }


    }

    void ScaleObject(float scaleFactor)
    {
        // Limit scaling within specified range
        Vector3 newScale = transform.localScale * scaleFactor;
        newScale.x = Mathf.Clamp(newScale.x, minScale, maxScale);
        newScale.y = Mathf.Clamp(newScale.y, minScale, maxScale);
        newScale.z = Mathf.Clamp(newScale.z, minScale, maxScale);

        // Apply the new scale
        transform.localScale = newScale;
    }

}
