using System;
using UnityEngine;
using AssemblyCSharp;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class EvenementGenerator
	{
		int entropie;							//pourcentage de chance de popage d'un event
		public EvenementGenerator instance;

		public EvenementGenerator ()
		{
			instance = this;
		}

		public void initialisation() 			// remettre la variable de chance d'apparition d'event a sa valeur initiale
		{
			entropie = 60; 						//modifiable
		}

		public void testEvent()
		{
			System.Random rnd = new System.Random();
			int test = rnd.Next(0, 100);
			if(entropie >= test)				//si oui alors faire tout les test de poids d'event sur la case et en tirer un au hasard
			{
			
			}
		}
	}
}

