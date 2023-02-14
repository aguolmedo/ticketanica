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
    public ActionResult<string> AgregarUsuario(Organizador user) {
        try
        {
            _usersService.RegistrarUsuario(user);
            return Ok("Se registro un usuario (" + user.Email +") correctamente");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            return BadRequest(e);
        }
    }

    [HttpPost("login")]
    public ActionResult<string> IniciarSesion(Organizador user)
    {
        try
        {
            return _usersService.IniciarSesion(user) ? 
                Ok("Se inicio sesion con el usuario: " + user.Email + " correctamente.") : 
                Problem("ni puta idea q mierda pasó");
        }
        catch (ArgumentException e)
        {
            return BadRequest(Convert.ToString(e));
        }
    }
}