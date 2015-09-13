using System;
using System.Collections.Generic;


namespace AssemblyCSharp
{
	public class GestionnaireDePartie
	{

		List<Case> cases = new List<Case>(); /*Liste des cases du jeu*/
		int tour = 0;
		Joueur joueur;
		Colonie colonie;

		public static int maxXCases = 1000;
		public static int maxYCases = 1000;
		public Case[,] grille = new Case[maxXCases,maxYCases]; 

		public GestionnaireDePartie ()
		{
			joueur = new Joueur ();
			colonie = new Colonie ();
		}

		public void jouerUnTour() {
			foreach (Case kase in cases) {
				kase.jouer();
			}
			tour++;
		}



		//"case" est un mot clef du langage. On utilisera donc le merveilleux mot "kase".
		public void ajouterUneCase(Case kase) {
			cases.Add(kase);
			grille [kase.coordX + (maxXCases/2) , kase.coordY + (maxYCases/2)] = kase;
		}

		public Case obtenirCase(int x, int y) {
			return grille [x + (maxXCases / 2), y + (maxYCases / 2)];
		}
	}
}

