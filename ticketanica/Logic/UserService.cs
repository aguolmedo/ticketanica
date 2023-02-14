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
        var userDb = new User
        {
            Email = user.Email,
            Password = EncryptHelper.GetSha256(user.Password)
        }; 
        
        var newUser = _ticketanicaDb.Users.Add(userDb);
        
        if (newUser is null) throw new ArgumentException();
        
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
    
    
    
    public bool ValidarUsuario(User user)
    {
        throw new NotImplementedException();
    }

    public string ResetearPassword()
    {
        throw new NotImplementedException();
    }
}