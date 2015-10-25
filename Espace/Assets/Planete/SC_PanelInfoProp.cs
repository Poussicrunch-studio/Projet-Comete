using UnityEngine;
using AssemblyCSharp;
using System.Collections;

public class SC_PanelInfoProp : MonoBehaviour {

	public GameObject titreProposition;
	public GameObject bouttonDeValidation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateProposition(Proposition proposition) {

		if (proposition != null && !proposition.estVide()) {
			bouttonDeValidation.SetActive(true);
		} else {
			bouttonDeValidation.SetActive(false);
		}

	}
}
