using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Platform;
using Oculus.Platform.Models;

public class ScriptedMovement : MonoBehaviour
{
    private Vector3 starting_scale = new Vector3(1, 1, 1);
    public float moveSpeed = 3f; // Adjust this to control movement speed
    public float rotationSpeed = 90f; // Adjust this to control rotation speed
    public float scaleSpeed = 1f; // Adjust this to control scaling speed
    public float minScale = 0.0625f; // Minimum scale factor
    public float maxScale = 8f; // Maximum scale factor
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
        starting_scale = starting_scale * scaleFactor;
        starting_scale.x = Mathf.Clamp(starting_scale.x, minScale, maxScale);
        starting_scale.y = Mathf.Clamp(starting_scale.y, minScale, maxScale);
        starting_scale.z = Mathf.Clamp(starting_scale.z, minScale, maxScale);

        // Apply the new scale
        transform.localScale = starting_scale;
    }

}
