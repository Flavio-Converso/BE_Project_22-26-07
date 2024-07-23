using Project.Models;

namespace Project.Services.Manage
{
    public interface IVisualizzaCreazioniService
    {
        List<Camera> GetAllCamere(); //FATTO
        List<Persona> GetAllPersone(); //FATTO
        List<Prenotazione> GetAllPrenotazioni(); //DA FARE
    }
}
