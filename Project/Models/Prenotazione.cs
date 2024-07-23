
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Prenotazione
    {
        [Key]
        public int IdPrenotazione { get; set; }

        [Required]
        public DateTime DataPrenotazione { get; set; }

        [Required]
        public int NumProgressivo { get; set; }

        [Required]
        public int Anno { get; set; }

        [Required]
        public DateTime SoggiornoDal { get; set; }

        [Required]
        public DateTime SoggiornoAl { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Caparra deve essere un valore positivo.")]
        public decimal Caparra { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Tariffa deve essere un valore positivo.")]
        public decimal Tariffa { get; set; }

        [StringLength(50)]
        [RegularExpression(@"^(Prima Colazione|Pensione Completa|Mezza Pensione)$", ErrorMessage = "Tipo Pensione non valido.")]
        public string TipoPensione { get; set; }

        [Required]
        public int IdPersona { get; set; }

        [Required]
        public int IdCamera { get; set; }
    }
}
