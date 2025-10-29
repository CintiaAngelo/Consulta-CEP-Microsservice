using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CepServiceApp.Models
{
    [Table("Ceps")]
    public class Cep
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("Cep")] // Mapeia para a coluna "Cep" do banco
        [StringLength(8)]
        public string CepCode { get; set; } = string.Empty; // ✅ Mude para CepCode

        [Column("Logradouro")]
        [StringLength(255)]
        public string Logradouro { get; set; } = string.Empty;

        [Column("Complemento")]
        [StringLength(255)]
        public string Complemento { get; set; } = string.Empty;

        [Column("Bairro")]
        [StringLength(100)]
        public string Bairro { get; set; } = string.Empty;

        [Column("Localidade")]
        [StringLength(100)]
        public string Localidade { get; set; } = string.Empty;

        [Column("Uf")]
        [StringLength(2)]
        public string Uf { get; set; } = string.Empty;

        [Column("Ibge")]
        [StringLength(10)]
        public string Ibge { get; set; } = string.Empty;

        [Column("Gia")]
        [StringLength(10)]
        public string Gia { get; set; } = string.Empty;

        [Column("Ddd")]
        [StringLength(3)]
        public string Ddd { get; set; } = string.Empty;

        [Column("Siafi")]
        [StringLength(10)]
        public string Siafi { get; set; } = string.Empty;

        [Required]
        [Column("DataConsulta")]
        public DateTime DataConsulta { get; set; } = DateTime.Now;
    }
}