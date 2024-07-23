using Project.Models;

namespace Project.Services.Management
{
    public interface IVisualizzaCreazioniService
    {
        List<Camera> GetAllCamere(); //FATTO
        List<Persona> GetAllPersone(); //FATTO
        List<Prenotazione> GetAllPrenotazioni(); //DA FARE
    }
}
