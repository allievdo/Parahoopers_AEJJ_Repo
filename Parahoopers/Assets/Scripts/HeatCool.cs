using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatCool : MonoBehaviour
{
    public PlayerController playerController;
    public int upDownForce; //use this when setting up course

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.tag == "Heat")
        {
            if (other.TryGetComponent<PlayerController>(out PlayerController player))
            {
                playerController.rb.AddForce(transform.up * 100);
                Debug.Log("went up");
            }
        }

        if (this.gameObject.tag == "Cool")
        {
            if (other.TryGetComponent<PlayerController>(out PlayerController player))
            {
                playerController.rb.AddForce(-transform.up * 100);
                Debug.Log("went down"); 
            }
        }
    }
}
