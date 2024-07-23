using Project.Models;

namespace Project.Services.Manage
{
    public interface IVisualizzaCreazioniService
    {
        List<Camera> GetAllCamere();
        List<Persona> GetAllPersone();
    }
}
