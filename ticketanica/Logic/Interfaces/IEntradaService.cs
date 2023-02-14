namespace ticketanicav2.Logic.Interfaces;

public interface IEntradaService
{
    public bool GenerarEntrada(int idEvento);

    public bool ValidarEntrada(int idEvento, string codigoQr);


}