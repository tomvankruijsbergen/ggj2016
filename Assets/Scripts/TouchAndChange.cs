using UnityEngine;
using System.Collections;

public class TouchAndChange : MonoBehaviour
{
    private GameObject touchedItem;
    public int id;
    private bool canChange = true;
    public AudioClip grabItemSound;
    private bool touchingDrum = false;
    private GameObject drum;

    void Start()
    {

    }

    void Update()
    {
        if (touchingDrum && SixenseInput.Controllers[id].Trigger == 1)
        {
            drum.GetComponent<SubmitButton>().HitDrum();
            touchingDrum = false;
        }
        if (touchedItem != null && SixenseInput.Controllers[id].Trigger == 1 && canChange)
        {
            transform.parent.GetComponent<Animator>().SetTrigger("select");
            touchedItem.GetComponent<GridItem>().toggleState();
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
        if (other.tag.Equals("LittleDrum"))
        {
            //Debug.Log("Touching Drum");
            drum = other.gameObject;
            touchingDrum = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (touchedItem == other.gameObject)
        {
            //will be makeHighlighted()
            touchedItem = null;
        }
        if (other.tag.Equals("LittleDrum"))
        {
            touchingDrum = false;
        }
    }    
}
