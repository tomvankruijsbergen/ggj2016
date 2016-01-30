using UnityEngine;
using System.Collections;

public class TouchAndChange : MonoBehaviour
{
    private GameObject touchedItem;
    public int id;
    private bool canChange = true;
    private float selectedScale = 0.5f;

    void Start()
    {

    }

    void Update()
    {
        if (touchedItem != null && SixenseInput.Controllers[id].Trigger == 1 && canChange)
        {
            touchedItem.GetComponent<ChangePuzzleItem>().ChangeStyle(); //will be toggleState()
            canChange = false;
        }
        if (SixenseInput.Controllers[id].Trigger == 0 && !canChange)
        {
            canChange = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("PuzzleItem"))
        {
            //will be makeHighlighted()
            touchedItem = other.gameObject;
            touchedItem.transform.localScale = new Vector3(selectedScale, selectedScale, selectedScale);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (touchedItem == other.gameObject)
        {
            //will be makeHighlighted()
            touchedItem.transform.localScale = new Vector3(1, 1, 1);
            touchedItem = null;
        }
    }
}
