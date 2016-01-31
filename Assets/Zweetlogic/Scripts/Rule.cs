using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rule {
	
	public static Rule generateRule(List<GameObject> itemTypes) {
		// Generate a pattern, then a rule with that pattern.
		bool horizontal = Random.value < 0.5;

		Vector2 secondPosition = new Vector2 ();
		if (horizontal == true) {
			secondPosition.x = 1;
		} else {
			secondPosition.y = 1;
		}

		var pattern = new Dictionary<Vector2, RuleCell> ();
		pattern.Add (new Vector2 (), RuleCell.generateRuleCell (itemTypes));
		pattern.Add (secondPosition, RuleCell.generateRuleCell (itemTypes));

		var rule = new Rule ();
		rule.pattern = pattern;
		return rule;
	}

	public Dictionary<Vector2, RuleCell> pattern;
}
