using ticketanicav2.Models;

namespace ticketanicav2.Logic.Interfaces;

public interface IEventoService
{
    public List<Evento> GetAll();

    public Evento GetById(int id);

    public byte[] GetImageById(int id);
    
    public int AddEvento(Evento evento);
    
    public bool ModifyEvento(int idEvento);


}