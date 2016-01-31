using UnityEngine;
using System.Collections;

public class GridItemGraphic : MonoBehaviour {

	public GameObject highlightModel;
	public GameObject unHighLightModel;

	public string type = "SET THIS";
	public Sprite sprite;

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


	public void Show(){

		//do random popup animation
	}

	public void Hide(){
	
		//
	}
}
