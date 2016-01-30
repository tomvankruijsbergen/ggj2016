using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SpriteSheetNG : MonoBehaviour
{
	public int offset = 0;
    public int Columns = 5;

    public float FramesPerSecond = 10f;
    public Vector2 textureSize = Vector2.one;

  
    void Start()
    {
		index = offset;
		deltaT = 1f / FramesPerSecond;
        InitializeTexture();
    }

	float t = 0;
	int index = 0;
	float deltaT;

    void Update()
    {
        if (Application.isEditor && !Application.isPlaying) {

            InitializeTexture();     
        }      

		t += Time.deltaTime;

		if (t >= deltaT) {
			t = 0;
			NextSprite ();
		}
	
	}

	private Vector2 textureOffset = Vector2.zero;

	private void NextSprite(){

		index++;
		if (index >= Columns)
			index = 0;
	
		textureOffset.x = (float)index / (float)Columns;
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", textureOffset);
	}


	Material materialCopy;

    private void InitializeTexture()
    {
        // Copy its material to itself in order to create an instance not connected to any other
        //materialCopy = new Material(GetComponent<Renderer>().sharedMaterial);
        //GetComponent<Renderer>().sharedMaterial = materialCopy;

        Vector2 size = new Vector2(1f / Columns, 1f);
        GetComponent<Renderer>().sharedMaterial.SetTextureScale("_MainTex", size);
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", Vector2.zero);

        float _sw = transform.localScale.x;
        transform.localScale = new Vector3(_sw, _sw * (textureSize.y / textureSize.x), 1);
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", Vector2.zero);
    }

   
}