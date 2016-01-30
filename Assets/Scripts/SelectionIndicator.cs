using UnityEngine;
using System.Collections;

public class SelectionIndicator : MonoBehaviour
{
    private bool isSelected = false;
    private Material unselectedMat;
    public Material selectedMat;

    void Start()
    {
        unselectedMat = GetComponent<Renderer>().material;
    }

    void Update()
    {

    }
    public void ChangeSelected(bool verdict)
    {
        if (verdict && !isSelected)
        {
            isSelected = true;
            GetComponent<Renderer>().material = selectedMat;
        } else if (!verdict && isSelected)
        {
            isSelected = false;
            GetComponent<Renderer>().material = unselectedMat;
        }
    }
}
