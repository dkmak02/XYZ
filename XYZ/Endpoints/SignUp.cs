using XYZ.Models;
using FastEndpoints;
using XYZ.Endpoints.Requests;
using XYZ.Endpoints.Responses;
using XYZ.Mappers;
using XYZ.Services.MongoDB;
using XYZ.Services.Auth;
using MongoDB.Driver;

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
            Post("users/sign");
            AllowAnonymous();
        }

        public override async Task HandleAsync(SignUpRequest req, CancellationToken ct)
        {
            var user = Map.ToEntity(req);
            var con = _mongo.Conn<UserModel>("users");
            var checkUsername = (await con.FindAsync(u => u.Username == user.Username)).FirstOrDefault();
            //if (checkUsername is not null)
            //{

            //}
            //var checkEmail = (await con.FindAsync(u => u.Email == user.Email)).FirstOrDefault();
            //if (checkEmail is not null)
            //{

            //}
            await con.InsertOneAsync(user);
            var token = _authService.GetToken(user.Id);
            this.HttpContext.Response.Headers.Add("Authorization", $"Bearer {token}");
            await SendAsync(new SignUpResponse(){ 
                token = token,
                user = user 
             });
        }
           
    }
}