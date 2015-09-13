using System;
using UnityEngine;
using AssemblyCSharp;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class EvenementGenerator
	{
		int entropie; 							//pourcentage de chance de popage d'un event
		int poidTotal;
		int poidTemp;
		public EvenementGenerator instance;

		public EvenementGenerator ()
		{
			instance = this;
		}

		public void initialisation() 			// remettre la variable de chance d'apparition d'event a sa valeur initiale
		{
			entropie = 60; 						//modifiable
		}

		public void testEvent(Case laCase)
		{
			poidTotal=0;
			poidTemp=0;
			System.Random rnd = new System.Random();
			int test = rnd.Next(0, 100);
			if(entropie >= test)				//si oui alors faire tout les test de poids d'event sur la case et en tirer un au hasard
			{
			foreach(Evenement e in DataManager.dataManager.listEvenement)
			{
				foreach(ConditionTerrain t in e.conditionT)
					{
					if(t.requis == true)
					{
						if(t.type == laCase.terrain)
						{
							poidTemp += t.poid;
						}
						else
						{
							poidTemp=0; 		//le terrain est requis et ce n'est pas le meme
							break;
						}
					}
					else
					{
						if(t.type == laCase.terrain)
						{
							poidTemp += t.poid;
						}
					}
			}
				}
				if(poidTemp<0)
					poidTemp = 0; 				// pas de poid négatif
			}
		}
	}
}

