using System;
using System.Xml;

namespace AssemblyCSharp
{
	public class EffetConsommer : Effet
	{

		public float quantite;
		public Ressources ressource;

		public EffetConsommer (XmlNode node)
		{
		}

		override public void appliquerEffet(Batiment batiment) {
			//base.jouer (batiment);
			Colonie.instance.consommer(ressource,quantite);
		}

	}
}

