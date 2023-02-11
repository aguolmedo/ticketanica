namespace ticketanicav2.Models;

public class Organizador
{
    public string Email { get; set; } = null!;

    public string? Password { get; set; } = null!;


    public Organizador(string email)
    {
        Email = email;
    }

    public Organizador() {}


}