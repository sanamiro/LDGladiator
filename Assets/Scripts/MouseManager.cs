using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour
{
    public enum MousePos
    {
        TopLeft,
        TopRight,
        BotRight,
        BotLeft
    }

    public float Threshold = 0.1f;
    public bool Top = false;
    public bool Left = false;
    public bool hasJoystick = false;

    public MousePos CurrentMousePos
    {
        get
        {
            return Top ? (Left ? MousePos.TopLeft : MousePos.TopRight) : (Left ? MousePos.BotLeft : MousePos.BotRight);
        }
    }

    public Vector2 MouseDir
    {
        get
        {
            return new Vector2(Left ? -1 : 1, Top ? 1 : -1);
        }
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            // Lock the cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        float dx;
        float dy;

        if (hasJoystick == false)
        {
            dx = Input.GetAxis("Mouse X");
            dy = Input.GetAxis("Mouse Y");
        }
        else
        {
            dx = -Input.GetAxis("RightAnalogX1") * 10;
            dy = Input.GetAxis("RightAnalogY1") * 10;
        }

        if (Mathf.Abs(dx) > Threshold)
        {
            Left = dx < 0;
        }

        if (Mathf.Abs(dy) > Threshold)
        {
            Top = dy > 0;
        }


        Debug.DrawRay(transform.position, new Vector3(MouseDir.x, 0, MouseDir.y), Color.cyan, 0.1f);
    }
}
