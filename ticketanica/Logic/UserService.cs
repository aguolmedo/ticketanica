using ticketanicav2.DataLayer;
using ticketanicav2.Logic.Interfaces;
using ticketanicav2.Models;

namespace ticketanicav2.Logic;

public class UserService : IUsersService
{
    private readonly TicketanicaDbContext _ticketanicaDb;
    
    public UserService(TicketanicaDbContext ticketanicaDb)
    {
        _ticketanicaDb = ticketanicaDb;
    }

    public void RegistrarUsuario(Organizador user)
    {
        
        var userDb = new User
        {
            Email = user.Email,
            Password = user.Password
        }; 
        
        var newUser = _ticketanicaDb.Users.Add(userDb);
        
        if (newUser is null) throw new ArgumentException();
        
        _ticketanicaDb.SaveChanges();
    }

    public bool DeletearUsario(string mail)
    {
        throw new NotImplementedException();
    }

    public bool validarUsuario(User user)
    {
        throw new NotImplementedException();
    }

    public string resetearPassword()
    {
        throw new NotImplementedException();
    }
}