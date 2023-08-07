using MongoDB.Driver;
using MongoDB.Bson;

namespace XYZ.Services.MongoDB
{
    public interface IMongo
    {
        IMongoCollection<T> Conn<T>(string collection);

    }
}
