using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Altimeter : MonoBehaviour
{
    public Transform player;
    public Transform top;
    public Transform bottom;

    public Slider slider;

    private void Start()
    {
        
    }

    private void Update()
    {
        float maxHeight = top.position.y;
        float minHeight = bottom.position.y;

        float difference = maxHeight - minHeight;

        slider.value = Mathf.Clamp01((player.position.y - minHeight) / difference);
    }
}
