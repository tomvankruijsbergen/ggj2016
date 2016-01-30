using UnityEngine;
using System.Collections;

public class GridItemGraphic : MonoBehaviour {

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
