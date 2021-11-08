using System.Text.Json;

namespace Recrutify.DataAccess.Models
{
    public class ErrorDetails
    {
        public int StatusCorde { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
