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
			int poidTotal=0;
			foreach (TypeDeBatiment b in batimentsDisponibles){
				poidTotal += b.poids;
			}
			System.Random rnd =new System.Random();
			int test = rnd.Next(0,poidTotal);
			TypeDeBatiment enCours = new TypeDeBatiment();
			while(test>0){
			foreach (TypeDeBatiment b in batimentsDisponibles){
				test -= b.poids;
					if (test<=0)
						break;
			}
			}
		}
	}
}

