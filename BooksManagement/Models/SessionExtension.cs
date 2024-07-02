using System.Text.Json;

namespace BooksManagement.Models
{
    public  static class SessionExtension
    {
        public static void Set<T>(this ISession session,string Key, T value)
        {
            session.SetString(Key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session,string Key) 
        {
            var value = session.GetString(Key);
            return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
        
        }
    }
}
