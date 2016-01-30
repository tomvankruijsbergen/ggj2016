using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Spirit : MonoBehaviour {

	public Transform body;

	private float intensity = 0.1f;
	public float nearRange = 1f;
	public float farRange = 4f;
	private Vector3 pos = Vector3.zero;


	// Use this for initialization
	void Start () {
	
		
	}


	// Update is called once per frame
	void Update () {
	
	

		intensity = PlayerPrefs.GetFloat ( "intensity" );

		pos.z = farRange - (intensity * (farRange - nearRange));
		body.localPosition = pos;
	}
}
