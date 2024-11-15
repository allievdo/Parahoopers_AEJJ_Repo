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
        slider.minValue = bottom.position.y;
        slider.maxValue = top.position.y;
    }

    private void Update()
    {
        slider.value = player.position.y;
    }
}
