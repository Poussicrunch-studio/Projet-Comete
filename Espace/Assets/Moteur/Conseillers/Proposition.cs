using AssemblyCSharp;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	/**
	 * une proposition faite par un conseiller.
	 * Contient généralement un unique batiment. 
	 */
	public class Proposition
	{
		public List<Batiment> batiments = new List<Batiment>();

		public Proposition ()
		{

		}

		public void addBatiment(Batiment b) {
			batiments.Add(b);
		}

		public List<Batiment> getBatiments() {
			return batiments;
		}

		public bool estVide() {
			return batiments.Count == 0;
		}
	}
}

