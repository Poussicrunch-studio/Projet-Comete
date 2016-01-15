using System;
using UnityEngine;
using AssemblyCSharp;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

namespace AssemblyCSharp
{
	public class Case
	{
		public Terrains terrain;  //c'est jacques !!!
		public GameObject tuile;
		List<Case> cases = new List<Case> ();
		Batiment batiment;
		public int coordX, coordZ;
		public static Case caseSelectionnee;
		public Dictionary<ValeurDeCase,double> valeurs = new Dictionary<ValeurDeCase, double> ();

		public Case (GameObject tuile, int coordX, int coordZ)
		{
			this.coordX = coordX;
			this.coordZ = coordZ;
			this.tuile = tuile;

			foreach (ValeurDeCase vdc in DataManager.dataManager.listeValeursDeCase) {
				valeurs.Add(vdc,0);
			}

		}

		public void changerTerrain(Terrains t, bool cache) {
			if (cache) {
				SC_GenerateurPlanete.instance.changeHexagoneTextureCache (coordX,coordZ,t);
			} else {
				SC_GenerateurPlanete.instance.changeHexagoneTexture (coordX,coordZ,t);
			}
		}

		public List<Case> getCasesVoisines() {
			return cases;
		}


		//Joue le tour de la case
		public void jouer() {
			if (possedeUnBatiment ()) {
				batiment.jouer ();
			}
		}

		public void selectionner() {
			if (caseSelectionnee != null) {
				caseSelectionnee.deselectionner();
			}
			caseSelectionnee = this;
			tuile.GetComponent<SC_Case>().selectionner();
		}

		public void deselectionner() {
			tuile.GetComponent<SC_Case>().deselectionner();
		}

		public Boolean possedeUnBatiment() {
			return batiment != null;
		}

		public bool estImpaire() {
			return coordX%2==1;
		}

		public void construire(Batiment b) {
			batiment = b;
			GameObject go = (GameObject) MonoBehaviour.Instantiate(b.type.prefab, 
			                                                       new Vector3(SC_GestionPlanete.getXofHexagone(coordX,coordZ), 0.0f, SC_GestionPlanete.getYofHexagone(coordX,coordZ)),
			                          Quaternion.identity);
			SC_DataForPrefab script = go.GetComponent<SC_DataForPrefab>();

			go.transform.Rotate(new Vector3(1,0,0) * script.rotationX, Space.World);
			go.transform.Translate (new Vector3(script.decalageEnX,script.decalageEnY,script.decalageEnZ));
			changerTerrain (Terrains.MONTAGNE, false);
			//t.parent = tuile;
		}
	}
}

