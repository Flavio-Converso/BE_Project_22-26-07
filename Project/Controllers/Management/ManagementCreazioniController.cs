using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Services.Management;

namespace Project.Controllers.Management
{
    [Authorize]
    public class ManagementCreazioniController : Controller
    {
        private readonly ICreazioneService _creazioneService;
        private readonly ILogger<ManagementCreazioniController> _logger;

        public ManagementCreazioniController(ICreazioneService creazioneService, ILogger<ManagementCreazioniController> logger)
        {
            _creazioneService = creazioneService;
            _logger = logger;
        }


        public IActionResult CreazionePersona()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreazionePersona(Persona persona)
        {
            if (!ModelState.IsValid)
            {
                return View(persona);
            }

            try
            {
                var createdPersona = _creazioneService.CreazionePersona(persona);
                return RedirectToAction("Management", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione della persona.");
                ModelState.AddModelError(string.Empty, "Si è verificato un errore. Riprova più tardi.");
                return View(persona);
            }
        }


        public IActionResult CreazioneCamera()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreazioneCamera(Camera camera)
        {
            if (!ModelState.IsValid)
            {
                return View(camera);
            }

            try
            {
                var createdCamera = _creazioneService.CreazioneCamera(camera);
                return RedirectToAction("Management", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione della camera.");
                ModelState.AddModelError(string.Empty, "Si è verificato un errore. Riprova più tardi.");
                return View(camera);
            }
        }

        public IActionResult CreazionePrenotazione()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreazionePrenotazione(Prenotazione prenotazione)
        {
            if (!ModelState.IsValid)
            {
                return View(prenotazione);
            }

            try
            {
                var createdPrenotazione = _creazioneService.CreazionePrenotazione(prenotazione);
                return RedirectToAction("Management", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante la creazione della prenotazione.");
                ModelState.AddModelError(string.Empty, "Si è verificato un errore. Riprova più tardi.");
                return View(prenotazione);
            }
        }


    }
}
