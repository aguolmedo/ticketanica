using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace ticketanicav2.Models;

public class Organizador
{
    [EmailAddress]
    public string? Email { get; init; }

    [RegularExpression(@"^(?=.*\d).{8,24}")]
    public string? Password { get; set; } = null!;


    public Organizador(string email)
    {
        Email = email;
    }

    public Organizador() {}
    
}