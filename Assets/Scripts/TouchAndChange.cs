using UnityEngine;
using System.Collections;

public class TouchAndChange : MonoBehaviour
{
    public AudioClip spellSound;
    private GameObject touchedItem;
    public int id;
    private bool canChange = true;

    void Start()
    {

    }

    void Update()
    {
        if (touchedItem != null && SixenseInput.Controllers[id].Trigger == 1 && canChange)
        {
            touchedItem.GetComponent<GridItem>().toggleState();
            canChange = false;
            //AudioSource.PlayClipAtPoint("spellSound");
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
            Debug.Log("Item selected" + other.name);
            //will be makeHighlighted()
            touchedItem = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (touchedItem == other.gameObject)
        {
            Debug.Log("Item de-selected" + other.name);

            //will be makeHighlighted()
            touchedItem = null;
        }
    }    
}
