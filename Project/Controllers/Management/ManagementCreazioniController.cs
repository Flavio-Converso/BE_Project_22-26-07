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


        // GET: /Manage/Create
        public IActionResult CreazionePersona()
        {
            return View();
        }

        // POST: /Manage/Create
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


        // GET: /Manage/CreazioneCamera
        public IActionResult CreazioneCamera()
        {
            return View();
        }

        // POST: /Manage/CreazioneCamera
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

        // GET: /Manage/CreazionePrenotazione
        public IActionResult CreazionePrenotazione()
        {
            return View();
        }

        // POST: /Manage/CreazionePrenotazione
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
