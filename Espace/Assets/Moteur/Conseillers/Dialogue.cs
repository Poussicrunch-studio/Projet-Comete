using System;

namespace AssemblyCSharp
{
	public class Dialogue
	{

		public double poids;
		public String texte;
		public Faction faction;
		public TypeDeDialogue type;

		public Dialogue ()
		{
		}

		public double getPoids() {
			return poids;
		}
	}
}

