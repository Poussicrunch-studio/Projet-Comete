using UnityEngine;
using System.Collections;

public class SC_GenerateurPlanete : MonoBehaviour {


	public Terrain terrain;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void generer() {
		TerrainData data = terrain.terrainData;
		float[, ,] alphas = data.GetAlphamaps(0, 0, data.alphamapWidth, data.alphamapHeight);

		for (int i = 0; i < data.alphamapWidth; i++) {
			for (int j = 0; j < data.alphamapHeight; j++) {
				if ((i+j)%2 == 0) {
					alphas[i, j, 1] = 1000;
					//alphas[i, j, 2] = 70;
					alphas[i, j, 0] = 0;
				}
				else {
					alphas[i, j, 0] = 1000;
					alphas[i, j, 1] = 0;
					//alphas[i, j, 2] = 70;

				}
			}
		}
		data.SetAlphamaps(0, 0, alphas);
	}
}
