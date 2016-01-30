using UnityEngine;
using System.Collections;

public class GreenLit : MonoBehaviour {

	private Material material;
	private float intensity = 0;
	private Color c;
	private Color baseColor;

	// Use this for initialization
	void Start () {

		foreach (Material matt in GetComponent<Renderer>().materials) {
			Debug.Log ("found matt: " + matt.name);
			if (matt.name == "GreenLit (Instance)") {
				material = matt;
				Debug.Log ("found matt: " + material);
				baseColor = material.GetColor ("_EmissionColor");
				c = material.GetColor ("_EmissionColor");
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		intensity = PlayerPrefs.GetFloat("intensity");
		c.r = intensity * baseColor.r;
		c.g = intensity * baseColor.g;
		c.b = intensity * baseColor.b;
		material.SetColor ("_EmissionColor", c);
	}
}
