using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoissonDiskResultHelper : MonoBehaviour {

	/// fot algorithm details please take a look at PoissonDiskGenerator.cs.
	public float minDistance = 5.0f;	// minimum distance between samples.
	public int k = 30;					// darting time. Higher number get better result but slower.
	public float sampleRange = 256.0f;	// the edge length of the squre area for generation.
	public int sampleCount = 0;			// number of the samples.
	public List<Vector2> result;		// the result of sample list.
	public Texture2D resultTexture;		// the texture2D with the samples on it.

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Space)) {
			Generate();
		}
	}

	void Generate(){
		PoissonDiskGenerator.minDist = minDistance;
		PoissonDiskGenerator.k = k;
		PoissonDiskGenerator.sampleRange = sampleRange;
		result = PoissonDiskGenerator.Generate();
		
		// create result texture.
		if(resultTexture !=null){
			if(resultTexture.width != (int)PoissonDiskGenerator.sampleRange){
				DestroyImmediate(resultTexture);
			}
			
		}
		resultTexture = new Texture2D((int)PoissonDiskGenerator.sampleRange, 
		                              (int)PoissonDiskGenerator.sampleRange);
		ClearTetxure(Color.white);
		sampleCount = PoissonDiskGenerator.sampleCount;
		for(int i = 0; i < sampleCount; ++i){
			resultTexture.SetPixel((int)result[i].x, (int)result[i].y, Color.black);
		}
		resultTexture.Apply();
	}

	void ClearTetxure(Color clearColor){
		if (resultTexture != null) {
			int w = resultTexture.width, h = resultTexture.height;
			for(int x = 0; x < w; ++x){
				for(int y = 0; y < h; ++y){
					resultTexture.SetPixel(x,y,clearColor);
				}
			}
			resultTexture.Apply();
		}
	}

	void OnGUI(){
		GUI.Label(new Rect(0,0,200,50),"Press space to generate samples.");

		if(resultTexture != null){
			GUI.DrawTexture (new Rect(0,0,resultTexture.width,resultTexture.height), resultTexture);
		}
	}
}
