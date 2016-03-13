using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class SC_Case : MonoBehaviour {
		
	public Case kase;
	public Transform selecteur;
	public GameObject surcase;
	public bool isVisible;

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
		go.transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
		go.transform.rotation = transform.rotation;

		selecteur = go;

	}

	public void deselectionner() {
		Destroy (selecteur.gameObject);
	}

	public void OnBecameVisible() {
		isVisible = true;
		SC_GestionPlanete.instance.casesVisibles.Add (this);
	}

	public void OnBecameInvisible() {
		isVisible = false;
		SC_GestionPlanete.instance.casesVisibles.Remove (this);
	
	}
}
