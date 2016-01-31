using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public struct GridScoreResult {
	public Dictionary<Vector2, Rule> matches;
	public Dictionary<Vector2, Rule> matchesWithoutState;
}

public class Grid : MonoBehaviour {
	
	protected Dictionary<Vector2, GridItem> objectsByPosition = new Dictionary<Vector2, GridItem>();

	protected PixelMap pixelMap = new PixelMap();
	public List<Texture> gridSetups = new List<Texture>();

	public List<GameObject> itemTypes = new List<GameObject> ();
	public Vector3 randomOffsetRadius = new Vector3();

	protected List<Rule> rules;
	public GameObject ruleDisplayer;

	// This is here so the grid can send the score script messages, and then the GameLogic can handle broadcasting the score.
	// The idea is that the scoreScript is always the one that sends score changes to the GameLogic and noone else.
	public Score scoreScript;

	public float gridCompleteDisappearTime = 0.5f;

	private bool gridEnabled = true;

	public void Awake() {
		GameLogic.Instance.OnSubmitButtonClicked += this.OnSubmitButtonClicked;
	}
	public void Destroy() {
		GameLogic.Instance.OnSubmitButtonClicked -= this.OnSubmitButtonClicked;
	}

	void Start() {
		int childCount = transform.childCount;
		for (int i = 0; i < childCount; i++) {
			Transform row = transform.GetChild (i);

			int cellCount = row.childCount;
			for (int j = 0; j < cellCount; j++) {
				Transform cell = row.GetChild (j);

				GridItem cellItem = cell.GetComponent<GridItem> ();
				cellItem.position = new Vector2 (j, i);

				this.objectsByPosition.Add (cellItem.position, cellItem);
			}
		}
		this.populateGrid (false);
	}

	// This function gets invoked.
	private void populateGridAnimated() {
		this.populateGrid (true);
	}
	private void populateGrid(bool animated) {
		// NOTICE: always just takes the first one.
		int randomIndex = 0;//Mathf.FloorToInt (Random.Range (0, this.gridSetups.Count));
		Texture2D texture = this.gridSetups [randomIndex] as Texture2D;
		Dictionary<Vector2, bool> pixelData = this.pixelMap.pixelmapFromTexture (texture);

		/* Steps:
		 * For each item,
		 * - either disable or enable it based on a pixel map.
		 * - enabled items become a type.
		 */
		foreach (var item in this.objectsByPosition) {
			bool isEnabled = pixelData [item.Key];
			item.Value.setUsed (isEnabled);

			if (isEnabled == true) {
				int randomTypeIndex = Random.Range(0, this.itemTypes.Count);
				GameObject itemType = this.itemTypes [randomTypeIndex];

				Vector3 randomOffset = new Vector3 ();
				randomOffset.x = Random.Range (-this.randomOffsetRadius.x, this.randomOffsetRadius.x);
				randomOffset.y = Random.Range (-this.randomOffsetRadius.y, this.randomOffsetRadius.y);
				randomOffset.z = Random.Range (-this.randomOffsetRadius.z, this.randomOffsetRadius.z);
				item.Value.changeToType (itemType, animated, randomOffset);
					
				GameLogic.ItemStates randomState = (GameLogic.ItemStates)Mathf.FloorToInt (Random.Range (0, (int)GameLogic.ItemStates._All));
				item.Value.changeToState (randomState, animated);
			}
		}

		this.rules = new List<Rule> ();
		this.rules.Add (Rule.generateRule (this.itemTypes));
		//this.rules.Add (Rule.generateRule (this.itemTypes));

		/*foreach (var rule in this.rules) {
			foreach (var cell in rule.pattern) {
				Debug.Log (cell);
			}
		}*/

		if (this.ruleDisplayer) {
			var r = this.ruleDisplayer.GetComponent<RuleDisplay> ();
			r.rulesUpdated (this.rules);
		}

		this.gridEnabled = true;

	}

	public static string StringFromPosition(Vector2 position) {
		return Mathf.Round(position.x) + "-" + Mathf.Round(position.y);
	}
	public static Vector2 PositionFromString(string positionString) {
		Vector2 result = new Vector2 ();
		result.x = int.Parse(Regex.Match(positionString, @"^\d+").Value);
		result.y = int.Parse(Regex.Match(positionString, @"\d+$").Value);
		return result;
	}


	private void OnSubmitButtonClicked() {
		if (this.gridEnabled == false)
			return;
		this.gridEnabled = false;

		GridScoreResult scoreResult = this.processScore ();

		if (this.scoreScript) {
			this.scoreScript.UpdateScore (scoreResult.matches, scoreResult.matchesWithoutState);
		}

		var allPointsMatched = new Dictionary<Vector2, bool> ();
		foreach (var score in scoreResult.matches) {
			foreach (var cell in score.Value.pattern) {
				Vector2 key = cell.Key + score.Key;
				if (allPointsMatched.ContainsKey(key) == false) allPointsMatched.Add (key, true);
			}
		}
		foreach (var item in this.objectsByPosition) {
			if (item.Value.used == false)
				continue;
			if (allPointsMatched.ContainsKey (item.Key)) {
				item.Value.Hide(true);
			} else {
				item.Value.Hide(false);
			}
		}
		Invoke("populateGridAnimated", this.gridCompleteDisappearTime);
	}
	private GridScoreResult processScore () {
		
		var matches = new Dictionary<Vector2, Rule>();
		var matchesWithoutState = new Dictionary<Vector2, Rule>();

		/* For every rule, we check every item in the grid, that item being the top left anchor point of the rule.
		 * For every item in the grid, we keep checking cells in the rule until we find something that doesn't add up.
		 * If we don't find any irregularities, it's a perfect match. 
		 * Alternatively, we can also find combinations that could have worked, but didn't: we save those too.
		 */

		foreach (var rule in this.rules) {
			/*
			foreach (var cell in rule.pattern) {
				Debug.Log (cell);
			}
			*/
			foreach (var item in this.objectsByPosition) {
				if (item.Value.used == false) {
					continue;
				}
				
				var matchFound = true;
				var matchFoundWithoutState = true;
				foreach (var cell in rule.pattern) {
					var cellPosition = item.Key + cell.Key;
					var itemForCell = this.objectsByPosition [cellPosition];
					if (!itemForCell || itemForCell.used == false) {
						matchFound = false;
						matchFoundWithoutState = false;
						break;
					}
					if (itemForCell.itemGraphic.type != cell.Value.itemScript.type) {
						matchFound = false;
						matchFoundWithoutState = false;
						break;
					}
					if (itemForCell.state != cell.Value.state) {
						matchFound = false;
					}
				}
				if (matchFound == true) {
					matches.Add (item.Value.position, rule);
				} else if (matchFoundWithoutState == true) {
					matchesWithoutState.Add (item.Value.position, rule);
				}

			}
				
		}

		//Debug.Log ("=== MATCHES D-F " + matches.Count + " - " + matchesWithoutState.Count);
		/*
		Debug.Log ("=== MATCHES WITHOUT STATE");
		foreach (var item in matchesWithoutState) {
			Debug.Log (item.Key);
		}
		Debug.Log ("=== MATCHES");
		foreach (var item in matches) {
			Debug.Log (item.Key);
		}
		*/
		GridScoreResult result = new GridScoreResult ();
		result.matches = matches;
		result.matchesWithoutState = matchesWithoutState;
		return result;

	}

}
