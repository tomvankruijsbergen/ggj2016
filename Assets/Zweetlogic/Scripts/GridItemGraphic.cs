using UnityEngine;
using System.Collections;

public class GridItemGraphic : MonoBehaviour {

	public string type = "SET THIS";

	public void makeHighlighted() {
		Debug.Log ("highlight");
	}
	public void makeUnhighlighted() {
		Debug.Log ("UN-highlight");
	}

	public void changeToState(GameLogic.ItemStates state, bool animated) {
		// 
	}
}
