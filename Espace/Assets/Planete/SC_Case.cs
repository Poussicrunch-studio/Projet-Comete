using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class SC_Case : MonoBehaviour {
		
	public Case kase;
	public Transform selecteur;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void selectionner() {
		Transform go = (Transform) MonoBehaviour.Instantiate(SC_GestionPlanete.instance.selecteurDeCase,
		                                                       new Vector3(0.0f, 0.0f, 0.0f),
		                                                     Quaternion.identity) as Transform;
		go.transform.SetParent(gameObject.transform);
		selecteur = go;

	}

	public void deselectionner() {
		Debug.Log ("desel");
		Destroy (selecteur.gameObject);
	}
}
