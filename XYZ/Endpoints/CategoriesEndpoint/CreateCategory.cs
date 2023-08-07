using XYZ.Models;
using FastEndpoints;
using XYZ.Services.MongoDB;
using MongoDB.Driver;
using MongoDB.Bson;
using XYZ.Endpoints.Requests;

namespace XYZ.Endpoints.CategoriesEndpoint
{
    public class CreateCategory : Endpoint<CategoryRequest>
    {
        private readonly IMongo _mongo;
        public CreateCategory(IMongo mongo)
        {
            _mongo = mongo;
        }
        public override void Configure()
        {
            Post("data/categories");
        }

        public override async Task HandleAsync(CategoryRequest req, CancellationToken ct)
        {
            var con = _mongo.Conn<CategoriesModel>("categories");
            await con.InsertOneAsync(new CategoriesModel()
            {
                Category = req.name,
            });
            await SendAsync(new
            {
                Message = "Created",
            });
            
        }

    }
}