using UnityEngine;
using System.Collections;

public class FireSize : MonoBehaviour {
	public float minScale = 0.3f;
	public float maxScale = 2.0f;
	Vector3 scale;
	float intensity;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		intensity = PlayerPrefs.GetFloat ("intensity");
		scale = (Vector3.one  * minScale) + (Vector3.one * intensity * (maxScale - minScale));
		scale.y *= 1 + intensity;
		transform.localScale = scale;
	}
}
