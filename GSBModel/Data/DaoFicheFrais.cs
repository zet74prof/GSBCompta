using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using GSBModel.Business;

namespace GSBModel.Data
{
    public class DaoFicheFrais
    {
        private readonly Dbal _dbal;
        private readonly DaoVisiteur _daoVisiteur;
        private readonly DaoEtat _daoEtat;
        private readonly DaoFraisForfait _daoFraisForfait;
        private readonly DaoLigneFraisForfait _daoLigneFraisForfait;
        private readonly DaoLigneFraisHorsForfait _daoLigneFraisHorsForfait;

        public DaoFicheFrais(Dbal myDbal, DaoEtat myDaoEtat, DaoFraisForfait myDaoFraisForfait, DaoVisiteur myDaoVisiteur, DaoLigneFraisForfait myDaoLigneFraisForfait, DaoLigneFraisHorsForfait myDaoLigneFraisHorsForfait)
        {
            _dbal = myDbal;
            _daoEtat = myDaoEtat;
            _daoFraisForfait = myDaoFraisForfait;
            _daoVisiteur = myDaoVisiteur;
            _daoLigneFraisForfait = myDaoLigneFraisForfait;
            _daoLigneFraisHorsForfait = myDaoLigneFraisHorsForfait;
        }

        public void Insert(FicheFrais uneFicheFrais)
        {
            string query = "fichefrais (idVisiteur, mois, nbJustificatifs, montantValide, dateModif, idEtat) " +
                "VALUES ('" + uneFicheFrais.UnVisiteur.Id + "', '" + uneFicheFrais.Mois + "', '"
                + uneFicheFrais.NbJustificatifs + "', '" + uneFicheFrais.MontantValide + "', '"
                + uneFicheFrais.DateModif.ToString("yyyy-MM-dd") + "', '"
                + uneFicheFrais.UnEtat.Id + "')";
            this._dbal.Insert(query);

            //lors de la création d'une nouvelle fiche frais, on créé et initialise les frais forfait à 0
            List<FraisForfait> lesFraisForfaits = _daoFraisForfait.SelectAll();
            foreach (FraisForfait f in lesFraisForfaits)
            {
                LigneFraisForfait uneLigneff = new LigneFraisForfait(uneFicheFrais, f, 0);
                _daoLigneFraisForfait.Insert(uneLigneff);
            }
        }

        public void Update(FicheFrais uneFicheFrais)
        {
            string query = "fichefrais SET idVisiteur='" + uneFicheFrais.UnVisiteur.Id 
                + "', mois='" + uneFicheFrais.Mois
                + "', nbJustificatifs=" + uneFicheFrais.NbJustificatifs 
                + ", montantValide=" + uneFicheFrais.MontantValide 
                + ", dateModif='" + uneFicheFrais.DateModif.ToString("yyyy-MM-dd") 
                + "', idEtat='" + uneFicheFrais.UnEtat.Id 
                + "' WHERE idVisiteur='" + uneFicheFrais.UnVisiteur.Id 
                + "' AND mois='" + uneFicheFrais.Mois + "'";
            this._dbal.Update(query);
        }

        public List<FicheFrais> SelectAll()
        {
            List<FicheFrais> listFicheFrais = new List<FicheFrais>();
            DataTable myTable = this._dbal.SelectAll("fichefrais");

            foreach (DataRow r in myTable.Rows)
            {
                Etat myEtat = this._daoEtat.SelectById((string)r["idEtat"]);
                Visiteur myVisiteur = this._daoVisiteur.SelectById((string)r["idVisiteur"]);
                FicheFrais uneFicheFrais = new FicheFrais(myVisiteur, (string)r["mois"], myEtat, (int)r["nbJustificatifs"], (DateTime)r["dateModif"], (decimal)r["montantValide"]);
                List<LigneFraisForfait> lesLignesFraisForfaits = new List<LigneFraisForfait>(this._daoLigneFraisForfait.SelectByFicheFrais(uneFicheFrais));
                List<LigneFraisHorsForfait> lesLignesFraisHorsForfaits = new List<LigneFraisHorsForfait>(this._daoLigneFraisHorsForfait.SelectByFicheFrais(uneFicheFrais));
                uneFicheFrais.LesLignesFraisForfait = lesLignesFraisForfaits;
                uneFicheFrais.LesLignesFraisHorsForfait = lesLignesFraisHorsForfaits;
                listFicheFrais.Add(uneFicheFrais);

                listFicheFrais.Add(uneFicheFrais);
            }
            return listFicheFrais;
        }

