using System;
using System.Collections.Generic;
using System.Text;
using GSBModel.Business;
using System.Data;

namespace GSBModel.Data
{
    public class DaoFraisForfait
    {
        private Dbal _dbal;

        public DaoFraisForfait(Dbal theDbal)
        {
            _dbal = theDbal;
        }

        public List<FraisForfait> SelectAll()
        {
            List<FraisForfait> listFraisForfait = new List<FraisForfait>();
            DataTable myTable = this._dbal.SelectAll("fraisforfait");

            foreach (DataRow r in myTable.Rows)
            {
                listFraisForfait.Add(new FraisForfait((string)r["id"], (string)r["libelle"], (decimal)r["montant"]));
            }

            return listFraisForfait;
        }

        public FraisForfait SelectById(string idFraisForfait)
        {
            DataRow result = this._dbal.SelectById("fraisforfait", idFraisForfait);
            return new FraisForfait((string)result["id"], (string)result["libelle"], (decimal)result["montant"]);
        }
    }
}
