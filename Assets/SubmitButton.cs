using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class SubmitButton : MonoBehaviour {

	private AudioSource source;
	public List<AudioClip> clips;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnMouseDown(){

		GameLogic.Instance.sendSubmitEvent ();

		int randomIndex = Mathf.FloorToInt (Random.Range (0, this.clips.Count));
		AudioClip clip = clips [randomIndex];

		if (source != null) {
			source.clip = clip;
			source.Play ();
		}
	}
    public void HitDrum()
    {
        GameLogic.Instance.sendSubmitEvent();

		int randomIndex = Mathf.FloorToInt (Random.Range (0, this.clips.Count));
		AudioClip clip = clips [randomIndex];

		if (source != null) {
			source.clip = clip;
			source.Play ();
		}
    }
}
