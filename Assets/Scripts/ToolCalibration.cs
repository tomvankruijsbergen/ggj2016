using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class ToolCalibration : MonoBehaviour
{

    void Start()
    {
        Invoke("UpdatePosition", 1f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.VR.InputTracking.Recenter();
        }
        if (SixenseInput.Controllers[0].GetButtonDown(SixenseButtons.START) || SixenseInput.Controllers[1].GetButtonDown(SixenseButtons.START))
        {
            Invoke("UpdatePosition", 0.2f);           
        }


    }
    void UpdatePosition()
    {
        Vector3 temp = Camera.main.transform.position;
        temp.y -= 0.2f;
        transform.position = temp;
    }
}
