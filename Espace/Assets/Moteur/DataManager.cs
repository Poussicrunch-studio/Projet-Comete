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

		private System.Random random;
		private String langage = "Fr";

		/*----Data----*/
		public List<TypeDeBatiment> typesDeBatiment = new List<TypeDeBatiment>();
		public List<Evenement> listEvenement = new List<Evenement>();
		public List<Faction> listeFactions = new List<Faction>();
		public List<ValeurDeCase> listeValeursDeCase = new List<ValeurDeCase>();
		public List<TypeColon> listeTypesColon = new List<TypeColon>();
		public List<Dialogue> listeDialogue = new List<Dialogue>();
		/*------------*/

		public DataManager ()
		{
			dataManager = this;
			random = new System.Random ();
			load ();
		}


		public void load() {
			dataPath = Application.dataPath + "/Data";

			Debug.Log ("Démarrage du chargement des données de jeu...");
			Debug.Log (Application.dataPath + "/Data");

			//Chargement des autres fichiers
			chargerValeurDeCase ();
			loadColons ();
			loadBatiments ();
			loadFactions ();
			loadDialogues ();


			XmlDocument doc = new XmlDocument ();
			//doc.Load (dataPath + "/Batiments.xml");
			doc.Load (dataPath + "/Evenements.xml");
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
				a.entropie = int.Parse (node.SelectSingleNode ("entropie").InnerText);
				XmlNodeList nodes2 = node.SelectNodes("terrain");
				foreach(XmlNode node2 in nodes2)
				{
					//a.category = (ActionCategory) Enum.Parse (typeof(ActionCategory), node.SelectSingleNode ("Category").InnerText);
					ConditionTerrain b = new ConditionTerrain();
					b.type = (Terrains) Enum.Parse(typeof(Terrains), node2.SelectSingleNode ("type").InnerText);
					b.requis = bool.Parse (node2.SelectSingleNode ("requis").InnerText);
					b.poid = int.Parse (node2.SelectSingleNode ("poid").InnerText);
					a.conditionT.Add (b);
				}
				nodes2 = node.SelectNodes("ressource");
				foreach(XmlNode node2 in nodes2)
				{
					//a.category = (ActionCategory) Enum.Parse (typeof(ActionCategory), node.SelectSingleNode ("Category").InnerText);
					ConditionRessource b = new ConditionRessource();
					b.type = (Ressources) Enum.Parse(typeof(Ressources), node2.SelectSingleNode ("type").InnerText);
					b.requis = bool.Parse (node2.SelectSingleNode ("requis").InnerText);
					b.combien = int.Parse (node2.SelectSingleNode ("combien").InnerText);
					a.conditionR.Add (b);
				}
				listEvenement.Add (a);
					
			}

			
			Debug.Log ("Fin du chargement des données de jeu...");
		}

		public void loadBatiments() {
			XmlDocument doc = new XmlDocument ();
			doc.Load (dataPath + "/Batiments.xml");
			XmlNodeList nodes = doc.DocumentElement.ChildNodes;
			foreach (XmlNode node in nodes) {
				TypeDeBatiment b = new TypeDeBatiment();
				b.poids = int.Parse (node.SelectSingleNode ("Poids").InnerText);
				b.niveau = int.Parse (node.SelectSingleNode ("Niveau").InnerText);
				b.nom = node.SelectSingleNode ("Nom").InnerText;
				b.description = node.SelectSingleNode ("Description_" + langage).InnerText;
				Debug.Log (node.SelectSingleNode ("Categorie").InnerText);
				if (node.SelectSingleNode ("Categorie").InnerText != "NULL") {
					b.categorie = (CategoriesConseiller) Enum.Parse (typeof(CategoriesConseiller), node.SelectSingleNode ("Categorie").InnerText);
				}
				b.prefab = globalPrefabs.getPrefab(node.SelectSingleNode("Prefab").InnerText);

				//Chargement des effets
				XmlNodeList nodesEffet = node.SelectNodes("Effet");
				foreach (XmlNode nodeEffet in nodesEffet) {
					Debug.Log("Je charge l'effet : " + nodeEffet.InnerText);
					chargerUnEffet(nodeEffet,b);
				}

				typesDeBatiment.Add(b);
			}

		}

		public void chargerValeurDeCase() {
			XmlDocument doc = new XmlDocument ();
			doc.Load (dataPath + "/ValeursDeCase.xml");
			XmlNodeList nodes = doc.DocumentElement.ChildNodes;
			foreach (XmlNode node in nodes) {
				ValeurDeCase vdc = new ValeurDeCase();
				vdc.nom = node.SelectSingleNode ("Nom").InnerText;

				listeValeursDeCase.Add(vdc);
			}
		}

		public void loadFactions() {
			XmlDocument doc = new XmlDocument ();
			doc.Load (dataPath + "/Factions.xml");
			XmlNodeList nodes = doc.DocumentElement.ChildNodes;
			foreach (XmlNode node in nodes) {
				Faction f = new Faction();
				f.nom = node.SelectSingleNode ("Nom").InnerText;

				f.sprite = Resources.Load<Sprite>("SPRITE/Personnages/" + node.SelectSingleNode ("Sprite").InnerText);
			//	SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
			//	spriteRenderer.sprite = Resources.Load<UnityEngine.Sprite>("Textures/Interface/button");
				
				listeFactions.Add(f);
			}

		}

		public void loadDialogues() {
			XmlDocument doc = new XmlDocument ();
			doc.Load (dataPath + "/Dialogues.xml");
			XmlNodeList nodes = doc.DocumentElement.ChildNodes;
			foreach (XmlNode node in nodes) {
				Dialogue d = new Dialogue();
				d.texte = node.SelectSingleNode ("Texte").InnerText;
				d.poids = double.Parse (node.SelectSingleNode ("Poids").InnerText);
				if (node.SelectSingleNode ("Type").InnerText != "NULL") {
					d.type = (TypeDeDialogue) Enum.Parse (typeof(TypeDeDialogue), node.SelectSingleNode ("Type").InnerText);
				}
				d.faction = this.getFaction(node.SelectSingleNode ("Faction").InnerText);

				listeDialogue.Add(d);
			}

		}


		public void loadColons() {
			XmlDocument doc = new XmlDocument ();
			doc.Load (dataPath + "/Colons.xml");
			XmlNodeList nodes = doc.DocumentElement.ChildNodes;
			foreach (XmlNode node in nodes) {
				TypeColon tc = new TypeColon();
				tc.nom = node.SelectSingleNode ("Nom").InnerText;

				listeTypesColon.Add(tc);
			}

		}


		private void chargerUnEffet(XmlNode node, TypeDeBatiment b) {
			String type = node.SelectSingleNode ("Type").InnerText;
			Effet effet = null;

			if (type.Equals ("PRODUIRE")) {
				EffetProduire e = new EffetProduire();
				e.quantite = float.Parse(node.SelectSingleNode ("Quantite").InnerText);
				e.ressource = (Ressources) Enum.Parse (typeof(Ressources), node.SelectSingleNode ("Ressource").InnerText);
				effet = e;
			}

			b.ajouterEffet(effet);
		}

		public TypeDeBatiment getTypeDeBatiment(String nom) {
			foreach (TypeDeBatiment b in typesDeBatiment) {
				if (b.nom.Equals(nom)) {
					return b;
				}
			}
			return null;
		}

		public Faction getFaction(String nom) {
			foreach (Faction f in listeFactions) {
				if (f.nom.Equals(nom)) {
					return f;
				}
			}
			return null;
		}

		public Faction getRandomFaction() {
			return listeFactions[random.Next(listeFactions.Count)];
		}

		public TypeColon getRandomColon() {
			return listeTypesColon[random.Next(listeTypesColon.Count)];
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

