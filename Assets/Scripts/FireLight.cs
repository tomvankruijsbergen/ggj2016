using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class FireLight : MonoBehaviour {

	private Vector3 basePos;
	private Light light;
	public float minIntensity = 0.1f;
	public float maxIntensity = 1.5f;
	private float intensity = 0.1f;
	public float maxShake = 0.2f;
	public float maxInterval = 0.5f;
	private Vector3 shake = Vector3.one;
	private Vector3 targetPos = Vector3.zero;

	// Use this for initialization
	void Start () {
		targetIntensity = intensity;
		basePos = transform.position;
		light = GetComponent<Light> ();
	}

	private float t = 0;
	private float interval = 0;
	private float targetIntensity;
	// Update is called once per frame
	void Update () {

		intensity = minIntensity + ((maxIntensity - minIntensity) * PlayerPrefs.GetFloat ("intensity"));
		light.intensity += (targetIntensity - light.intensity) * 0.3f;
		transform.position = Vector3.Lerp (transform.position, basePos + targetPos, 2.0f * Time.deltaTime);
		t += Time.deltaTime;
		if (t > interval) {
			targetPos = Random.insideUnitSphere * maxShake;

			interval = Random.value * maxInterval;
			targetIntensity =  intensity - (Random.value * 0.3f * intensity);
			t = 0;
		}

	}
}
