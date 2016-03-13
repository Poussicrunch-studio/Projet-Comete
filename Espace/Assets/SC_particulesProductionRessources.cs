using UnityEngine;
using System.Collections;

public class SC_particulesProductionRessources : MonoBehaviour {

	public double tempsDeVie;
	public ParticleSystem particules;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		tempsDeVie -= Time.deltaTime;
		if (tempsDeVie <= 0) {
			GameObject.Destroy (gameObject);
		}
	}
}
