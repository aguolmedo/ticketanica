using Microsoft.AspNetCore.Authentication.Cookies;

namespace ticketanicav2.Models;

public class Entrada
{
    private int Codigo { get; set; }

    private bool Usada { get; set; }

    public Entrada()
    {
        Codigo = Convert.ToInt32(new Random().NextInt64());
        Usada = false;
    }
}