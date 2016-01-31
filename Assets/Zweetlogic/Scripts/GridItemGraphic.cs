using UnityEngine;
using System.Collections;
using DG.Tweening;

public class GridItemGraphic : MonoBehaviour {

	public GameObject highlightModel;
	public GameObject unHighLightModel;

	public string type = "SET THIS";
	public GameObject spriteOn;
	public GameObject spriteOff;

	private AudioSource source;

	public AudioClip clipOn;
	public AudioClip clipOff;

	public AudioClip audioStateOn;
	public AudioClip audioStateOff;

	private Vector3 baseScale;
    void Start()
    {

        baseScale = transform.localScale;
    }

	public bool isActive(){
		return allowInteraction;
	}

	public void makeHighlighted() {
		highlightModel.SetActive (true);
		unHighLightModel.SetActive (false);
	}
	public void makeUnhighlighted() {
		highlightModel.SetActive (false);
		unHighLightModel.SetActive (true);
	}

	public void changeToState(GameLogic.ItemStates state, bool animated) {
		switch(state){
		case GameLogic.ItemStates.On:
			makeHighlighted ();
			if (animated == true && this.audioStateOn) {
				source.clip = audioStateOn;
				source.Play ();
			}
			break;
		case GameLogic.ItemStates.Off:
			makeUnhighlighted();
			if (animated == true &&this.audioStateOff) {
				source.clip = audioStateOff;
				source.Play ();
			}
			break;
		}
	}

	void Awake(){
		source = GetComponent<AudioSource> ();
	}

	public void Show(){

		//transform.localScale = Vector3.zero;
		float d = Random.value;
		transform.DOScale (Vector3.zero, 0.5f).From().SetEase (Ease.OutBack).SetDelay (d).OnComplete(Activate);
		transform.DOShakeRotation(0.5f, 40f, 8, 50).SetDelay(d);
		if (clipOn != null) {
			source.clip = clipOn;
			source.Play ();
		}
	}

	private void Activate(){
		allowInteraction = true;
	}

	bool allowInteraction = false;

	/*	0 = unimportant
	 * 	1 = incorrect
	 * 	2 = correct
	 */
	public void Hide(int pointResult){
		
		allowInteraction = false;

		if (pointResult == 2) {

			//stay and float into the fire
			Vector3 randomOffset = Vector3.one * Random.value * 0.1f;
			transform.DOMove (new Vector3(0.052f, 1f, 2.433f) + randomOffset, 1f).SetEase (Ease.InOutCirc).SetDelay(1f + (Random.value)).OnComplete(IntoTheFire);
		
		} else if (pointResult == 1) {
			// whatever
			transform.DOShakeRotation(2f, 40,7,40).OnComplete(Fall).SetDelay(1f + Random.value * 0.3f);

		} else {
			//hide
			transform.DOScale(new Vector3(0,0,0), 0.4f).SetEase(Ease.InExpo).SetDelay(Random.value * 0.5f);
		}


		if (clipOff != null) {
			source.clip = clipOff;
			source.Play ();
		}
	}

	void Fall(){

		transform.DOScale (new Vector3 (0, 0, 0), 0.4f).SetEase (Ease.InExpo);
	}

	void IntoTheFire(){
		Vector3 endPos = Vector3.zero;
		endPos.y = 0.6f;
		transform.DOMove (endPos, 1.5f).SetEase (Ease.InExpo).OnComplete(DoFireParticle);
	}

	void DoFireParticle(){
		
		//awesome explosion
	}

	public void RollOver(){
		//if (!allowInteraction)
			return;
		//scale up
		//transform.DOScale(baseScale * 1.2f, 0.4f).SetEase(Ease.OutBack);

	}

	public void RollOut(){
		//if (!allowInteraction)
			return;
		//scale down
		//transform.DOScale(baseScale, 0.8f).SetEase(Ease.OutExpo);
	}
}
