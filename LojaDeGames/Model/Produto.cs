using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using LojaDeGames.Util;

namespace LojaDeGames.Model
{
    public class Produto
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Column(TypeName = "varchar")]
        [StringLength(1000)]
        public string Descricao { get; set; } = string.Empty;

        [Column(TypeName = "date")]
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateTime DataLancamento { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(250)]
        public string Console { get; set; } = string.Empty;

        [Column(TypeName = "decimal")]
        [Precision(7,2)]
        public decimal Preco { get; set; }

        [Column(TypeName = "varchar")]
        [StringLength(5000)]
        public string Foto { get; set; } = string.Empty;
        
        public virtual Categoria? Categoria { get; set; }
    }
}
