using Microsoft.AspNetCore.Authentication.Cookies;

namespace ticketanicav2.Models;

public class Entrada
{
    public int IdEntrada { get; set; }
    public string CodigoQr { get; set; }
    public bool Usada { get; set; }
    public DateTime? FechaCreacion { get; set; }
    public Entrada() {}
    
}