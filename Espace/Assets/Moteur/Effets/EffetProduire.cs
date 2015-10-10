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

		override public void jouer(Batiment batiment) {
			//base.jouer (batiment);
			Colonie.instance.produire(ressource,quantite);
		}
	}
}

