using UnityEngine;
using System.Collections;


public class SeanceController : MonoBehaviour {

	[Range(0.0f, 1.0f)]
	public float intensity;

	// Use this for initialization
	void Start () {
	

	}


	float glowIntensity = 0f;
	// Update is called once per frame
	void Update () {
	
		//PlayerPrefs.SetFloat ("intensity", intensity);

		//ses sinus intensity for purplish glow effect
		glowIntensity =  (Mathf.Sin(Time.timeSinceLevelLoad * (intensity*10f)) * 0.5f)* (intensity * 0.2f);
		//Debug.Log ("glow: " + glowIntensity);
		PlayerPrefs.SetFloat ("glowIntensity", glowIntensity);
	}
}
