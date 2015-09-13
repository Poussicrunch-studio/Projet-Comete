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
		public List<Evenement> listEvenement = new List<Evenement>();
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
			//doc.Load (dataPath + "/Batiments.xml");
			doc.Load (dataPath + "/evenements.xml");
			XmlNodeList nodes = doc.DocumentElement.ChildNodes;
			foreach (XmlNode node in nodes) 
			{
				Evenement a = new Evenement();
				a.nom = node.SelectSingleNode ("Nom").InnerText;
				a.description = node.SelectSingleNode ("description").InnerText;
				a.choix = bool.Parse(node.SelectSingleNode ("choix").InnerText);
				a.choix1 = node.SelectSingleNode ("choix1").InnerText;
				a.choix2 = node.SelectSingleNode ("choix2").InnerText;
				a.choix3 = node.SelectSingleNode ("choix3").InnerText;
				foreach(XmlNode node2 in node.SelectSingleNode ("terrain"))
				{
					ConditionTerrain b = new ConditionTerrain();
					b.type = Terrains.Parse(node2.SelectSingleNode ("type").InnerText);
					b.requis = bool.Parse (node2.SelectSingleNode ("requis").InnerText);
					b.poid = int.Parse (node2.SelectSingleNode ("poid").InnerText);
					a.conditionT.Add (b);
				}
				listEvenement.Add (a);
					
			}

			
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

