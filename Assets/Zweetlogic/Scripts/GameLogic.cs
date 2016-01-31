using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : Singleton<GameLogic> {

	public enum ItemStates { Off = 0, On = 1, _All = 2 };

	// The scoreScript attaches itself to the singleton. 
	public Score scoreScript;

	protected GameLogic() {}

	public void sendSubmitEvent() {
		// zzzz why C# why
		if (this.OnSubmitButtonClicked != null) this.OnSubmitButtonClicked();
	}

	private int debugcounter = 0;
	public void sendScoreChangedEvent(float newScore) {
		// zzzz why C# why
		debugcounter++;
		if (newScore > 0 && debugcounter > 100) {
			debugcounter = 0;
			Debug.Log (newScore);
		}
		float newIntensity = newScore / 100;
		if (newIntensity > 1)
			newIntensity = 1;
		PlayerPrefs.SetFloat("intensity", newIntensity);
		if (this.OnScoreChanged != null) this.OnScoreChanged(newScore);
	}

	public delegate void _OnSubmitButtonClicked();
	public event _OnSubmitButtonClicked OnSubmitButtonClicked;

	public delegate void _OnScoreChanged(float newScore);
	public event _OnScoreChanged OnScoreChanged;

}
