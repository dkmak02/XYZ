using FastEndpoints;
using FastEndpoints.Security;
using XYZ.Endpoints.Requests;
using XYZ.Endpoints.Responses;
using XYZ.Models;

namespace XYZ.Mappers
{
    public class UserMapper : Mapper<SignUpRequest, SignUpResponse, UserModel>
    {
        public override UserModel ToEntity(SignUpRequest req) => new UserModel
        {
            Username = req.username,
            Email = req.email,
            Active = true,
            Password = req.password,
            Role = "user"
        };



    }
}
