using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private int arduinoInput = -9999;
    private int lastInput = -9999;
    private float processedInput;
    private float deadzone = 10f;

    private bool turningLeft = false; //Temporary for now
    private bool turningRight = false; //Temporary for now

    private float speed = 3.5f;

    private float force = 2;
    private float gravity = -0.25f;

    private float torque = 0.05f;
    private float torqueOp = -0.05f;
    private float spinForce = 0.3f;

    public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        //Subscribe to the arduino's input.
        ArduinoInput.InputRecieved += SetArduinoValue;
    }

    private void OnDisable()
    {
        //Unsubscribe from the arduino's input when disabled.
        ArduinoInput.InputRecieved -= SetArduinoValue;
    }

    private void Update()
    {
        ProcessArduinoInput();

        //Always gives a constant forward speed
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        //Always applies a downward force (Represents gravity)
        //transform.Translate(Vector3.up * gravity * Time.deltaTime);
        rb.AddForce(Vector3.up * gravity * Time.deltaTime, ForceMode.Impulse);

        //Adds force to the left
        if(Input.GetKey(KeyCode.A) || turningLeft)
        {
            //rb.AddForce(-transform.right * force);
            transform.Rotate(Vector3.forward * torque);
            transform.Rotate(Vector3.down * spinForce);
            //rb.AddTorque(transform.forward * torque);
        }

        //Adds force to the right
        if (Input.GetKey(KeyCode.D) || turningRight)
        {
            //rb.AddForce(transform.right * force);
            transform.Rotate(Vector3.forward * torqueOp);
            transform.Rotate(Vector3.up * spinForce);
            //rb.AddTorque(-transform.forward * torque);
        }

        //FOR TESTING
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.up * force);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.up * force);
        }
    }

    private void ProcessArduinoInput()
    {
        if (lastInput == -9999 && arduinoInput != -9999)
        {
            // Sets the last input equal to arduino input if it has never been set before.
            lastInput = arduinoInput;
        }

        //Clears our temp variables
        turningLeft = false;
        turningRight = false;

        float difference = MathF.Abs(lastInput - arduinoInput);

        if (difference != 0)
        {
            bool isLeft = arduinoInput < lastInput || (lastInput < 32 && difference > 200);
            //bool isRight = arduinoInput > lastInput || (lastInput > 200 && difference > 200);

            if (isLeft)
            {
                processedInput -= 10; //10 needs to be changed to an adjustable variable
            }
            else
            {
                processedInput += 10;// 10 here too
            }
            if (processedInput > deadzone) turningRight = true;
            else if (processedInput < -deadzone) turningLeft = true;
        }

        // Set the last input equal to current input.
        lastInput = arduinoInput;
    }

    //Sets the current value read from the arduino.
    private void SetArduinoValue (int value)
    {
        arduinoInput = value;
    }
}
