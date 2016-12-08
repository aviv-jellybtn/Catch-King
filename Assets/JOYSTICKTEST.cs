using UnityEngine;
using System.Collections;
using System;

public class JOYSTICKTEST : MonoBehaviour
{
//    private void Start()
//    {
//    }
//
    private void Update()
    {
        var connectedJoystickNames = Input.GetJoystickNames();
        foreach (var joystickName in connectedJoystickNames)
        {
            Debug.Log(joystickName);
        }
    }
}
