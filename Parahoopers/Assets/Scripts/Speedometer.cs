using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public Transform player;
    public Slider slider;
    public TextMeshProUGUI text;
    public float topSpeed = 10;

    private float displayedValue;
    private Vector3 lastPos;
    void Start()
    {
        slider.maxValue = topSpeed;
        lastPos = player.position;

    }
    private void Update()
    {
        float speed = Mathf.Abs(Vector3.Distance(lastPos, player.position))/Time.deltaTime;
        lastPos = player.position;
        displayedValue = Mathf.Lerp(displayedValue, speed, Time.deltaTime);
        slider.value = displayedValue;
        text.text = (displayedValue).ToString("F0") + " m/s";
    }
}
