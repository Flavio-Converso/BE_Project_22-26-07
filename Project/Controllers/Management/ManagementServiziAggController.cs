using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Models.ViewModels;
using Project.Services.Management;

namespace Project.Controllers.Management
{
    [Authorize]
    public class ManagementServiziAggController : Controller
    {
        private readonly IAddServiziAgg _addServiziAggService;

        public ManagementServiziAggController(IAddServiziAgg addServiziAggService)
        {
            _addServiziAggService = addServiziAggService;
        }

        // GET: ManagementServiziAgg/AddServiziAgg
        public IActionResult AddServizioAgg(int idPrenotazione)
        {
            var viewModel = new AddServizioAggViewModel
            {
                IdPrenotazione = idPrenotazione
            };
            return View(viewModel);
        }

        // POST: ManagementServiziAgg/AddServizioAgg
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddServizioAgg(AddServizioAggViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var servizioAgg = new PrenotazioneServizioAgg
                    {
                        IdPrenotazione = model.IdPrenotazione,
                        IdServizioAgg = model.IdServizioAgg,
                        Data = model.Data,
                        Quantita = model.Quantita,
                        Prezzo = model.Prezzo
                    };

                    _addServiziAggService.AddServizioAgg(servizioAgg, model.IdPrenotazione);
                    return RedirectToAction("VisualizzaPrenotazioni", "ManagementVisualizzazioni");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Errore durante l'aggiunta del servizio aggiuntivo.");
                    // Log the exception if needed
                }
            }

            return View("AddServiziAgg", model);
        }
    }
}
