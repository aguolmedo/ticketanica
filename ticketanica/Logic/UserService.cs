using System.Text;
using ticketanica.DataLayer;
using ticketanicav2.Helpers;
using ticketanicav2.Logic.Interfaces;
using ticketanicav2.Models;

namespace ticketanicav2.Logic;

public class UserService : IUsersService
{
    private readonly TicketanicaDbContext _ticketanicaDb;

    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserService(TicketanicaDbContext ticketanicaDb, IHttpContextAccessor httpContextAccessor)
    {
        _ticketanicaDb = ticketanicaDb;
        _httpContextAccessor = httpContextAccessor;
    }

    public void RegistrarUsuario(Organizador user)
    {

        if (_ticketanicaDb.Users.Find(user.Email) is not null) throw new ArgumentException("Ya existe un usuario con ese mail");
        var userDb = new User
        {
            Email = user.Email,
            Password = EncryptHelper.GetSha256(user.Password)
        }; 
        
        _ticketanicaDb.Users.Add(userDb);
        _ticketanicaDb.SaveChanges();
    }

    public bool IniciarSesion(Organizador user)
    {
        var usuarioDb = _ticketanicaDb.Users.Find(user.Email);
        if (usuarioDb is null) throw new ArgumentException("El usuario " + user.Email + " no existe");

        if (usuarioDb.Password != EncryptHelper.GetSha256(user.Password))
            throw new ArgumentException("La contraseña no es correcta");
        
        _httpContextAccessor.HttpContext.Session.Set("usuario",Encoding.UTF8.GetBytes(usuarioDb.Email));
        
        return true;
    }

    public bool DeletearUsario(string mail)
    {
        throw new NotImplementedException();
    }
    
    public string ResetearPassword()
    {
        throw new NotImplementedException();
    }
}