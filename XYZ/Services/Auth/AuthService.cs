using FastEndpoints.Security;
using MongoDB.Driver;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using XYZ.Models;
using XYZ.Services.MongoDB;

namespace XYZ.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IMongo _mongo;
        public AuthService(IMongo mongo) {
            _mongo = mongo;
        }
        public async Task<object> CredentialsAreVaild(string username, string password)
        {
            var con = _mongo.Conn<UserModel>("users");
            var user = (await con.FindAsync(c => c.Username == username)).FirstOrDefault();
            if (user is null)
            {
                return new
                {
                    message = "Invalid data"
                };
            }

            if (!VerifyPassword(password, user.Password))
            {
                return new
                {
                    message = "Invalid password"
                };
            }

            return new
            {
                Token = GetToken(user.Id),
                User = user,
                //Id = ReadIdFromToken(GetToken(user.Id))
                
            };
        }
        private bool VerifyPassword(string enteredPassword, string savedPasswordHash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(enteredPassword, savedPasswordHash);
        }
        public string GetToken(string id)
        {

            return JWTBearer.CreateToken(
                signingKey: "TokenSigningKeyrwerwerqweqwtereqwewtyewewqrert",
                expireAt: DateTime.UtcNow.AddDays(1),
                priviledges: u =>
                {
                    u.Claims.Add(new("Id", id));
                    u.Roles.Add("User");
                    u.Permissions.AddRange(new[] { "ManageUsers", "ManageInventory" });
                });
        }
        public string ReadIdFromToken(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtHandler.ReadJwtToken(token);

            var idClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "Id");

            if (idClaim != null)
            {
                return idClaim.Value;
            }

            return null;
        }
    }
}
