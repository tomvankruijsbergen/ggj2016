using UnityEngine;
using System.Collections;


public class SeanceController : MonoBehaviour {

	[Range(0.0f, 1.0f)]
	public float intensity;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {
	
		PlayerPrefs.SetFloat ("intensity", intensity);

	}
}
