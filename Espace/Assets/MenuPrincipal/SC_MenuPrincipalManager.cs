using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class SC_MenuPrincipalManager : MonoBehaviour {

	void Start () {
	
		//Creation du gestionaire de données
		new DataManager ();


	}
	
	void Update () {
	
	}

	public void  loadLevel(){
		Application.LoadLevel("MenuCreationPartie");
	}
}
