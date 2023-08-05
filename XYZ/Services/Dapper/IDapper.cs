using System.Data.Common;

namespace XYZ.Services.Dapper
{
    public interface IDapper
    {
        DbConnection GetDbConnection();
        void Insert<T>(string sql, T model);
    }
}
