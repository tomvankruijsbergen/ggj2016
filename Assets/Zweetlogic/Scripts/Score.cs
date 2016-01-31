using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour {

	private float currentScore = 0;

	public float scorePerMatch = 1;
	public List<float> bonusPointsForMistakeCount;
	public float decaySpeedFactor = 0.98f;
	public float decaySpeedConstant = 0.5f;

	// Todo: implement this. Allows us to reward getting a perfect score with several rules.
	//public float bonusMultiplierForMoreRules = 2;

	void Start () {
		GameLogic.Instance.scoreScript = this;
	}
	void Destroy() {
		if (GameLogic.Instance.scoreScript == this) {
			GameLogic.Instance.scoreScript = null;
		}
	}

	public void UpdateScore(Dictionary<Vector2, Rule> correctMatches, Dictionary<Vector2, Rule> forgottenMatches){
		float addedScore = 0;
		addedScore += correctMatches.Count * this.scorePerMatch;
		if (bonusPointsForMistakeCount.Count > forgottenMatches.Count) {
			addedScore += bonusPointsForMistakeCount [forgottenMatches.Count];
		}
		Debug.Log ("=== MATCHES D-F " + correctMatches.Count + " - " + forgottenMatches.Count + "  : " +  addedScore);

		this.currentScore += addedScore;
		GameLogic.Instance.sendScoreChangedEvent (this.currentScore);
	}

	void Update () {
		float tempScore = this.currentScore;
		this.currentScore *= Mathf.Pow (this.decaySpeedFactor, Time.deltaTime);
		this.currentScore -= decaySpeedConstant * Time.deltaTime;
		if (this.currentScore < 0)
			this.currentScore = 0;
		if (this.currentScore > 100)
			this.currentScore = 100;
		GameLogic.Instance.sendScoreChangedEvent (this.currentScore);
	}
}
