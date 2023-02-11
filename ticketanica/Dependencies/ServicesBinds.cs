using ticketanicav2.Logic;
using ticketanicav2.Logic.Interfaces;

namespace ticketanicav2.Dependencies;

public static class ServicesBinds
{
    public static void BindsServices(this IServiceCollection services)
    {
        services.AddScoped<IEventoService, EventoService>();
    }
}