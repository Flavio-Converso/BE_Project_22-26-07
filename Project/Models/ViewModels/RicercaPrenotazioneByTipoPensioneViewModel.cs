using System.ComponentModel.DataAnnotations;

namespace Project.Models.ViewModels
{
    public class RicercaPrenotazioneByTipoPensioneViewModel
    {
        [Required(ErrorMessage = "Il tipo di pensione è richiesto.")]
        [StringLength(50, ErrorMessage = "Il tipo di pensione non può superare i 50 caratteri.")]
        [RegularExpression(@"^(Prima Colazione|Pensione Completa|Mezza Pensione)$", ErrorMessage = "Tipo di pensione non valido.")]
        public string TipoPensione { get; set; }
    }
}
