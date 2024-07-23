using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Camera
    {
        public int IdCamera { get; set; }

        [Required(ErrorMessage = "Il numero della camera è obbligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Il numero della camera deve essere un numero positivo.")]
        public int NumeroCamera { get; set; }

        [StringLength(255, ErrorMessage = "La descrizione può contenere al massimo 255 caratteri.")]
        public string Descrizione { get; set; }

        [StringLength(20, ErrorMessage = "La tipologia può contenere al massimo 20 caratteri.")]
        [RegularExpression("^(doppia|singola)$", ErrorMessage = "La tipologia deve essere 'doppia' o 'singola'.")]
        public string Tipologia { get; set; }
    }
}
