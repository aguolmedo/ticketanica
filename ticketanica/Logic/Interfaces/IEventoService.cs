using ticketanicav2.Models;

namespace ticketanicav2.Logic.Interfaces;

public interface IEventoService
{
    public List<Evento> GetAll();
    
    public bool AddEvento(Evento evento);

    public bool ModifyEvento(int idEvento);
    
    
}