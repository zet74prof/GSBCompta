using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using GSBModel.Business;

namespace GSBModel.Data
{
    public class DaoLigneFraisHorsForfait
    {
        private readonly Dbal _dbal;

        public DaoLigneFraisHorsForfait(Dbal myDbal)
        {
            _dbal = myDbal;
        }

        public void Insert(LigneFraisHorsForfait uneLigneFraisHorsForfait)
        {
            string query = "lignefraishorsforfait (idVisiteur, mois, libelle, date, montant)" +
                " VALUES ('" + uneLigneFraisHorsForfait.UneFicheFrais.UnVisiteur.Id
                + "', '" + uneLigneFraisHorsForfait.UneFicheFrais.Mois
                + "', '" + uneLigneFraisHorsForfait.Libelle
                + "', '" + uneLigneFraisHorsForfait.Date.ToString("yyyy-MM-dd")
                + "', " + uneLigneFraisHorsForfait.Montant + ")";
            this._dbal.Insert(query);
        }

        public void Update(LigneFraisHorsForfait uneLigneFraisHorsForfait)
        {
            string query = "lignefraishorsforfait SET date='" + uneLigneFraisHorsForfait.Date.ToString("yyyy-MM-dd")
                + "', montant=" + uneLigneFraisHorsForfait.Montant
                + ", libelle='" + uneLigneFraisHorsForfait.Libelle
                + "', mois='" + uneLigneFraisHorsForfait.UneFicheFrais.Mois
                + "', idVisiteur='" + uneLigneFraisHorsForfait.UneFicheFrais.UnVisiteur.Id
                + "' WHERE id=" + uneLigneFraisHorsForfait.Id;
            this._dbal.Update(query);
        }

        public void Delete(LigneFraisHorsForfait uneLigneFraisHorsForfait)
        {
            string query = "lignefraishorsforfait"
                + " WHERE id=" + uneLigneFraisHorsForfait.Id;
            this._dbal.Delete(query);
        }

        public List<LigneFraisHorsForfait> SelectByFicheFrais(FicheFrais uneFicheFrais)
        {
            List<LigneFraisHorsForfait> listLignesFraisHorsForfait = new List<LigneFraisHorsForfait>();
            DataTable myTable = this._dbal.SelectByComposedFK2("lignefraishorsforfait", "idVisiteur", uneFicheFrais.UnVisiteur.Id, "mois", uneFicheFrais.Mois);

            foreach (DataRow r in myTable.Rows)
            {
                listLignesFraisHorsForfait.Add(new LigneFraisHorsForfait((int)r["id"], uneFicheFrais, (string)r["libelle"], (DateTime)r["date"], (decimal)r["montant"]));
            }
            return listLignesFraisHorsForfait;
        }
    }
}
