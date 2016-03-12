using System;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp
{
	public class GestionnaireDeDialogue
	{
		public List<Dialogue> listeDialogue = new List<Dialogue>();
		public SC_GestionPlanete gestionnaire;
		public double poidsTotal = 0.0;

		public float tamponTemps = 0.0f;
		public float tempsEntreDeuxDialogues = 5.0f;

		public GestionnaireDeDialogue (SC_GestionPlanete gestionnaire)
		{
			this.gestionnaire = gestionnaire;
			foreach (Dialogue d in DataManager.dataManager.listeDialogue) {
				if (Colonie.instance.factionEstPresente (d.faction)) {
					listeDialogue.Add (d);
				}
			}
		}

		public void mettreAJour() {
			tamponTemps += Time.deltaTime;
			if (tamponTemps > tempsEntreDeuxDialogues) {
				tamponTemps -= tempsEntreDeuxDialogues;
				Dialogue d = selectionnerUnDialogue ();
				Debug.Log (d.texte);
				gestionnaire.creerUnDialogue (d);
			}
		}

		public Dialogue selectionnerUnDialogue() {
			Debug.Log ("Creer un dialogue");
			poidsTotal = calculerPoidsTotal ();
			Debug.Log ("Poids total : " + poidsTotal);
			System.Random rnd = new System.Random();
			double tirageAleatoire = rnd.Next (0, (int) poidsTotal);
			foreach (Dialogue d in listeDialogue) {
				double poids = d.getPoids ();
				if (tirageAleatoire <= poids) {
					return d;
				} else {
					tirageAleatoire -= poids;
				}
			}
			return null;
			//TODO
		}


		public double calculerPoidsTotal() {
			double nouveauPoids = 0.0;
			foreach (Dialogue d in listeDialogue) {
				nouveauPoids += d.getPoids ();
			}

			return nouveauPoids;
		}
	}
}

