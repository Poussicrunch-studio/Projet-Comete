using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class SC_GestionPlanete : MonoBehaviour {

	public Transform modeleTuile;
	public Transform modelePanelDataBatiment;

	static public float rotationXTuile = 0.0f;
	static public float rotationYTuile = 0.0f;
	static public float rotationZTuile = 0.0f;

	static public float longueurXTuile = 0.0f;
	static public float longueurZTuile = 0.0f;

	public GestionnaireDePartie gestionnaire; /*Le gestionnaire s'occupe de gerer tout le jeu "interne", pas les graphismes*/
	public static SC_GestionPlanete instance;

	//Pour l'interface
	public Case kaseSelectionnee;
	public Text xDeLaCase;
	public Text yDeLaCase;
	public Canvas canvasActions;
	public GameObject panelPropositions;
	public GameObject panelInfoPropositions;
	public GameObject panelSelectionBatiment;
	public GameObject panelInfoBatiment;

	public SC_PanelInfoProp scriptPanelInfoPropositions;

	//Ressources et population pour l'IU
	public Text infoNourriture;
	public Text infoOxygene;
	public Text infoEau;
	public Text infoPopulation;
	public Text infoPopulationEnStase;
	public Text infoCredit;
	public Text infoBiens;
	public Text infoEnergie;
	public Text infoMinerai;

	//Pour le canvas des conseillers
	Dictionary<CategoriesConseiller, TypeDeBatiment> batimentsProposes = new Dictionary<CategoriesConseiller, TypeDeBatiment>();
	public Button bGeneral;
	public Button bMinistre;
	public Button bExplorateur;
	public Button bScientifique;
	public Button bGouverneur;

	public Dropdown dValeurDeCaseObservable;

	public Image iGeneral;
	public Image iMinistre;
	public Image iExplorateur;
	public Image iScientifique;
	public Image iGouverneur;

	public int tailleDuMondeX = 50;
	public int tailleDuMondeZ = 50;
	
	
	//Pour le mode construction
	public Batiment batimentEnCoursDeConstruction = null;

	public Transform selecteurDeCase;

	//Etat du jeu
	public bool propositionEnCours = false;
	public bool valeurDeCaseEstAffichee = false;
	public Proposition propositionActuelle;

	public SC_GenerateurPlanete generateur;

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
		globalPrefabs.LoadAll ("PREFAB");
		gestionnaire = new GestionnaireDePartie ();

		longueurXTuile = modeleTuile.GetComponent<Renderer>().bounds.size.x;
		longueurZTuile = modeleTuile.GetComponent<Renderer>().bounds.size.z;

		instance = this;

		creerLesCasesInitiales ();

		generateur.generer (longueurXTuile,longueurZTuile);
		gestionnaire.obtenirCase (0, 0).construire (new Batiment(DataManager.dataManager.getTypeDeBatiment("Vaisseau")));
		generateur.creerMondeAleatoire (gestionnaire.cases);

		iGeneral.sprite = Colonie.instance.conseillers [CategoriesConseiller.GENERAL].faction.sprite;
		iScientifique.sprite = Colonie.instance.conseillers [CategoriesConseiller.SCIENTIFIQUE].faction.sprite;
		iExplorateur.sprite = Colonie.instance.conseillers [CategoriesConseiller.EXPLORATEUR].faction.sprite;
		iGouverneur.sprite = Colonie.instance.conseillers [CategoriesConseiller.GOUVERNEUR].faction.sprite;
		iMinistre.sprite = Colonie.instance.conseillers [CategoriesConseiller.MINISTRE].faction.sprite;
	
		initialiserUI ();
	}

	private void initialiserUI() {
		List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
		Debug.Log("Initialisation de l'UI");

		foreach (ValeurDeCase vdc in DataManager.dataManager.listeValeursDeCase) {
			Debug.Log(vdc.nom);
			options.Add(new Dropdown.OptionData(vdc.nom));
		}
		dValeurDeCaseObservable.AddOptions(options);

	}


	private void creerLesCasesInitiales() {
		for (int i = tailleDuMondeX / 2 * (-1); i < tailleDuMondeX / 2; i++) {
			for (int j = tailleDuMondeZ / 2 * (-1); j < tailleDuMondeZ / 2; j++) {
				instancierUnHexagone (i, j);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		gererLaSouris ();
		gererLesInfos ();

	}

	public void switchAffichageValeurDeCase() {
		valeurDeCaseEstAffichee = !valeurDeCaseEstAffichee;
		Debug.Log ("Switch to : " + valeurDeCaseEstAffichee);
		mettreAJourDesSurcases ();
	}

	public void mettreAJourDesSurcases() {
		if (valeurDeCaseEstAffichee) {
			GameObject[] surcases = GameObject.FindGameObjectsWithTag("Surcase");
			foreach (GameObject go in surcases) {
				Debug.Log("surcase");
				go.GetComponent<Renderer>().material.SetColor("_SpecColor", Color.red);
			
			}
		}

	}

	public void selectionnerPropositionMINISTRE() {
		selectionnerUneProposition (CategoriesConseiller.MINISTRE);
	}

	public void selectionnerPropositionGENERAL() {
		selectionnerUneProposition (CategoriesConseiller.GENERAL);
	}

	public void selectionnerPropositionGOUVERNEUR() {
		selectionnerUneProposition (CategoriesConseiller.GOUVERNEUR);
	}

	public void selectionnerPropositionSCIENTIFIQUE() {
		selectionnerUneProposition (CategoriesConseiller.SCIENTIFIQUE);
	}

	public void selectionnerPropositionEXPLORATEUR() {
		selectionnerUneProposition (CategoriesConseiller.EXPLORATEUR);
	}
	public void selectionnerUneProposition(CategoriesConseiller c) {
		Debug.Log ("Categorie sélectionnée : " + c.ToString());
		propositionActuelle = gestionnaire.colonie.conseillers[c].proposition;
		scriptPanelInfoPropositions.updateProposition (propositionActuelle);
	}

	public void validerProposition() {
		if (propositionActuelle.getBatiments().Count > 0) {
			Batiment bat = propositionActuelle.getBatiments()[0];
			batimentEnCoursDeConstruction = bat;
			Colonie.instance.batimentsDisponibles.Add(bat);
			propositionEnCours = false;
			panelPropositions.SetActive(false);
			panelInfoPropositions.SetActive(false);
			//panelInfoBatiment.SetActive(true);
			//panelSelectionBatiment.SetActive(true);
		}
	}


	private void afficherLesPropositions() {
		panelPropositions.SetActive(true);
		panelInfoPropositions.SetActive(true);
	}

	private void gererLesInfos() {
		infoNourriture.text = "Nourriture : " + gestionnaire.colonie.getQuantiteDeRessource (Ressources.NOURRITURE).ToString();
		infoEau.text = "Eau : " + gestionnaire.colonie.getQuantiteDeRessource (Ressources.EAU).ToString();
		infoOxygene.text = "Oxygène : " + gestionnaire.colonie.getQuantiteDeRessource (Ressources.OXYGENE).ToString();
		infoCredit.text = "Credits : " + gestionnaire.colonie.getQuantiteDeRessource (Ressources.CREDITS).ToString();
		infoMinerai.text = "Minerai : " + gestionnaire.colonie.getQuantiteDeRessource (Ressources.MINERAI).ToString();
		infoBiens.text = "Biens : " + gestionnaire.colonie.getQuantiteDeRessource (Ressources.BIENS).ToString();
		infoEnergie.text = "Energie : " + gestionnaire.colonie.getQuantiteDeRessource (Ressources.ENERGIE).ToString();
		infoPopulationEnStase.text = "En stase : " + gestionnaire.colonie.colonsEnStase.Count.ToString();
	}


	public void jouerUnTour() {
		Debug.Log ("Fin du tour");
		gestionnaire.jouerUnTour ();
		canvasActions.enabled = true;
		propositionEnCours = true;
		propositionActuelle = null;
		scriptPanelInfoPropositions.updateProposition (propositionActuelle);
		afficherLesPropositions ();
		panelInfoBatiment.SetActive(false);
		panelSelectionBatiment.SetActive(false);
	
	}



	public void jouerSonAction() {
		canvasActions.enabled = false;
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
					yDeLaCase.text = kaseSelectionnee.coordZ.ToString();
					kaseSelectionnee.selectionner();
					if (batimentEnCoursDeConstruction != null && !kaseSelectionnee.possedeUnBatiment()) {
						construire (kaseSelectionnee);
					}
				}
			}
		} 
	}

	public void construire(Case kase) {
		kase.construire (batimentEnCoursDeConstruction);
		batimentEnCoursDeConstruction = null;
	}

	public void instancierUnHexagone(int x, int y) {

		//Si la colonne est impaire, -0,5y
		float yModifie = 0.0f;
		if (x % 2 != 0) {
			yModifie = 0.5f * longueurZTuile;
		}


		Transform t = Instantiate(modeleTuile, new Vector3(getXofHexagone(x,y), 0.0f , getYofHexagone(x,y)), Quaternion.identity) as Transform;
		t.transform.Rotate(new Vector3(1,0,0) * 90.0f, Space.World);

		Case kase = new Case (t.gameObject,x,y);
		gestionnaire.ajouterUneCase (kase);
		SC_Case script = t.gameObject.GetComponent<SC_Case>();
		script.kase = kase;
	}

	static public float getXofHexagone(int x, int y) {
		return x * longueurXTuile * (3.0f / 4.0f);
	}

	static public float getYofHexagone(int x, int y) {
		float yModifie = 0.0f;
		if (x % 2 != 0) {
			yModifie = 0.5f * longueurZTuile;
		}
		return y * longueurZTuile + yModifie;
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

		instancierUnHexagone (caseInitiale.coordX + decalX, caseInitiale.coordZ + decalY);


	}
}
