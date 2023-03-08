using System.Net.Mime;
using MimeDetective;
using MimeDetective.Storage;
using MimeDetective.Storage.Xml.v2;

namespace ticketanicav2.Helpers;

public static class MimeHelper
{
    public static string getMimeTypeFromBytes(IEnumerable<byte> ContentByteArray)
    {
        var Inspector = new ContentInspectorBuilder() {
            Definitions = MimeDetective.Definitions.Default.All()
        }.Build();

        var results = Inspector.Inspect(ContentByteArray).FirstOrDefault().Definition;

        return results.File.MimeType;
    }

    
}

