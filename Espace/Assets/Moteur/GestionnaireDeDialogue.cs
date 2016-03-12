using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class GestionnaireDeDialogue
	{
		public List<Dialogue> listeDialogue;
		public SC_GestionPlanete gestionnaire;

		public GestionnaireDeDialogue (SC_GestionPlanete gestionnaire)
		{
			this.gestionnaire = gestionnaire;
			listeDialogue = DataManager.dataManager.listeDialogue;
		}

		public void mettreAJour() {

		}

	}
}

