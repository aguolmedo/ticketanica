using Microsoft.AspNetCore.Mvc;
using ticketanicav2.Logic;
using ticketanicav2.Models;

namespace ticketanicav2.Controllers;

[ApiController]
[Route("evento")]
public class EventoController : ControllerBase
{
    private readonly EventosService _eventosService;

    public EventoController(EventosService eventoService)
    {
        _eventosService = eventoService;
    }

    [HttpGet("todos")]
    public ActionResult<List<Evento>> ObtenerEventos()
    {
        var listaEventos = _eventosService.Eventos;
        
        return Ok(listaEventos);
    }

    [HttpPost("crear")]
    public ActionResult<Evento> CrearEventos([FromBody] Evento evento)
    {
        var eventoCreado = _eventosService.CrearEvento(evento.Nombre,evento.Artista, evento.Direccion, evento.CapacidadMaxima);

        return Ok(eventoCreado);
    }
    
    
    
    

}