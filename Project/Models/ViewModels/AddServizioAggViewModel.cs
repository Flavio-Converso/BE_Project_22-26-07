namespace Project.Models.ViewModels
{
    public class AddServizioAggViewModel
    {
        public int IdPrenotazione { get; set; }
        public int IdServizioAgg { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public int Quantita { get; set; }
        public decimal Prezzo { get; set; }
    }
}
