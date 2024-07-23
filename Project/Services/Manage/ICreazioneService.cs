using Project.Models;
namespace Project.Services.Manage
{
    public interface ICreazioneService
    {
        Persona CreazionePersona(Persona persona); 
        Camera CreazioneCamera(Camera camera);
        Prenotazione CreazionePrenotazione(Prenotazione prenotazione);
    }
}
