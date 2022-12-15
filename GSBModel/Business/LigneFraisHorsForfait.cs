using System;
using System.Collections.Generic;
using System.Text;

namespace GSBModel.Business
{
    public class LigneFraisHorsForfait
    {
        public int Id { get; set; }
        public FicheFrais UneFicheFrais { get; set; }
        public string Libelle { get; set; }
        public DateTime Date { get; set; }
        public decimal Montant { get; set; }

        public LigneFraisHorsForfait(int id, FicheFrais laFicheFrais, string libelle, DateTime date, decimal montant)
        {
            Id = id;
            UneFicheFrais = laFicheFrais;
            Libelle = libelle;
            Date = date;
            Montant = montant;
        }

        public override string ToString()
        {
            return Id.ToString() + "-" + UneFicheFrais.UnVisiteur.Id + "-" + UneFicheFrais.Mois + "-" + Libelle + "-" + Montant.ToString(); 
        }
    }
}
