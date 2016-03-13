using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class Effet
	{

		public Effet ()
		{

		}

		public void jouer(Batiment batiment) {
			appliquerEffet (batiment);
			appliquerEffetGraphique (batiment);
		}

		virtual public void appliquerEffet(Batiment batiment) {}
		virtual public void appliquerEffetGraphique(Batiment batiment) {}


	}
}

