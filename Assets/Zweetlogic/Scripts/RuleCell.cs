using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class RuleCell {

	public static RuleCell generateRuleCell(List<GameObject> itemTypes) {
		int randomTypeIndex = Mathf.FloorToInt (Random.Range (0, itemTypes.Count));
		GameObject randomItemType = itemTypes [randomTypeIndex];
		var itemscript = randomItemType.GetComponent<GridItemGraphic> ();

		GameLogic.ItemStates randomState = (GameLogic.ItemStates)Mathf.FloorToInt (Random.Range (0, (int)GameLogic.ItemStates._All));

		var ruleCell = new RuleCell ();
		ruleCell.itemScript = itemscript;
		ruleCell.state = randomState;
		return ruleCell;
	}

	public override string ToString() {
		return this.GetType().Name + " : " + this.itemScript.type + " - " + this.state;
	}

	public GameObject getSprite() {
		if (this.state == GameLogic.ItemStates.Off) {
			return this.itemScript.spriteOff;
		} else {
			return this.itemScript.spriteOn;
		}
	}
	public GridItemGraphic itemScript;

	public GameLogic.ItemStates state;

}
