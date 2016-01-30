using UnityEngine;
using System.Collections;

public class TweenHandGrab : MonoBehaviour
{
    public GameObject[] fingers = new GameObject[5];
    private Quaternion[] startRotations = new Quaternion[5];
    private Vector3 fingerInc = new Vector3(0, 5, 5);

    void Start()
    {
        for (int i = 0; i < fingers.Length; i++)
        {
            startRotations[i] = fingers[i].transform.localRotation;
        }
    }

    void Update()
    {

    }
    private void CurlFingers()
    {
        for (int i = 0; i < fingers.Length; i++)
        {
            fingers[i].transform.localRotation = Quaternion.Euler(fingerInc);
        }
    }
}
