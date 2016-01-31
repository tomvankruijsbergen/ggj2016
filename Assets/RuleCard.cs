using UnityEngine;
using System.Collections;

public class RuleCard : MonoBehaviour {

	public GameObject firstIcon;
	public GameObject secondIcon;

	private GameObject firstIconSprite;
	private GameObject secondIconSprite;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Clear(){

	}

	public void setIconToSprite(int index, GameObject sprite) {
		
		GameObject newSprite = GameObject.Instantiate (sprite);
		newSprite.transform.SetParent (transform, false);

		if (index == 0) {
			if (firstIconSprite)
				Destroy (firstIconSprite);
			newSprite.transform.SetParent (firstIcon.transform, false);

			firstIconSprite = newSprite;
		} else {
			if (secondIconSprite)
				Destroy (secondIconSprite);
			newSprite.transform.SetParent (secondIcon.transform, false);
			secondIconSprite = newSprite;
		}

		newSprite.transform.localPosition = Vector3.zero;
	}
}
