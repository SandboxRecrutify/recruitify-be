using System.Text.Json;

namespace Recrutify.DataAccess.Extensions
{
    public static class ModelExtensions
    {
        public static T DeepCopy<T>(this T obj)
        {
            var serialized = JsonSerializer.Serialize(obj);
            return JsonSerializer.Deserialize<T>(serialized);
        }
    }
}
