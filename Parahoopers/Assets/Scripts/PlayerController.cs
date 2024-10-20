using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private float speed = 3.5f;
    private float force = 2;
    private float gravity = -0.5f;

    private float torque = 0.05f;
    private float torqueOp = -0.05f;

    private float spinForce = 0.3f;

    public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //Always gives a constant forward speed
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        //Always applies a downward force (Represents gravity)
        transform.Translate(Vector3.up * gravity * Time.deltaTime);

        //Adds force to the left
        if(Input.GetKey(KeyCode.A))
        {
            //rb.AddForce(-transform.right * force);
            transform.Rotate(Vector3.forward * torque);
            transform.Rotate(Vector3.down * spinForce);
            //rb.AddTorque(transform.forward * torque);
        }

        //Adds force to the right
        if (Input.GetKey(KeyCode.D))
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
}
