using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using GSBModel.Business;

namespace GSBModel.Data
{
    public class DaoVisiteur
    {
        private Dbal _dbal;

        public DaoVisiteur(Dbal mydbal)
        {
            this._dbal = mydbal;
        }

        public void Insert(Visiteur theVisiteur)
        {
            string query = "Visiteur VALUES (" + theVisiteur.Id + ",'" + theVisiteur.Nom.Replace("'", "''") + "')";
            this._dbal.Insert(query);
        }

        //public void InsertFromCSV(string filename)
        //{
        //    using (var reader = new StreamReader(filename))
        //    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        //    {
        //        csv.Configuration.Delimiter = ";";
        //        csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();

        //        var record = new Pays();
        //        var records = csv.EnumerateRecords(record);

        //        foreach (Pays r in records)
        //        {
        //            Console.WriteLine(r.Id + "-" + r.Name);
        //            this.Insert(record);
        //        }
        //    }
        //}

        public List<Visiteur> SelectAll()
        {
            List<Visiteur> listVisiteurs = new List<Visiteur>();
            DataTable myTable = this._dbal.SelectAll("visiteur");

            foreach (DataRow r in myTable.Rows)
            {
                listVisiteurs.Add(new Visiteur((string)r["id"], (string)r["nom"], (string)r["prenom"],
                    (string)r["login"], (string)r["mdp"],
                    (string)r["adresse"], (string)r["cp"],
                    (string)r["ville"], (DateTime)r["dateEmbauche"]));
            }

            return listVisiteurs;
        }

        public Visiteur SelectById(string idVisiteur)
        {
            DataRow r = this._dbal.SelectById("Visiteur", idVisiteur);
            return new Visiteur((string)r["id"], (string)r["nom"], (string)r["prenom"],
                    (string)r["login"], (string)r["mdp"],
                    (string)r["adresse"], (string)r["cp"],
                    (string)r["ville"], (DateTime)r["dateEmbauche"]);
        }
    }
}
