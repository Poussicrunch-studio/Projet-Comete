using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AssemblyCSharp;

public class SC_PanelDataBatiment : MonoBehaviour {

	public Text titre;
	public Text texte;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void mettreAJour(Batiment bat) {
		titre.text = bat.getNom ();
		texte.text = bat.getDescription ();
	}
}
