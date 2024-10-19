using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Renderer ren;

    void Start()
    {
        //ren.GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("player collided");
            ren.material.color = Color.green;
        }
    }
}
