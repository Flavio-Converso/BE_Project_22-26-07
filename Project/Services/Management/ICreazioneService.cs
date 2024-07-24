using Project.Models;
namespace Project.Services.Management
{
    public interface ICreazioneService
    {
        Persona CreazionePersona(Persona persona);
        Camera CreazioneCamera(Camera camera);
        Prenotazione CreazionePrenotazione(Prenotazione prenotazione);
    }
}
