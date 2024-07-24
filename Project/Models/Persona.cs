using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Persona
    {
        [Key]
        public int IdPersona { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il nome può contenere al massimo 50 caratteri.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il cognome è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il cognome può contenere al massimo 50 caratteri.")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "Il Codice Fiscale è obbligatorio.")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Il Codice Fiscale deve contenere esattamente 16 caratteri.")]
        [RegularExpression(@"^[^\s]{16}$", ErrorMessage = "Il Codice Fiscale deve essere di 16 caratteri e non deve contenere spazi.")]
        public string CF { get; set; }

        [EmailAddress(ErrorMessage = "Inserisci un indirizzo email valido.")]
        [StringLength(100, ErrorMessage = "L'email può contenere al massimo 100 caratteri.")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "L'email non deve contenere spazi.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Inserisci un numero di telefono valido.")]
        [StringLength(20, ErrorMessage = "Il telefono può contenere al massimo 20 caratteri.")]
        public string Telefono { get; set; }

        [Phone(ErrorMessage = "Inserisci un numero di cellulare valido.")]
        [StringLength(20, ErrorMessage = "Il cellulare può contenere al massimo 20 caratteri.")]
        public string Cellulare { get; set; }

        [StringLength(50, ErrorMessage = "La città può contenere al massimo 50 caratteri.")]
        public string Città { get; set; }

        [StringLength(2, ErrorMessage = "La provincia può contenere al massimo 2 caratteri.")]
        public string Provincia { get; set; }

        public List<Prenotazione> Prenotazioni { get; set; } = new List<Prenotazione>();
    }
}
