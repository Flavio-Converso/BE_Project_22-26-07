
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Prenotazione
    {
        [Key]
        public int IdPrenotazione { get; set; }

        [Required]
        public DateTime DataPrenotazione { get; set; }


        public int NumProgressivo { get; set; }


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

        [Required(ErrorMessage = "Il tipo di pensione è richiesto.")]
        [StringLength(50, ErrorMessage = "Il tipo di pensione non può superare i 50 caratteri.")]
        [RegularExpression(@"^(Prima Colazione|Pensione Completa|Mezza Pensione)$", ErrorMessage = "Tipo di pensione non valido.")]
        public string TipoPensione { get; set; }

        [Required]
        public int IdPersona { get; set; }

        [Required]
        public int IdCamera { get; set; }
    }
}
