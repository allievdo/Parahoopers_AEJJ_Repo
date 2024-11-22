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
    [SerializeField] private float inputDelta = 0.1f;
    [SerializeField] private float deadzone = 0.2f;

    private bool turningLeft = false; //Temporary for now
    private bool turningRight = false; //Temporary for now

    private float speed = 3.5f;

    private float force = 2;
    private float gravity = -0.25f;

    private float torque = 0.05f;
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

    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.up);
    }

    private void Update()
    {
        ProcessArduinoInput();

        //Always gives a constant forward speed
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        //Always applies a downward force (Represents gravity)
        //transform.Translate(Vector3.up * gravity * Time.deltaTime);
        rb.AddForce(Vector3.up * gravity * Time.deltaTime, ForceMode.Impulse);

        if (Input.GetKey(KeyCode.A))
        {
            turningLeft = true;
            processedInput = 1;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            turningLeft = false;
            processedInput = 0;
        }
        
        
        if (Input.GetKey(KeyCode.D)) 
        {
            turningRight = true;
            processedInput = -1;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            turningRight = false;
            processedInput = 0;
        }

        //Adds force to the 
        if (turningLeft)
        {
            //rb.AddForce(-transform.right * force);
            transform.Rotate(Vector3.forward * torque * processedInput);
            transform.Rotate(Vector3.down * spinForce * processedInput);
            //rb.AddTorque(transform.forward * torque);
        }

        //Adds force to the right
        if (turningRight)
        {
            //rb.AddForce(transform.right * force);
            transform.Rotate(Vector3.forward * torque * processedInput);
            transform.Rotate(Vector3.up * -spinForce * processedInput);
            //rb.AddTorque(-transform.forward * torque);
        }

        //FOR TESTING
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.up * force);
            transform.Rotate(Vector3.right * torque * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.up * force);
            transform.Rotate(Vector3.left * torque * Time.deltaTime);
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
            bool isRight = arduinoInput > lastInput || (lastInput > 200 && difference > 200);


            if (isRight)
            {
                processedInput -= inputDelta; //10 needs to be changed to an adjustable variable
            }
            else if (isLeft)
            {
                processedInput += inputDelta;// 10 here too
            }
            processedInput = Mathf.Clamp(processedInput, -1f, 1f);
        }
        if (processedInput > deadzone) turningRight = true;
        else if (processedInput < -deadzone) turningLeft = true;
        // Set the last input equal to current input.
        lastInput = arduinoInput;
    }

    //Sets the current value read from the arduino.
    private void SetArduinoValue(int value)
    {
        arduinoInput = value;
    }
}