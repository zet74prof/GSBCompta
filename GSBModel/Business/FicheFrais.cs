using System;
using System.Collections.Generic;
using System.Text;

namespace GSBModel.Business
{
    public class FicheFrais
    {

        public Visiteur UnVisiteur { get; set; }
        public string Mois { get; set; }
        public int NbJustificatifs { get; set; }
        public decimal MontantValide { get; set; }
        public DateTime DateModif { get; set; }
        public Etat UnEtat { get; set; }

        public List<LigneFraisForfait> LesLignesFraisForfait { get; set; }
        public List<LigneFraisHorsForfait> LesLignesFraisHorsForfait { get; set; }

        public FicheFrais(Visiteur leVisteur, string leMois, Etat lEtat, int leNbJustificatifs = 0, DateTime laDateModif = new DateTime(), decimal leMontantValide = 0M)
        {
            UnVisiteur = leVisteur;
            Mois = leMois;
            NbJustificatifs = leNbJustificatifs;
            MontantValide = leMontantValide;
            DateModif = laDateModif;
            UnEtat = lEtat;
        }

        public override string ToString()
        {
            return "Fiche frais " + Mois + "-" + UnVisiteur.Id + "-" + UnVisiteur.Prenom + " " + UnVisiteur.Nom;
        }
    }
}
