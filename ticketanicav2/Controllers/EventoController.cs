using Microsoft.AspNetCore.Mvc;
using ticketanicav2.DataLayer;
using ticketanicav2.Logic;

namespace ticketanicav2.Controllers;

[ApiController]
[Route("evento")]
public class EventoController : ControllerBase
{
    private readonly TicketanicaDbContext _ticketanicaDb;

    public EventoController(TicketanicaDbContext ticketanicaDb)
    {
        _ticketanicaDb = ticketanicaDb;
    }

    [HttpGet("all")]
    public ActionResult<List<Evento>> ObtenerEventos()
    {
        return Ok(_ticketanicaDb.Eventos.ToList());
    }


}








