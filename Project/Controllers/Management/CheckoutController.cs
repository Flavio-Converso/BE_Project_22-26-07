using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Services;

[Authorize]
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
        var prenotazione = await _checkoutService.GetPrenotazioneConImportoDaSaldare(idPrenotazione);

        if (prenotazione == null)
        {
            _logger.LogWarning("No prenotazione found for ID {IdPrenotazione}", idPrenotazione);
            return NotFound("Prenotazione not found.");
        }

        return View(prenotazione);
    }
}
