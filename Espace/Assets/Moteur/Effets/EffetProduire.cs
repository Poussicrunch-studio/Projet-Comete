using System;
using AssemblyCSharp;
using UnityEngine;
using System.Xml;


namespace AssemblyCSharp
{
	public class EffetProduire : Effet
	{

		public float quantite;
		public Ressources ressource;

		public EffetProduire (XmlNode node)
		{
			quantite = float.Parse(node.SelectSingleNode ("Quantite").InnerText);
			ressource = (Ressources) Enum.Parse (typeof(Ressources), node.SelectSingleNode ("Ressource").InnerText);
		}

		override public void appliquerEffet(Batiment batiment) {
			//base.jouer (batiment);
			Colonie.instance.produire(ressource,quantite);
		}

		override public void appliquerEffetGraphique(Batiment batiment) {
			Case kase = batiment.kase;
			GameObject go = MonoBehaviour.Instantiate (SC_GestionPlanete.instance.particulesProductionRessources);
			go.transform.position = kase.tuile.transform.position;
			SC_particulesProductionRessources script = go.GetComponent<SC_particulesProductionRessources> ();
			script.tempsDeVie = 3;
			script.particules.maxParticles = (int) Math.Round(quantite);

		}





	}
}

