using System;

namespace AssemblyCSharp
{
	public class EffetConsommer : Effet
	{

		public float quantite;
		public Ressources ressource;

		public EffetConsommer ()
		{
		}

		override public void jouer(Batiment batiment) {
			//base.jouer (batiment);
			Colonie.instance.consommer(ressource,quantite);
		}

	}
}

