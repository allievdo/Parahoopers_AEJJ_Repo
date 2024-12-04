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

    public float startTime;
    [SerializeField] private float timeTaken;
    [SerializeField] private float inputDelta = 0.1f;
    [SerializeField] private float deadzone = 0.2f;

    private bool turningLeft = false; //Temporary for now
    private bool turningRight = false; //Temporary for now
    public bool isPlaying = false;
    public bool hasEnded = false;

    private float speed = 3.5f;

    private float force = 2;
    private float gravity = -0.25f;

    [SerializeField] private float torque = 0.05f;
    [SerializeField] private float torqueOp = -0.05f;
    [SerializeField] private float spinForce = 0.3f;

    public Rigidbody rb;

    public HeatCool heatCool;

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject speedometer;
    [SerializeField] private GameObject altitude;

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
        if (!isPlaying || PauseMenu.GameIsPaused)
            return;
        else
        {
            ProcessArduinoInput();

            isPlaying = true;

            if (hasEnded == true)
            {
                End();
                Debug.Log("Game has ended");
            }

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

            if (turningLeft)
            {
                //rb.AddForce(-transform.right * force);
                //transform.Rotate(Vector3.forward * torque * processedInput * 1000f * Time.deltaTime);
                transform.Rotate(Vector3.down * spinForce * processedInput * 1000f * Time.deltaTime);
                //rb.AddTorque(transform.forward * torque);
            }

            //Adds force to the right
            if (turningRight)
            {
                //rb.AddForce(transform.right * force);
                //transform.Rotate(Vector3.forward * torqueOp * processedInput * 1000f * Time.deltaTime);
                transform.Rotate(Vector3.up * -spinForce * processedInput * 1000f * Time.deltaTime);
                //rb.AddTorque(-transform.forward * torque);
            }

            //FOR TESTING
            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(transform.up * force);
                transform.Rotate(Vector3.right * torque * Time.deltaTime);
            }
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

    void End()
    {
        timeTaken = Time.time - startTime;
        Debug.Log(timeTaken);
        isPlaying = false;
        Leaderboard.instance.SetLeaderboardEntry(-Mathf.RoundToInt(timeTaken * 1000.0f));
    }

    IEnumerator SpeedCooldown()
    {
        yield return new WaitForSeconds(0.8f);
        transform.Translate(Vector3.forward * -3f * Time.deltaTime);
    }
}