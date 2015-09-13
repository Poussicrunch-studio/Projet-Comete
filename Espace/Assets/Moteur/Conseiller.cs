using System.Collections.Generic;
using AssemblyCSharp;
using System;

namespace AssemblyCSharp
{
	public class Conseiller
	{

		public int fiabilité = 0;
		public int education = 0;
		public int inventivité = 0;
		public CategoriesConseiller categorie;
		public List<TypeDeBatiment> batimentsDisponibles = new List<TypeDeBatiment>();

		public Conseiller ()
		{
			genererLesListesDeBatimentsDisponibles ();

		}


		private void genererLesListesDeBatimentsDisponibles() { //Un nom aussi long c'est mal
			foreach (TypeDeBatiment b in DataManager.dataManager.typesDeBatiment) {
				if (b.categorie == categorie) {
					batimentsDisponibles.Add(b);
				}
			}
		}

		public void jouer() {


		}
	}
}

