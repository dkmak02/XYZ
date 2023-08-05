using XYZ.Models;

namespace XYZ.Endpoints.Responses
{
    public class SignUpResponse
    {
        public string token { get; init; }
        public UserModel user { get; init; }
    }
}
