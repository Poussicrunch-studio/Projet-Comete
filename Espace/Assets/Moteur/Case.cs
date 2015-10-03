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
		GameObject tuile;
		List<Case> cases = new List<Case> ();
		Batiment batiment;
		public int coordX, coordY;

		public Case (GameObject tuile, int coordX, int coordY)
		{
			this.coordX = coordX;
			this.coordY = coordY;
			this.tuile = tuile;
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

		public Boolean possedeUnBatiment() {
			return batiment != null;
		}

		public bool estImpaire() {
			return coordX%2==1;
		}

		public void construire(Batiment b) {
			batiment = b;
			GameObject go = (GameObject) MonoBehaviour.Instantiate(b.type.prefab, 
			                                                       new Vector3(SC_GestionPlanete.getXofHexagone(coordX,coordY), SC_GestionPlanete.getYofHexagone(coordX,coordY), 0.0f),
			                          Quaternion.identity);
			SC_DataForPrefab script = go.GetComponent<SC_DataForPrefab>();

			go.transform.Rotate(new Vector3(1,0,0) * script.rotationX, Space.World);
			go.transform.Translate (new Vector3(script.decalageEnX,script.decalageEnY,script.decalageEnZ));
			//t.parent = tuile;
		}
	}
}

