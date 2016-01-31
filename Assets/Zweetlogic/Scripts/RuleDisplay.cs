using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class RuleDisplay : MonoBehaviour {

	public RuleCard horizontal;
	public RuleCard vertical;
	public Transform stackBase;
	public Transform tableBase;

	private RuleCard currentRuleCard;
	private List<Rule> currentRules;

	private AudioSource source;

	public AudioClip hideClip;
	public AudioClip showClip;

	void Start(){

		source = GetComponent<AudioSource> ();
		horizontal.gameObject.transform.position = stackBase.position;
		vertical.gameObject.transform.position = stackBase.position; 
	}


	public void rulesUpdated(List<Rule> newRules) {

		currentRules = newRules;


		if (currentRuleCard != null) {
		
			HideCurrent ();
			DOVirtual.DelayedCall (1, UpdateCards);
		} else {
			UpdateCards ();
		}

	}

	private void HideCurrent(){

		currentRuleCard.gameObject.transform.DOMove (stackBase.position, 0.3f).SetEase (Ease.InExpo);

		if (hideClip != null) {
			source.clip = hideClip;
			source.Play ();
		}
	}

	private void UpdateCards(){
			
		Rule rule = currentRules [0];

		Vector2 highestVector = new Vector2 ();
		foreach (var item in rule.pattern) {
			if (item.Key.sqrMagnitude > highestVector.sqrMagnitude) {
				highestVector = item.Key;
			}
		}


		if (highestVector.y > 0) {
			//vertical
			horizontal.gameObject.SetActive(false);
			currentRuleCard = vertical;
		} else {
			//horizontal
			vertical.gameObject.SetActive (false);
			currentRuleCard = horizontal;
		}

		currentRuleCard.gameObject.SetActive (true);

		// zzzz C#
		var index = 0;
		foreach (var item in rule.pattern) {
			
			RuleCell cell = item.Value;
			currentRuleCard.setIconToSprite (index, cell.getSprite());
			index++;
		}


		//show card
		currentRuleCard.gameObject.transform.DOMove (tableBase.position, 0.5f).SetEase (Ease.OutExpo);
		//play sound
		if (showClip != null) {
			source.clip = showClip;
			source.Play ();
		}
	}
}
