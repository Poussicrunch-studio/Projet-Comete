using System;
using AssemblyCSharp;
using UnityEngine;
namespace AssemblyCSharp
{
	public class EffetProduire : Effet
	{

		public float quantite;
		public Ressources ressource;

		public EffetProduire ()
		{
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

