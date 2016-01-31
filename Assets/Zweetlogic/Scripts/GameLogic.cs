using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : Singleton<GameLogic> {

	public enum ItemStates { Off = 0, On = 1, _All = 2 };

	protected GameLogic() {}

	public void sendSubmitEvent() {
		// zzzz why C# why
		this.OnSubmitButtonClicked();
	}

	public delegate void _OnSubmitButtonClicked();
	public event _OnSubmitButtonClicked OnSubmitButtonClicked;

}
