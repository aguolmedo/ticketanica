using ticketanica.DataLayer;
using ticketanicav2.Models;

namespace ticketanicav2.Logic.Interfaces;

public interface IUsersService
{
    public void RegistrarUsuario(Organizador user);

    public bool DeletearUsario(string mail);

    public bool IniciarSesion(Organizador user);
    
    public string ResetearPassword();
}