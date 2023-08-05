using FastEndpoints;
using XYZ.Endpoints.Requests;
using XYZ.Services.Auth;
using XYZ.Services.Dapper;

namespace XYZ.Endpoints
{
    public class LogIn : Endpoint<LogInRequest>
    {
        private readonly IDapper _dapper;
        private readonly IAuthService _authService;
        public LogIn(IDapper dapper, IAuthService authService) { 
            _dapper = dapper;
            _authService = authService;
        }
        public override void Configure()
        {
            Post("/login");
            AllowAnonymous();
        }
        public override async Task HandleAsync(LogInRequest req, CancellationToken ct)
        {
            var obj = await _authService.CredentialsAreVaild(req.login, req.password);
            await SendAsync(obj);
        }
    }
}