        public List<FicheFrais> SelectByVisiteur(Visiteur unVisiteur)
        {
            List<FicheFrais> listFicheFrais = new List<FicheFrais>();
            DataTable myTable = this._dbal.SelectByField("fichefrais", "idVisiteur='" + unVisiteur.Id + "'");

            foreach (DataRow r in myTable.Rows)
            {
                Etat myEtat = this._daoEtat.SelectById((string)r["idEtat"]);
                FicheFrais uneFicheFrais = new FicheFrais(unVisiteur, (string)r["mois"], myEtat, (int)r["nbJustificatifs"], (DateTime)r["dateModif"], (decimal)r["montantValide"]);
                List<LigneFraisForfait> lesLignesFraisForfaits = new List<LigneFraisForfait>(this._daoLigneFraisForfait.SelectByFicheFrais(uneFicheFrais));
                List<LigneFraisHorsForfait> lesLignesFraisHorsForfaits = new List<LigneFraisHorsForfait>(this._daoLigneFraisHorsForfait.SelectByFicheFrais(uneFicheFrais));
                uneFicheFrais.LesLignesFraisForfait = lesLignesFraisForfaits;
                uneFicheFrais.LesLignesFraisHorsForfait = lesLignesFraisHorsForfaits;
                listFicheFrais.Add(uneFicheFrais);
            }
            return listFicheFrais;
        }

        public List<FicheFrais> SelectByMois(string unMois)
        {
            List<FicheFrais> listFicheFrais = new List<FicheFrais>();
            DataTable myTable = this._dbal.SelectByField("fichefrais", "mois='" + unMois + "'");

            foreach (DataRow r in myTable.Rows)
            {
                Etat myEtat = this._daoEtat.SelectById((string)r["idEtat"]);
                Visiteur myVisiteur = this._daoVisiteur.SelectById((string)r["idVisiteur"]);
                FicheFrais uneFicheFrais = new FicheFrais(myVisiteur, unMois, myEtat, (int)r["nbJustificatifs"], (DateTime)r["dateModif"], (decimal)r["montantValide"]);
                List<LigneFraisForfait> lesLignesFraisForfaits = new List<LigneFraisForfait>(this._daoLigneFraisForfait.SelectByFicheFrais(uneFicheFrais));
                List<LigneFraisHorsForfait> lesLignesFraisHorsForfaits = new List<LigneFraisHorsForfait>(this._daoLigneFraisHorsForfait.SelectByFicheFrais(uneFicheFrais));
                uneFicheFrais.LesLignesFraisForfait = lesLignesFraisForfaits;
                uneFicheFrais.LesLignesFraisHorsForfait = lesLignesFraisHorsForfaits;
                listFicheFrais.Add(uneFicheFrais);
            }
            return listFicheFrais;
        }

        public FicheFrais SelectByVisiteurMois(Visiteur unVisiteur, string unMois)
        {
            if (this._dbal.SelectByComposedPK2("fichefrais", "mois", unMois, "idVisiteur", unVisiteur.Id) != null)
            {
                DataRow rowFicheFrais = this._dbal.SelectByComposedPK2("fichefrais", "mois", unMois, "idVisiteur", unVisiteur.Id);
                Etat myEtat = this._daoEtat.SelectById((string)rowFicheFrais["idEtat"]);
                FicheFrais uneFicheFrais = new FicheFrais(unVisiteur, unMois, myEtat, (int)rowFicheFrais["nbJustificatifs"], (DateTime)rowFicheFrais["dateModif"], (decimal)rowFicheFrais["montantValide"]);
                List<LigneFraisForfait> lesLignesFraisForfaits = new List<LigneFraisForfait>(this._daoLigneFraisForfait.SelectByFicheFrais(uneFicheFrais));
                List<LigneFraisHorsForfait> lesLignesFraisHorsForfaits = new List<LigneFraisHorsForfait>(this._daoLigneFraisHorsForfait.SelectByFicheFrais(uneFicheFrais));
                uneFicheFrais.LesLignesFraisForfait = lesLignesFraisForfaits;
                uneFicheFrais.LesLignesFraisHorsForfait = lesLignesFraisHorsForfaits;

                return uneFicheFrais;
            }
            else
                return null;
            
        }

        //Recherche dans la bdd les mois des fiches frais existantes dans l'ordre chronologique décroissant
        //On fait un select distinct pour ne conserver que les valeurs uniques
        public List<string> SelectListMois()
        {
            List<string> listMois = new List<string>();
            DataTable myTable = this._dbal.SelectDistinct("mois", "fichefrais", "mois", "desc");
            foreach (DataRow r in myTable.Rows)
            {
                listMois.Add((string)r["mois"]);
            }
            return listMois;
        }
    }
}
