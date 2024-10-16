using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoInput : MonoBehaviour
{
    void OnMessageArrived(string msg)
    {
        Debug.Log(msg);
    }

    void OnConnectionEvent (bool success)
    {
        Debug.Log($"connect: {success}");
    }
}
