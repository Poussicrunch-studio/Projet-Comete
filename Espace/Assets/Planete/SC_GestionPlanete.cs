﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AssemblyCSharp;

public class SC_GestionPlanete : MonoBehaviour {

	public Transform modeleTuile;

	public float rotationXTuile = 0.0f;
	public float rotationYTuile = 0.0f;
	public float rotationZTuile = 0.0f;

	public float longueurXTuile = 0.7f;
	public float longueurYTuile = 1.0f;

	public GestionnaireDePartie gestionnaire; /*Le gestionnaire s'occupe de gerer tout le jeu "interne", pas les graphismes*/

	//Pour l'interface
	public Case kaseSelectionnee;
	public Text xDeLaCase;
	public Text yDeLaCase;

	//Ressources et population pour l'IU
	public Text infoNourriture;
	public Text infoOxygene;
	public Text infoEau;
	public Text infoPopulation;
	public Text infoCredit;
	public Text infoBiens;
	public Text infoEnergie;
	public Text infoMinerai;

	

	/* Je propose une terminologie pour manipuler les hexagones :
	 * l'hexagone++ d'un hexagone donné est celui positionné en haut à droite (+x+y)
	 * Le += juste à droite (+x=y), et ainsi de suite.
	 * Le += est toujours le premier de nos listes, et on tourne toujours dans le sens trigonomètrique.
	 * Si on liste les six voisins, on obtient alors :
	 * n°0 : +=
	 * n°1 : ++
	 * n°2 : -+
	 * n°3 : -=
	 * n°4 : --
	 * n°5 : +-
	 * 
	 * 
	 * L'hexagone de départ est le 0,0
	 * Les lignes paires (incluant le 0) serve de base au Y, les impaires prennent le y décallé vers la droite! Yeurk...
	 */

	// Use this for initialization
	void Start () {
		gestionnaire = new GestionnaireDePartie ();

		creerLesCasesInitiales ();
	}



	private void creerLesCasesInitiales() {
		instancierUnHexagone (1, 0);
		instancierUnHexagone (0, 1);
		instancierUnHexagone (-1, 0);
		instancierUnHexagone (0, -1);
		instancierUnHexagone (1, -1);
		instancierUnHexagone (-1, -1);
		instancierUnHexagone (0, 0);


	}
	
	// Update is called once per frame
	void Update () {
		gererLaSouris ();

		infoNourriture.text = gestionnaire.colonie.getQuantiteDeRessource ("Nourriture : " + Ressources.NOURRITURE).ToString();
	}

	public void jouerUnTour() {
		Debug.Log ("Fin du tour");
		gestionnaire.jouerUnTour ();
	}


	public void gererLaSouris() {
		if (Input.GetMouseButtonDown(0))
		{

			RaycastHit hitInfo = new RaycastHit();
			bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if (hit) 
			{
				Debug.Log("Hit " + hitInfo.transform.gameObject.name);
				if (hitInfo.transform.gameObject.tag == "Case")
				{
					kaseSelectionnee = hitInfo.transform.gameObject.GetComponent<SC_Case>().kase;
					xDeLaCase.text = kaseSelectionnee.coordX.ToString();
					yDeLaCase.text = kaseSelectionnee.coordY.ToString();
				}
			}
		} 
	}


	public void instancierUnHexagone(int x, int y) {

		//Si la colonne est impaire, -0,5y
		float yModifie = 0.0f;
		if (x % 2 != 0) {
			yModifie = 0.5f * longueurYTuile;
		}


		Transform t = Instantiate(modeleTuile, new Vector3(x * longueurXTuile, y * longueurYTuile + yModifie, 0.0f), Quaternion.identity) as Transform;
		Case kase = new Case (t.gameObject,x,y);
		gestionnaire.ajouterUneCase (kase);
		SC_Case script = t.gameObject.GetComponent<SC_Case>();
		script.kase = kase;
	}
	
	public void instancierUnHexagone (int position, Case caseInitiale ) {
		int decalX, decalY;

		switch (position) {
		case(0) : decalX = 1;
			decalY = 0;
			break;
		case(1) : decalX = 1;
			decalY = 1;
			break;
		case(2) : decalX = (-1);
			decalY = 1;
			break;
		case(3) : decalX = (-1);
			decalY = 0;
			break;
		case(4) : decalX = (-1);
			decalY = (-1);
			break;
		case(5) : decalX = 1;
			decalY = (-1);
			break;

		default:
			decalX = 0;
			decalY = 0;
			break;
		}

		instancierUnHexagone (caseInitiale.coordX + decalX, caseInitiale.coordY + decalY);


	}
}
