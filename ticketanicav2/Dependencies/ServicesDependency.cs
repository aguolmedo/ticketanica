using ticketanicav2.Logic;

namespace ticketanicav2.Dependencies;

public static class ServicesDependency
{
    public static void AddServiceDependency(this IServiceCollection services)
    {
        services.AddSingleton<EventoService>();
    }
}