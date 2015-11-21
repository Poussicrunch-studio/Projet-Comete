using UnityEngine;
using AssemblyCSharp;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SC_PanelInfoProp : MonoBehaviour {

	public GameObject titreProposition;
	public GameObject bouttonDeValidation;
	public List<GameObject> panelBatiments = new List<GameObject>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateProposition(Proposition proposition) {

		foreach (GameObject go in panelBatiments) {
			Destroy(go);
		}
		panelBatiments.Clear();

		if (proposition != null && !proposition.estVide()) {
			bouttonDeValidation.SetActive(true);
			foreach (Batiment b in proposition.getBatiments()) {
				Transform t = (Transform) Instantiate(SC_GestionPlanete.instance.modelePanelDataBatiment) as Transform;
				t.SetParent(gameObject.transform, false);
				t.gameObject.GetComponent<SC_PanelDataBatiment>().mettreAJour(b);
				panelBatiments.Add (t.gameObject);
			}
		} else {
			bouttonDeValidation.SetActive(false);

		}

	}
}
