using UnityEngine;
using System.Collections;
using AssemblyCSharp;
public class SC_GenerateurPlanete : MonoBehaviour {


	public Terrain terrain;
	public TerrainData data;
	public float dx;
	public float dz;
	public float lx;
	public float lz;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void generer(float lx, float lz) {
		this.lx = lx;
		this.lz = lz;
		data = terrain.terrainData;
		float[, ,] alphas = data.GetAlphamaps(0, 0, data.alphamapWidth, data.alphamapHeight);
		Debug.Log ("alpha width : " + data.alphamapWidth);
		Debug.Log ("alpha he : " + data.alphamapWidth);
		Debug.Log ("world size : " + data.size);
		Debug.Log ("distance between patch X : " + data.size[0] / data.alphamapWidth);
		Debug.Log ("distance between patch Y : " + data.size[1] / data.alphamapHeight);
		dx = data.size [0] / data.alphamapWidth;
		dz = data.size [2] / data.alphamapHeight;

		for (int i = 0; i < data.alphamapWidth; i++) {
			for (int j = 0; j < data.alphamapHeight; j++) {
				if ((i+j)%2 == 0) {
					//data.get
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
		changeHexagoneTexture (0, 0, 3);
	}

	public void changeHexagoneTexture(int x, int z, int textureID) {
		float[, ,] alphas = data.GetAlphamaps(0, 0, data.alphamapWidth, data.alphamapHeight);
		GameObject kase = GestionnaireDePartie.instance.obtenirCase (x, z).tuile;
		float cX = kase.transform.position.x;
		float cZ = kase.transform.position.z;
		float tX = terrain.transform.position.x;
		float tZ = terrain.transform.position.z;
		int patchMinX = (int) Mathf.Floor((cX - tX -(0.5f * lx)) / dx);
		int patchMinZ = (int) Mathf.Floor((cZ - tZ -(0.5f * lz)) / dz);
		int patchMaxX = (int) Mathf.Ceil((cX + (0.5f * lx) - tX) / dx);
		int patchMaxZ = (int) Mathf.Ceil((cZ + (0.5f * lz) - tZ) / dz);

		Debug.Log("dx: " + dx);
		Debug.Log("dz: " + dz);
		Debug.Log("Position hexagone X: " + kase.transform.position.x);
		Debug.Log("Position hexagone Z: " + kase.transform.position.z);
		Debug.Log("Position terrain X: " + terrain.transform.position.x);
		Debug.Log("Position terrain Z: " + terrain.transform.position.z);
		Debug.Log("Position terrain patchMinX: " + patchMinX);
		Debug.Log("Position terrain patchMaxX: " + patchMaxX);
		Debug.Log("Position terrain patchMinZ: " + patchMinZ);
		Debug.Log("Position terrain patchMaxZ: " + patchMaxZ);

	//	for (int i = 0; i < data.alphamapWidth; i++) {
	//		for (int j = 0; j < data.alphamapHeight; j++) {
		//2*v*h – v*q2.x -h*q2.y
		float v = lz / 4.0f;
		float h = lx / 2.0f;
		for (int i = patchMinX; i < patchMaxX; i++) {
			for (int j = patchMinZ; j < patchMaxZ; j++) {
				float newX = Mathf.Abs((i*dx) + terrain.transform.position.x - kase.transform.position.x);
				float newZ = Mathf.Abs((j*dz) + terrain.transform.position.z - kase.transform.position.z);
				Debug.Log("i,j" + i + j + " nX, nZ : " + newX + " " + newZ);
				if (2*v*h - v*newX - h*newZ >= 0) {
					alphas[j, i, 1] = 0;
					alphas[j, i, 2] = 0;
					alphas[j, i, 4] = 200;
					alphas[j, i, 0] = 0;
				}

			}
		}
		data.SetAlphamaps(0, 0, alphas);

	}

}
