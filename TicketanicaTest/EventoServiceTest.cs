using ticketanicav2.DataLayer;
using ticketanicav2.Logic;

namespace TicketanicaTest;

public class Tests
{
    
    private TicketanicaDbContext _dbContext;
    private EventoService _eventoService;
    
    [SetUp]
    public void SetUp()
    {
        // Inicializa el contexto de la base de datos antes de cada prueba
        _dbContext = new TicketanicaDbContext();
        _eventoService = new EventoService(_dbContext);
    }
    
    [TearDown]
    public void TearDown()
    {
        // Limpia el contexto de la base de datos después de cada prueba
        _dbContext.Dispose();
    }
    
    [Test]
    public void GetAll()
    {
        var result = _eventoService.GetAll();

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<List<ticketanicav2.Models.Evento>>(result);
    }
}