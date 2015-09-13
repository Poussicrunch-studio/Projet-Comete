using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class TypeDeBatiment
	{
		public CategoriesConseiller categorie;
		public int niveau;
		public float poids;
		public List<Effet> effets = new List<Effet> ();


		public TypeDeBatiment ()
		{
		}
	}
}

