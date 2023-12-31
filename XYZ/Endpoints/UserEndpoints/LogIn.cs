﻿using FastEndpoints;
using MongoDB.Bson;
using XYZ.Endpoints.Requests;
using XYZ.Services.Auth;

namespace XYZ.Endpoints.UserEndpoints
{
    public class LogIn : Endpoint<LogInRequest>
    {
        private readonly IAuthService _authService;
        public LogIn(IAuthService authService)
        {
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
            var token = ((dynamic)obj).Token;
            HttpContext.Response.Headers.Add("Authorization", $"Bearer {token}");
            await SendAsync(obj);
        }
    }
}
