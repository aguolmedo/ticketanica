using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace ticketanicav2.Helpers;

public static class EncryptHelper
{

    public static string GetSha256(string texto)
    {
        using var sha = new System.Security.Cryptography.SHA256Managed();
        var textBytes = Encoding.UTF8.GetBytes(texto);
        var hashBytes = sha.ComputeHash(textBytes);

        return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
    }


}