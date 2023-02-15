using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PS4;
using System;

public class MouseCamera : MonoBehaviour
{
    public Vector2 turn;
    public float sensitivity = .5f;

    public int playerID;
   
    void Start()
    {
        PS4Input.PadResetOrientation(playerID);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        KeyCode options = (KeyCode)Enum.Parse(typeof(KeyCode), "Joystick1Button7", true);

        if (Input.GetKey(options))
            PS4Input.PadResetOrientation(playerID);

        Vector4 v = PS4Input.PadGetLastOrientation(0);

        turn.x += Mathf.Clamp(-v.z * 10, -1, 1) * sensitivity;
        
        if (turn.y >= 15)
        {
            turn.y = 14.99f;

        }
        else if(turn.y <= -15)
        {
            turn.y = -14.99f;
        }
        else
            turn.y += Mathf.Clamp(v.x * 10, -1, 1) * sensitivity;
        
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);

    }
}
