using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using ticketanicav2.Helpers.CustomValidations;
using ticketanicav2.Logic.Interfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;

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
    public ActionResult<string> GenerarEntrada(int idEvento)
    {
        try
        {
            if (_entradaService.GenerarEntrada(idEvento))
                return Ok(JsonSerializer.Serialize("Entrada Generada."));
            return BadRequest("No se pudo generar entradas, se alcanzo la capacidad máxima, SOLD OUT");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost("validar")]
    public ActionResult<string> ValidarEntrada( int idEvento, string CodigoQr)
    {
        try
        {
            if (_entradaService.ValidarEntrada(idEvento, CodigoQr)) return Ok("Acceso permitido");
            return BadRequest("Acceso Denegado - No existe Ninguna entrada con ese código");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


}