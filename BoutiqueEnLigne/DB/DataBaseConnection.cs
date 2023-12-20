using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using BoutiqueEnLigne.Models;

namespace BoutiqueEnLigne.DB
{
    class DataBaseConnection
    {
        private readonly SQLiteConnection _baseDeDonnees;
        public DataBaseConnection(string cheminBaseDeDonnees)
        {
            _baseDeDonnees = new SQLiteConnection(cheminBaseDeDonnees);
            _baseDeDonnees.CreateTable<Categorie>();
            _baseDeDonnees.CreateTable<Produit>();
            _baseDeDonnees.CreateTable<LigneCommande>();
            _baseDeDonnees.CreateTable<Commande>();
        }

        public void AddCategorie(Categorie c)
        {
            _baseDeDonnees.Insert(c);
        }
        public List<Categorie> ObtenirCategories()
        {
            return _baseDeDonnees.Table<Categorie>().ToList();
        }

        public void ModifierCategorie(Categorie categorie)
        {
            _baseDeDonnees.Update(categorie);
        }

        public void SupprimerCategorie(int idCategorie)
        {
            _baseDeDonnees.Delete<Categorie>(idCategorie);
        }


        /////////////////////////////////////

        public List<Produit> ObtenirProduits()
        {
            return _baseDeDonnees.Table<Produit>().ToList();
        }


        public List<Produit> ObtenirProduit(int idCategorie)
        {
            return _baseDeDonnees.Table<Produit>().Where(p => p.IdCategorie == idCategorie).ToList();
        }

        public void AjouterProduit(Produit produit)
        {
            _baseDeDonnees.Insert(produit);
        }

        public void ModifierProduit(Produit produit)
        {
            _baseDeDonnees.Update(produit);
        }

        public void SupprimerProduit(int idProduit)
        {
            _baseDeDonnees.Delete<Produit>(idProduit);
        }

        ///////////////////////////////

        public void AjouterLigneCommande(LigneCommande ligneCommande)
        {
            _baseDeDonnees.Insert(ligneCommande);
        }

        public List<LigneCommande> ObtenirLignesCommande(int idCommande)
        {
            return _baseDeDonnees.Table<LigneCommande>().Where(l => l.IdCommande== idCommande).ToList();
        }

        ////////////////////////////


        public void AjouterCommande(Commande commande)
        {
            _baseDeDonnees.Insert(commande);
        }

    }
}
