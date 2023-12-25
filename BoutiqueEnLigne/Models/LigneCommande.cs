using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;
namespace BoutiqueEnLigne.Models
{
    public class LigneCommande
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey(typeof(Produit))]
        public int IdProduit { get; set; }

        [ForeignKey(typeof(Commande))]
        public int IdCommande { get; set; }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Produit Produit { get; set; }

        public int Quantite { get; set; }


    }
}
