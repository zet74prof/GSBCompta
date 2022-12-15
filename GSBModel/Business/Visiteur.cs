using System;
using System.Collections.Generic;
using System.Text;

namespace GSBModel.Business
{
    public class Visiteur
    {
        public string Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Login { get; set; }
        public string Mdp { get; set; }
        public string Adresse { get; set; }
        public string Cp { get; set; }
        public string Ville { get; set; }
        public DateTime DateEmbauche { get; set; }

        public List<FicheFrais> Fichefrais { get; set; }

        public Visiteur(string id, string nom, string prenom, string login, string mdp, string adresse, string cp, string ville, DateTime dateEmbauche)
        {
            Id = id;
            Nom = nom;
            Prenom = prenom;
            Login = login;
            Mdp = mdp;
            Adresse = adresse;
            Cp = cp;
            Ville = ville;
            DateEmbauche = dateEmbauche;
        }

        public override string ToString()
        {
            return Id+"-"+Nom+"-"+Prenom;
        }
    }
}
