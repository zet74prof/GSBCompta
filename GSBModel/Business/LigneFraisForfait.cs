using System;
using System.Collections.Generic;
using System.Text;

namespace GSBModel.Business
{
    public class LigneFraisForfait
    {
        public FicheFrais UneFicheFrais { get; set; }
        public FraisForfait UnFraisForfait { get; set; }
        public int Quantite { get; set; }

        public LigneFraisForfait(FicheFrais laFicheFrais, FraisForfait leFraisForfait, int laQuantite)
        {
            UneFicheFrais = laFicheFrais;
            UnFraisForfait = leFraisForfait;
            Quantite = laQuantite;
        }

        public override string ToString()
        {
            return UneFicheFrais.UnVisiteur.Id + "-" + UneFicheFrais.Mois + "-" + UnFraisForfait.Libelle + "-" + Quantite.ToString();
        }
    }
}
