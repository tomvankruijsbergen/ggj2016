using UnityEngine;
using System.Collections;

public class SeanceLight : MonoBehaviour {

	public Light light;
	public Rotate rotate;
	private Vector3 rotateSpeed;
	float intensity;
	// Use this for initialization
	void Start () {
		rotateSpeed = rotate.speed;
	}
	
	// Update is called once per frame
	void Update () {

		intensity = PlayerPrefs.GetFloat ("intensity");
		rotate.speed = rotateSpeed * intensity;
		light.intensity = intensity;
	}
}
