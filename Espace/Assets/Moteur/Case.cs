using System;
using UnityEngine;
using AssemblyCSharp;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class Case
	{
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
	}
}

