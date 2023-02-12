using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        try
        {
            var eventos = _eventoService.GetAll();
            return eventos is not null ? 
                Ok(eventos) : Problem("Que mierda pasó ahora..");
        }
        catch (Exception e)
        {

            return Problem(e.ToString(), statusCode: 500);
        }
    }

    [HttpPost("crear")]
    public ActionResult<string> CrearEvento(Evento evento)
    {
        try
        {
            var newEventId = _eventoService.AddEvento(evento);
            return Ok("Se creo un evento con Id { " + newEventId + " }");
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.ToString());
        }
    }


}








