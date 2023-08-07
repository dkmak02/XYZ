using XYZ.Models;
using FastEndpoints;
using XYZ.Services.MongoDB;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace XYZ.Endpoints.CategoriesEndpoint
{
    public class SendCategories : EndpointWithoutRequest
    {
        private readonly IMongo _mongo;
        public SendCategories(IMongo mongo)
        {
            _mongo = mongo;
        }
        public override void Configure()
        {
            Get("data/categories");
            Roles("User");
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var con = _mongo.Conn<CategoriesModel>("categories");
            var categories = await con.Find(new BsonDocument())
                          .Project(c => c.Category)
                          .ToListAsync();
            await SendAsync(new
            {
                categories = categories,
            });
        }

    }
}