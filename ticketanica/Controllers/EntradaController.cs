using Microsoft.AspNetCore.Mvc;
using ticketanicav2.Logic.Interfaces;

namespace ticketanicav2.Controllers;


[ApiController]
[Route("entrada")]
public class EntradaController : ControllerBase
{
    private readonly IEntradaService _entradaService;


    public EntradaController(IEntradaService entradaService)
    {
        _entradaService = entradaService;
    }

    [HttpPost("generar")]
    public ActionResult<bool> GenerarEntrada(int idEvento)
    {
        try
        {
            if (_entradaService.GenerarEntrada(idEvento))
                return Ok($"Se genero una entrada para el evento: {idEvento}, correctamente.");
            return BadRequest("No se pudo generar entradas, se alcanzo la capacidad máxima, SOLD OUT");
        }
        catch (Exception e)
        {
            return BadRequest(e.ToString());
        }
    }


}