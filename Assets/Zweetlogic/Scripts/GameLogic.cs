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

	public void processGridSubmit (Grid grid, List<Rule> rules) {
		/* This flow of something.sendSubmitEvent() -> grid.OnSubmitButtonClicked() -> gameLogic.process()
		 * is necessary because, for now, we don't hard-couple grids to the game logic singleton.
		 * Later,when we should, then the grids probably don't need to be called for a delegate.
		 */


	}

	public delegate void _OnSubmitButtonClicked();
	public event _OnSubmitButtonClicked OnSubmitButtonClicked;

}
