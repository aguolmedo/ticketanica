using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using SixLabors.ImageSharp.Formats;

namespace ticketanicav2.Helpers.CustomValidations;

public class ValidarImagenAttribute : ValidationAttribute
{
    
    
    private readonly string[] _allowedMimeTypes = { "image/jpeg", "image/png", "image/jpg" };
    private const int _maxSize = 10 * 1024 * 1024; // 10 MB in bytes

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success; // Skip validation if the value is null
        }

        if (value is not IFormFile file)
        {
            return new ValidationResult("Value must be a FormFile."); 
        }

        if (file.Length > _maxSize)
        {
            return new ValidationResult($"File size must be less than {_maxSize / 1024 / 1024} MB."); // 10 Mb
        }
        
        if (!_allowedMimeTypes.Contains(file.ContentType))
        {
            return new ValidationResult("Invalid file type. Only JPEG, PNG and JPG image formats are allowed."); // Check file type
        }

        try
        {
            using var stream = new MemoryStream();
            file.CopyTo(stream);
            stream.Seek(0, SeekOrigin.Begin); // Reset the stream position before reading the image
            using (var image = Image.Load(stream));
            
        }
        catch (Exception)
        {
            return new ValidationResult("Invalid image file. Only JPEG, PNG and JPG image formats are allowed."); // If an exception occurs, the file is invalid
        }

        return ValidationResult.Success;
    }
}