using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArduinoInput : MonoBehaviour
{
    public static Action<int> InputRecieved;
    public static Action<bool> ConnectionEvent;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnMessageArrived(string msg)
    { 
        if (int.TryParse(msg, out int value) && InputRecieved != null) {
            InputRecieved.Invoke(value);
        }
    }

    void OnConnectionEvent (bool success)
    {
        if(ConnectionEvent != null)
        {
            ConnectionEvent.Invoke(success);
        }
    }
}
