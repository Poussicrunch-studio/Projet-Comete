using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SC_BulleDeDialogue : MonoBehaviour {


	public Text texte;
	public double tempsDeVie;

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
