using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct SoundLayer {
	public AudioSource audioSource;
	public float intensityLowestVolume;
	public float intensityHighestVolume;
}
	

public class VoodooAudio : MonoBehaviour {

	public List<SoundLayer> soundLayers = new List<SoundLayer>();

	void Update () {
		float intensity = PlayerPrefs.GetFloat ("intensity");
		foreach (SoundLayer layer in this.soundLayers) {
			float volume;
			if (intensity >= layer.intensityHighestVolume) {
				volume = 1;
			} else if (intensity <= layer.intensityLowestVolume) {
				volume = 0;
			} else {
				volume = (intensity - layer.intensityLowestVolume) / layer.intensityHighestVolume;
			}

			layer.audioSource.volume = volume;
		}
	}
}
