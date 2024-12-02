using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CourseCheckpoints : MonoBehaviour
{
    //This implements something where you have to go back and fix your mistake. That way we are only worried about time.
    public event EventHandler OnPlayerCorrectCheckpoint;
    public event EventHandler OnPlayerWrongCheckpoint;

    public bool correct = false;
    public bool wrong = false;

    private List<HoopScript> hoopCheckpointList;
    private int nextHoopCheckpointIndex;

    public PlayerController playerController;


    private void Awake()
    {
        Transform hoopsCheckpointTransform = transform.Find("Course");
        Debug.Log("Found Course");

        hoopCheckpointList = new List<HoopScript>();
        foreach(Transform hoopsCheckpointSingleTransform in hoopsCheckpointTransform)
        {
            HoopScript hoopScript = hoopsCheckpointSingleTransform.GetComponent<HoopScript>();

            hoopScript.SetCourseCheckpoints(this);

            hoopCheckpointList.Add(hoopScript);
        }

        nextHoopCheckpointIndex = 0;
    }

    public void PlayerThroughCheckpoint(HoopScript hoopScript, Transform playerTransform)
    {
        if (hoopCheckpointList.IndexOf(hoopScript) == nextHoopCheckpointIndex)
        {
            //For the correct hoop:
            Debug.Log("Correct");
            correct = true;
            wrong = false;

            HoopScript correctHoopSingle = hoopCheckpointList[nextHoopCheckpointIndex];
            correctHoopSingle.WinColor();

            nextHoopCheckpointIndex = (nextHoopCheckpointIndex + 1) % hoopCheckpointList.Count;
            OnPlayerCorrectCheckpoint?.Invoke(this, EventArgs.Empty);

            HoopScript nextHoopSingle = hoopCheckpointList[nextHoopCheckpointIndex];
            nextHoopSingle.NextColor();

            Debug.Log(nextHoopCheckpointIndex);

            if (nextHoopCheckpointIndex >= hoopCheckpointList.Count - 1)
            {
                Time.timeScale = 0f;
                Debug.Log("you win");
                playerController.hasEnded = true;
                SceneManager.LoadScene("Win");
            }
        }

        else
        {
            //For the wrong hoop:
            Debug.Log("Wrong");
            correct = false;
            wrong = true;
            OnPlayerWrongCheckpoint?.Invoke(this, EventArgs.Empty);

            HoopScript correctHoopSingle = hoopCheckpointList[nextHoopCheckpointIndex];
        }
    }
}
