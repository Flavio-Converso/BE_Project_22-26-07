using Project.Models;

namespace Project.Services.Management
{
    public interface IVisualizzaCreazioniService
    {
        List<Camera> GetAllCamere();
        List<Persona> GetAllPersone();
        List<Prenotazione> GetAllPrenotazioni();
    }
}
