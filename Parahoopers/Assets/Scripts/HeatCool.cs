using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatCool : MonoBehaviour
{
    public PlayerController playerController;
    public int upDownForce; //use this when setting up course

    public GameObject coolPanel;
    public GameObject heatPanel;

    [SerializeField] private bool isActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.tag == "Heat")
        {
            if (other.TryGetComponent<PlayerController>(out PlayerController player))
            {
                playerController.rb.AddForce(transform.up * 100);
                //Debug.Log("went up");

                if(isActive == false)
                {
                    isActive = true;
                    heatPanel.SetActive(true);

                    StartCoroutine(PanelCooldown());
                }
            }
        }

        if (this.gameObject.tag == "Cool")
        {
            if (other.TryGetComponent<PlayerController>(out PlayerController player))
            {
                playerController.rb.AddForce(-transform.up * 100);
                //Debug.Log("went down"); 
                if (isActive == false)
                {
                    isActive = true;
                    coolPanel.SetActive(true);

                    StartCoroutine(PanelCooldown());
                }
            }
        }
    }

    IEnumerator PanelCooldown()
    {
        yield return new WaitForSeconds(0.8f);
        isActive = false;
        heatPanel.SetActive(false);
        coolPanel.SetActive(false);
    }
}
