using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExitApplication : MonoBehaviour
{
    public float timeToQuit = 60.0f;
    private float timeElapsed = 0.0f;

    private void Awake()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        timeElapsed += Time.deltaTime;
        if (timeElapsed > timeToQuit)
        {
            Application.Quit();
        }

        if (Keyboard.current != null)
        {
            if (Keyboard.current.anyKey.isPressed)
                timeElapsed = 0.0f;
        }

        if (Gamepad.current != null)
        {
            if (Gamepad.current.allControls.Any(control => control.IsPressed()))
            {
                timeElapsed = 0.0f;
            }
        }
        
        if (Joystick.current != null)
        {
            if (Joystick.current.allControls.Any(control => control.IsPressed()))
            {
                timeElapsed = 0.0f;
            }
        }

    }
}
