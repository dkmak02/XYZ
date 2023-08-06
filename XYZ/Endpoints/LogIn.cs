using FastEndpoints;
using XYZ.Endpoints.Requests;
using XYZ.Services.Auth;

namespace XYZ.Endpoints
{
    public class LogIn : Endpoint<LogInRequest>
    {
        private readonly IAuthService _authService;
        public LogIn(IAuthService authService) { 
            _authService = authService;
        }
        public override void Configure()
        {
            Post("users/login");
            AllowAnonymous();
        }
        public override async Task HandleAsync(LogInRequest req, CancellationToken ct)
        {
            var obj = await _authService.CredentialsAreVaild(req.login, req.password);
            await SendAsync(obj);
        }
    }
}
