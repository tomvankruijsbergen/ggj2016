using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridItem : MonoBehaviour {

	[HideInInspector] public Vector2 position;
	[HideInInspector] public bool used = true;

	[HideInInspector] public GridItemGraphic itemGraphic;
	public GameLogic.ItemStates state = GameLogic.ItemStates.Off;

	public void setUsed(bool used) {
		this.used = used;
		this.gameObject.SetActive (used);
	}

	public void Hide(int pointResult) {
		itemGraphic.Hide (pointResult)	;
	}


	public void changeToType(GameObject gameObject, bool animated, Vector3 randomOffset) {
		
		this.itemGraphic = null;

		List<GameObject> children = new List<GameObject>();
		foreach (Transform child in transform) children.Add(child.gameObject);
		children.ForEach(child => Destroy(child));

		GameObject newChild = GameObject.Instantiate (gameObject);
		newChild.transform.position = randomOffset;
		newChild.transform.SetParent (transform, false);

		// Sets the BoxCollider's center to the offset of the child.
		// If this is changed to a SphereCollider, we have to update this here.
		var collider = this.GetComponent<BoxCollider> ();
		collider.center = randomOffset;

		GridItemGraphic itemGraphic = newChild.GetComponent<GridItemGraphic> ();
		this.itemGraphic = itemGraphic;

		itemGraphic.Show ();
	}

	public void changeToState(GameLogic.ItemStates state, bool animated) {
		this.state = state;
		if (this.itemGraphic) {
			this.itemGraphic.changeToState (state, animated);
		}
	}

	public void OnMouseDown() {
		// Helper function to help development. Todo: remove before release.
		this.toggleState ();
	}

	public void makeHighlighted() {
		if (this.itemGraphic) {
			this.itemGraphic.makeHighlighted ();
		}
	}
	public void makeUnhighlighted() {
		if (this.itemGraphic) {
			this.itemGraphic.makeUnhighlighted ();
		}
	}

	public void toggleState() {
		if (itemGraphic != null && itemGraphic.isActive() == false)
			return;
		GameLogic.ItemStates newState;
		if (this.state == GameLogic.ItemStates.Off) {
			newState = GameLogic.ItemStates.On;
		} else {
			newState = GameLogic.ItemStates.Off;
		}

		this.changeToState(newState, true);
	}
}
