using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RuleDisplay : MonoBehaviour {

	public RuleCard horizontal;
	public RuleCard vertical;

	public void rulesUpdated(List<Rule> newRules) {
		Rule rule = newRules [0];

		Vector2 highestVector = new Vector2 ();
		foreach (var item in rule.pattern) {
			if (item.Key.sqrMagnitude > highestVector.sqrMagnitude) {
				highestVector = item.Key;
			}
		}
		RuleCard currentRuleCard;

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
	}
}
