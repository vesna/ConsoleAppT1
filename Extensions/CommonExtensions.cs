using System.Text.Json;

public static class CommonExtensions
{
    public static string SerializeToJson<T>(this T obj, JsonSerializerOptions options) where T : class
    {
        var jsonString = JsonSerializer.Serialize(obj, options);
        return jsonString;
    }
}
