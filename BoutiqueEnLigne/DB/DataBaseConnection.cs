using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using SQLite;
using BoutiqueEnLigne.Models;
using System;

namespace BoutiqueEnLigne.DB
{
    public class DataBaseConnection
    {
        public readonly SQLiteAsyncConnection _baseDeDonnees;
        public DataBaseConnection(string cheminBaseDeDonnees)
        {
            _baseDeDonnees = new SQLiteAsyncConnection(cheminBaseDeDonnees,true);
            _baseDeDonnees.CreateTableAsync<Categorie>().Wait();
            _baseDeDonnees.CreateTableAsync<Produit>().Wait();
            _baseDeDonnees.CreateTableAsync<LigneCommande>().Wait();
            _baseDeDonnees.CreateTableAsync<Commande>().Wait();
        }

        public void AddCategorie(Categorie c)
        {
            _baseDeDonnees.InsertAsync(c);
        }
        public Task<List<Categorie>> ObtenirCategories()
        {
            return _baseDeDonnees.Table<Categorie>().ToListAsync();
        }

        public void ModifierCategorie(Categorie categorie)
        {
            _baseDeDonnees.UpdateAsync(categorie);
        }

        public void SupprimerCategorie(int idCategorie)
        {
            _baseDeDonnees.DeleteAsync<Categorie>(idCategorie);
        }


        /////////////////////////////////////

        public Task<List<Produit>> ObtenirProduits()
        {
            return _baseDeDonnees.Table<Produit>().ToListAsync();
        }


        public Task<List<Produit>> ObtenirProduit(int idCategorie)
        {
            return _baseDeDonnees.Table<Produit>().Where(p => p.IdCategorie == idCategorie).ToListAsync();
        }

        public void AjouterProduit(Produit produit)
        {
            _baseDeDonnees.InsertAsync(produit);
        }

        public Task<Commande> GetCommandeByNomClientAsync(string nomClient)
        {
            return _baseDeDonnees.Table<Commande>().FirstOrDefaultAsync(c => c.NomClient == nomClient);
        }


        public void ModifierProduit(Produit produit)
        {
            _baseDeDonnees.UpdateAsync(produit);
        }

        public void SupprimerProduit(int idProduit)
        {
            _baseDeDonnees.DeleteAsync<Produit>(idProduit);
        }

        ///////////////////////////////

        public void AjouterLigneCommande(LigneCommande ligneCommande)
        {
            _baseDeDonnees.InsertAsync(ligneCommande);
        }

        public Task<List<LigneCommande>> ObtenirLignesCommande(int idCommande)
        {
            return _baseDeDonnees.Table<LigneCommande>().Where(l => l.IdCommande== idCommande).ToListAsync();
        }

        ////////////////////////////

        public async Task<List<LigneCommande>> ObtenirLignesCommande1Async()
        {
            return await _baseDeDonnees.Table<LigneCommande>().ToListAsync();
        }

        public void AjouterCommandeAsync(Commande commande)
        {
             _baseDeDonnees.InsertAsync(commande);
        }

        public async Task<Produit> GetProduitByIdAsync(int produitId)
        {

            return await _baseDeDonnees.FindAsync<Produit>(produitId);
        }
        public void UpdateLigneCommande(LigneCommande ligneCommande)
        {
            _baseDeDonnees.UpdateAsync(ligneCommande);
        }


    }
}
