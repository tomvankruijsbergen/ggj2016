using UnityEngine;
using System.Collections;

public class LockMouse : MonoBehaviour
{
    public bool lockMouse = false;
    private bool mouseLocked;

    void Start()
    {
        mouseLocked = lockMouse;
        if (lockMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (mouseLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                mouseLocked = false;
            } else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                mouseLocked = true;
            }
        }
    }
}
