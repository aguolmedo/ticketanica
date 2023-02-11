using Microsoft.AspNetCore.Mvc;
using ticketanicav2.DataLayer;
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
}