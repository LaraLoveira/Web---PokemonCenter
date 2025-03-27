using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace LoveiraNoresLaraTarea4.Extensions
{
    public static class SessionExtensions
    {
        //Método para guardar un objeto en la sesión
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                WriteIndented = true
            };
            session.SetString(key, JsonSerializer.Serialize(value, options));
        }

        //Método para recuperar un objeto de la sesión
        public static T? GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            if (value == null) return default;

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                WriteIndented = true
            };
            return JsonSerializer.Deserialize<T>(value, options);
        }
    }
}
