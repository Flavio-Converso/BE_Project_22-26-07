using Project.Models;

namespace Project.Services.Management
{
    public interface IAddServiziAgg
    {
        public PrenotazioneServizioAgg AddServizioAgg(PrenotazioneServizioAgg prenotazioneServizioAgg, int IdPrenotazione);
        public List<ServizioAgg> GetServiziAgg();
    }
}
