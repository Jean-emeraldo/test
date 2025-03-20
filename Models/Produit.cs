using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudDotNet.Models{
    public class Produit{
        [Key]
        public int id_produit {get; set;}
        [Required]
        [MaxLength(50)]
        public string nom_produit {get; set;}
        [ForeignKey("id_cat")]
        public Categorie? Categorie {get; set;}
        public int id_cat {get; set;}
    }
}