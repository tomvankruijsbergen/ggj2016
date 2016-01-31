using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SubmitButton : MonoBehaviour {

	private AudioSource source;

	// Use this for initialization
	void Start () {

		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnMouseDown(){

		GameLogic.Instance.sendSubmitEvent ();

		if (source != null) {
			source.Play ();
		}
	}
    public void HitDrum()
    {
        Debug.Log("Drum Hit");
        GameLogic.Instance.sendSubmitEvent();

        if (source != null)
        {
            source.Play();
        }
    }
}
