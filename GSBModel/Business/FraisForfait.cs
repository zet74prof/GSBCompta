using System;
using System.Collections.Generic;
using System.Text;

namespace GSBModel.Business
{
    public class FraisForfait
    {
        public string Id { get; set; }
        public string Libelle { get; set; }
        public decimal Montant { get; set; }

        public FraisForfait(string id, string libelle, decimal montant)
        {
            Id = id;
            Libelle = libelle;
            Montant = montant;
        }
    }
}
