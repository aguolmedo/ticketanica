using System.ComponentModel.DataAnnotations;

namespace ticketanicav2.Helpers.CustomValidations;

public class ValidarSesionAttribute : ValidationAttribute
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ValidarSesionAttribute()
    {
        _httpContextAccessor = new HttpContextAccessor();
    }
    
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext?.Session == null || !httpContext.Session.Keys.Contains("usuario"))
        {
            return new ValidationResult("Debe iniciar sesión para realizar esta acción.");
        }

        return ValidationResult.Success;
    }
}