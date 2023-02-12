using Microsoft.AspNetCore.Authentication.Cookies;

namespace ticketanicav2.Models;

public class Entrada
{
    public int IdEntrada { get; set; }
    public string Codigo { get; set; }

    public bool Usada { get; set; }

    public Entrada()
    {
        Codigo = "";
        Usada = false;
    }
    
}