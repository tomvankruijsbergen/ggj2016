using UnityEngine;
using System.Collections;

public class DevTemp : MonoBehaviour {

	void OnMouseDown() {
		GameLogic.Instance.sendSubmitEvent ();
	}
}
