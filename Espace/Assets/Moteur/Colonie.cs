using System;
using System.Collections.Generic;
using AssemblyCSharp;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Colonie
	{
		public Dictionary<Ressources, float> reserves = new Dictionary<Ressources, float>();
		public Dictionary<CategoriesConseiller,Conseiller> conseillers = new Dictionary<CategoriesConseiller,Conseiller>();
		public static Colonie instance;
		public List<Batiment> batimentsDisponibles = new List<Batiment> ();

		public Colonie ()
		{
			Debug.Log ("Creation de la colonie");

			instance = this;

			foreach (Ressources ressource in Enum.GetValues(typeof(Ressources))) {
				reserves.Add(ressource,0.0f);
				}

			reserves.Remove (Ressources.NOURRITURE);
			reserves.Add (Ressources.NOURRITURE,100.0F);

			reserves.Remove (Ressources.EAU);
			reserves.Add (Ressources.EAU,100.0F);

			reserves.Remove (Ressources.OXYGENE);
			reserves.Add (Ressources.OXYGENE,50.0F);

		//	for (int i = 0 ; i < Enum.GetValues(typeof(CategoriesConseiller).Le
			foreach (CategoriesConseiller c in Enum.GetValues(typeof(CategoriesConseiller))) {
				Conseiller conseiller = new Conseiller(c);
				conseillers.Add(c,conseiller);
			}
		}

		public void jouer() {
			foreach (CategoriesConseiller c in conseillers.Keys) {
				conseillers[c].jouer();
			}
		}

		public float getQuantiteDeRessource(Ressources r) {
			return reserves[r];
		}

		public void produire(Ressources r, float quantite) {
			reserves [r] += quantite;
			//Debug.Log ("Nouvelles réserves :" + reserves [r]);
		}
	}
}

