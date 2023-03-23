using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using ticketanicav2.Logic.Interfaces;
using ticketanicav2.Models;

namespace ticketanicav2.Controllers;


[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UserController(IUsersService usersService) {
        _usersService = usersService;
    }

    [HttpPost("useradd")]
    public ActionResult<string> AgregarUsuario([FromForm] Organizador user) {
        try
        {
            _usersService.RegistrarUsuario(user);
            return Ok(JsonSerializer.Serialize($"Se registro un usuario (" + user.Email +") correctamente"));
        }
        catch (ArgumentException e)
        {
            return BadRequest(JsonSerializer.Serialize(e.Message));
        }
    }

    [HttpPost("login")]
    public ActionResult<string> IniciarSesion([FromForm] Organizador user)
    {
        try
        {
            if (_usersService.IniciarSesion(user))
                return Ok(JsonSerializer.Serialize($"Login Ok {user.Email}")); 
            return Problem(JsonSerializer.Serialize("ni puta idea q mierda pasó"));
        }
        catch (ArgumentException e)
        {
            return BadRequest(JsonSerializer.Serialize(e.Message));
        }
    }
}