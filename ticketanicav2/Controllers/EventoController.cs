using Microsoft.AspNetCore.Mvc;
using ticketanicav2.DataLayer;
using ticketanicav2.Logic;
using ticketanicav2.Logic.Interfaces;
using Evento = ticketanicav2.Models.Evento;

namespace ticketanicav2.Controllers;

[ApiController]
[Route("evento")]
public class EventoController : ControllerBase
{
    private readonly IEventoService _eventoService;

    public EventoController(IEventoService eventoService)
    {
        _eventoService = eventoService;
    }

    [HttpGet("all")]
    public ActionResult<List<Evento>> ObtenerEventos()
    {
        return Ok(_eventoService.GetAll());
    }


}








