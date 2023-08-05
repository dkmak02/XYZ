namespace XYZ.Endpoints.Requests
{
    public class SignUpRequest
    {
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string passwordConfirm { get; set; }
    }
}
