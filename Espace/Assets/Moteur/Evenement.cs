using System;
using System.Collections.Generic;
namespace AssemblyCSharp
{
	public class Evenement
	{
		public string nom;
		public string description;
		public bool important;
		public bool choix;
		public string choix1;
		public string choix2;
		public string choix3;
		public List<ConditionTerrain> conditionT = new List<ConditionTerrain>();
		public List<ConditionRessource> conditionR = new List<ConditionRessource>();
		public int entropie;
		public Evenement ()
		{

		}
	}
}

