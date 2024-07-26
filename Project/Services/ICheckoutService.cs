using Project.Models.ViewModels;

namespace Project.Services
{
    public interface ICheckoutService
    {
        public Task<CheckoutViewModel> GetPrenotazioneConImportoDaSaldare(int idPrenotazione);
    }
}
