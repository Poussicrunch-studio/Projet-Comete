using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
	/**
	 * Cette classe super importante va s'occuper de charger et mémoriser toutes les infos stockées dans des fichiers textes.
	 */

	public class DataManager
	{
		public static DataManager dataManager; //Permet l'accès aux data n'importe où dans l'application! (On fait pas de l'ingé-log ici :p)
		public static string dataPath;


		/*----Data----*/
		public List<TypeDeBatiment> typesDeBatiment = new List<TypeDeBatiment>();
		/*------------*/

		public DataManager ()
		{
			dataManager = this;
			load ();
		}


		public void load() {
			dataPath = Application.dataPath + "/Data";

			Debug.Log ("Démarrage du chargement des données de jeu...");
			Debug.Log (Application.dataPath + "/Data");
			XmlDocument doc = new XmlDocument ();
			doc.Load (dataPath + "/Batiments.xml");

			
			Debug.Log ("Fin du chargement des données de jeu...");
		}


		/**
		 * 		public List<PoliticOrientation> loadPoliticOrientation() {

			Adaptator.println ("Loading politic orientations...");

			List<PoliticOrientation> politicOrientations = new List<PoliticOrientation> ();

			XmlDocument doc = new XmlDocument();
			doc.Load(path+"/PoliticOrientation.xml");

			XmlNodeList nodes = doc.DocumentElement.ChildNodes;

			foreach (XmlNode node in nodes) {
				PoliticOrientation p = new PoliticOrientation ();
				p.name = node.SelectSingleNode ("Name").InnerText;
				//Adaptator.println (p.name);
				politicOrientations.Add (p);
			}

			return politicOrientations;
			*/
	}
}

