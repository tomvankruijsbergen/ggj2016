using UnityEngine;
using System.Collections;

public class TouchAndChange : MonoBehaviour
{
    private GameObject touchedItem;
    public int id;
    private bool canChange = true;
    public AudioClip grabItemSound;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
        }
        if (touchedItem != null && SixenseInput.Controllers[id].Trigger == 1 && canChange)
        {
            transform.parent.GetComponent<Animator>().SetTrigger("select");
            //touchedItem.GetComponent<GridItem>().toggleState();
            //GetComponent<AudioSource>().PlayOneShot(grabItemSound, 1f);
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
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (touchedItem == other.gameObject)
        {
            //will be makeHighlighted()
            touchedItem = null;
        }
    }    
}
