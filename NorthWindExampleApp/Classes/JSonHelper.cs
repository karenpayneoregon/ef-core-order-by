using System.Text.Json;
using System.Text.Json.Serialization;

namespace NorthWindExampleApp.Classes;

public static class JSonHelper
{

    public static void ToJson<T>(this List<T> sender, string fileName)
    {
        File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Json", $"{fileName}.json"),
            JsonSerializer.Serialize(sender, new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 257
            }));
    }

}