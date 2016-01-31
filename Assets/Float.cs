using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {

	// Use this for initialization
	void Start () {
		basepos = transform.position;
	}
	Vector3 pos;
	Vector3 basepos;

	float t = 0;
	// Update is called once per frame
	void Update () {

		t = Mathf.Sin (Time.timeSinceLevelLoad ) * 0.05f;
		pos.y = t;

		transform.position = basepos + pos;;
	}
}
