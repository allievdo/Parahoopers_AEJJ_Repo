using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float time;
    public PlayerController playerController;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (playerController.isPlaying == false)
            return;
        else
        {
            time += Time.deltaTime;
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
