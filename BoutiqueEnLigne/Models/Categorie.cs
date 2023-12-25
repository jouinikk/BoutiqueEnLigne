// Categorie.cs
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BoutiqueEnLigne.Models
{
    public class Categorie
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Nom { get; set; }
        public string img { get; set; }

        public override string ToString()
        {
            return Nom; // Display the category name
        }
    }
}
