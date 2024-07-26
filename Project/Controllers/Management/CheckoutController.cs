using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Services;

namespace Project.Controllers.Management
{
    [Authorize] // Ensure that the controller requires authentication
    public class CheckoutController : Controller
    {
        private readonly ILogger<CheckoutController> _logger;
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService, ILogger<CheckoutController> logger)
        {
            _checkoutService = checkoutService;
            _logger = logger;
        }

        [HttpGet("Checkout")]
        public async Task<IActionResult> Checkout(int idPrenotazione)
        {
            // Retrieve the reservation and associated services
            var prenotazione = await _checkoutService.GetPrenotazioneConImportoDaSaldare(idPrenotazione);

            if (prenotazione == null)
            {
                // Log a warning if the reservation is not found
                _logger.LogWarning("No prenotazione found for ID {IdPrenotazione}", idPrenotazione);
                return NotFound("Prenotazione not found.");
            }

            // Return the "Checkout" view with the prenotazione model
            return View("Checkout", prenotazione);
        }
    }
}
