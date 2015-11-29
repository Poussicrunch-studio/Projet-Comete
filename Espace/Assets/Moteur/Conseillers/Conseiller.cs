using System.Collections.Generic;
using AssemblyCSharp;
using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Conseiller
	{

		public int fiabilité = 0;
		public int education = 0;
		public int inventivité = 0;

		public CategoriesConseiller categorie;
		public List<TypeDeBatiment> batimentsDisponibles = new List<TypeDeBatiment>();
		public Proposition proposition;

		public Faction faction;

		public Conseiller (CategoriesConseiller categorie)
		{
			this.categorie = categorie;
			genererLesListesDeBatimentsDisponibles ();
			definirFactionAleatoire ();
			Debug.Log (faction.nom);
		}

		public void definirFactionAleatoire() {
			faction = DataManager.dataManager.getRandomFaction ();
		}


		private void genererLesListesDeBatimentsDisponibles() { //Un nom aussi long c'est mal
			foreach (TypeDeBatiment b in DataManager.dataManager.typesDeBatiment) {
				if (b.categorie == categorie) {
					batimentsDisponibles.Add(b);
				}
			}
			Debug.Log("Nombre de batiment pour ce conseiller : " + batimentsDisponibles.Count);
		}

		public void jouer() {
			proposition = new Proposition();
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
					if (test<=0) {
						proposition.addBatiment (new Batiment(b));
						break;
					}
			}
			}
		}
	}
}

