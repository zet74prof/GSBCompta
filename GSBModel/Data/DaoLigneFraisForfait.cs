using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using GSBModel.Business;

namespace GSBModel.Data
{
    public class DaoLigneFraisForfait
    {
        private readonly Dbal _dbal;
        private readonly DaoFraisForfait _daoFraisForfait;

        public DaoLigneFraisForfait(Dbal myDbal, DaoFraisForfait myDaoFraisForfait)
        {
            _dbal = myDbal;
            _daoFraisForfait = myDaoFraisForfait;
        }

        public void Insert(LigneFraisForfait uneLigneFraisForfait)
        {
            string query = "lignefraisforfait VALUES ('" + uneLigneFraisForfait.UneFicheFrais.UnVisiteur.Id
                + "', '" + uneLigneFraisForfait.UneFicheFrais.Mois
                + "', '" + uneLigneFraisForfait.UnFraisForfait.Id
                + "', " + uneLigneFraisForfait.Quantite + ")";
            this._dbal.Insert(query);
        }

        public void Update(LigneFraisForfait uneLigneFraisForfait)
        {
            string query = "lignefraisforfait SET idVisiteur='" + uneLigneFraisForfait.UneFicheFrais.UnVisiteur.Id
                + "', mois='" + uneLigneFraisForfait.UneFicheFrais.Mois
                + "', idFraisForfait='" + uneLigneFraisForfait.UnFraisForfait.Id 
                + "', quantite=" + uneLigneFraisForfait.Quantite
                + " WHERE idVisiteur='" + uneLigneFraisForfait.UneFicheFrais.UnVisiteur.Id
                + "' AND mois='" + uneLigneFraisForfait.UneFicheFrais.Mois
                + "' AND idFraisForfait='" + uneLigneFraisForfait.UnFraisForfait.Id + "'";
            this._dbal.Update(query);
        }

        public List<LigneFraisForfait> SelectByFicheFrais(FicheFrais uneFicheFrais)
        {
            List<LigneFraisForfait> listLignesFraisForfait = new List<LigneFraisForfait>();
            DataTable myTable = this._dbal.SelectByComposedFK2("lignefraisforfait", "idVisiteur", uneFicheFrais.UnVisiteur.Id, "mois", uneFicheFrais.Mois);

            foreach (DataRow r in myTable.Rows)
            {
                FraisForfait leFraisForfait = this._daoFraisForfait.SelectById((string)r["idFraisForfait"]);
                listLignesFraisForfait.Add(new LigneFraisForfait(uneFicheFrais, leFraisForfait, (int)r["quantite"]));
            }
            return listLignesFraisForfait;
        }
    }
}
