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

	private Vector3 baseScale;

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
				break;
		case GameLogic.ItemStates.Off:
				makeUnhighlighted();
				break;
		}
	}

	void Start(){
		source = GetComponent<AudioSource> ();
		baseScale = transform.localScale;
	}

	public void Show(){

		transform.localScale = Vector3.zero;
		float d = Random.value;
		transform.DOScale (baseScale, 0.5f).SetEase (Ease.OutBack).SetDelay (d).OnComplete(Activate);
		transform.DOShakeRotation(0.5f, 10f, 8, 50).SetDelay(d);
		if (clipOn != null) {
			source.clip = clipOn;
			source.Play ();
		}
	}

	private void Activate(){
		allowInteraction = true;
	}

	bool allowInteraction = false;

	public void Hide(bool isPoint){
		allowInteraction = false;

		if (isPoint) {
			//stay and float into the fire

		} else {
			//hide
			transform.DOScale(Vector3.zero, 0.4f).SetEase(Ease.InExpo).SetDelay(Random.value * 0.5f);
		}


		if (clipOff != null) {
			source.clip = clipOff;
			source.Play ();
		}
	}

	public void RollOver(){
		if (!allowInteraction)
			return;
		//scale up
		transform.DOScale(baseScale * 1.2f, 0.4f).SetEase(Ease.OutBack);

	}

	public void RollOut(){
		if (!allowInteraction)
			return;
		//scale down
		transform.DOScale(baseScale, 0.8f).SetEase(Ease.OutExpo);
	}
}
