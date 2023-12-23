using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions.Attributes;
namespace BoutiqueEnLigne.Models
{
    public class Commande
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string NomClient { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<LigneCommande> LignesCommande { get; set; }
    }
}
