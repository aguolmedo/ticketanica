
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ticketanicav2.DataLayer;

namespace ticketanicav2.Logic;

public  class EventoService
{
    private readonly TicketanicaDbContext _ticketanicaDb;

    public EventoService(TicketanicaDbContext ticketanicaDb)
    {
        _ticketanicaDb = _ticketanicaDb;
    }

    


}