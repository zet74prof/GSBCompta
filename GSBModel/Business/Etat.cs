using System;
using System.Collections.Generic;
using System.Text;

namespace GSBModel.Business
{
    public class Etat
    {
        public string Id { get; set; }
        public string Libelle { get; set; }

        public Etat(string id, string libelle)
        {
            Id = id;
            Libelle = libelle;
        }
    }
}

