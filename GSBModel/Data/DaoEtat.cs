using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using GSBModel.Business;

namespace GSBModel.Data
{
    public class DaoEtat
    {
        private Dbal _dbal;

        public DaoEtat (Dbal theDbal)
        {
            _dbal = theDbal;
        }

        public List<Etat> SelectAll()
        {
            List<Etat> listEtat = new List<Etat>();
            DataTable myTable = this._dbal.SelectAll("etat");

            foreach (DataRow r in myTable.Rows)
            {
                listEtat.Add(new Etat((string)r["id"], (string)r["libelle"]));
            }

            return listEtat;
        }

        public Etat SelectById(string idEtat)
        {
            DataRow result = this._dbal.SelectById("Etat", idEtat);
            return new Etat((string)result["id"], (string)result["libelle"]);

        }
    }
}
