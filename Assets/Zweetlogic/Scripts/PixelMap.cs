using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PixelMap {
	public Dictionary<Vector2, bool> pixelmapFromTexture(Texture2D texture) {
		Dictionary<Vector2, bool> result = new Dictionary<Vector2, bool> ();
	
		Color32[] colors = texture.GetPixels32 ();
		int total = colors.Length;
		for (int i = 0; i < total; i++) {
			Color32 color = colors [i];

			Vector2 position = new Vector2 ();
			position.x = i % texture.width;
			position.y = Mathf.FloorToInt(i / texture.width);

			/* 	This sets enabled to tiles that are BLACK (rather than white). 
				If you want to reverse this, reverse the operator.
			*/
			bool enabled = color.a < 128;

			result.Add (position, enabled);
		}

		return result;
	}
}
