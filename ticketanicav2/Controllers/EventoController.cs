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
        var listaEventos = _eventoService.GetAll();
        
        return Ok(listaEventos);
    }


}








