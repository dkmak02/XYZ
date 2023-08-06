using XYZ.Models;
using FastEndpoints;
using XYZ.Endpoints.Requests;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using FastEndpoints.Security;
using XYZ.Endpoints.Responses;
using XYZ.Mappers;
using XYZ.Services.MongoDB;
using XYZ.Services.Auth;

namespace XYZ.Endpoints
{
    public class SignUp : Endpoint<SignUpRequest, SignUpResponse, UserMapper>
    { 
        private readonly IMongo _mongo;
        private readonly IAuthService _authService;
        public SignUp(IMongo mongo, IAuthService authService) {
            _mongo = mongo;
            _authService = authService;
        }
        public override void Configure()
        {
            Post("/sign");
            AllowAnonymous();
        }

        public override async Task HandleAsync(SignUpRequest req, CancellationToken ct)
        {
            try
            {
                var user = Map.ToEntity(req);

                var con = _mongo.Conn<UserModel>("users");

                await con.InsertOneAsync(user);

                await SendAsync(new (){ 
                    token = _authService.GetToken(user.Id),
                    user = user 
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
           
    }
}