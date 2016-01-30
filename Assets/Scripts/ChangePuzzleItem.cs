using UnityEngine;
using System.Collections;

public class ChangePuzzleItem : MonoBehaviour
{
    public Material[] matArray = new Material[4];
    private int currentMat = 0;

    void Start()
    {
        GetComponent<Renderer>().material = matArray[currentMat];
    }

    void Update()
    {
    }
    public void ChangeStyle()
    {
         int tempInt = Random.Range(0, matArray.Length);
        if (tempInt == currentMat)
        {
            ChangeStyle();
        } else
        {
            GetComponent<Renderer>().material = matArray[tempInt];
            currentMat = tempInt;
        }
    }
}
