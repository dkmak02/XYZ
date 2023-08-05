using System.Data.Common;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Data;

namespace XYZ.Services.Dapper
{
    public class DapperM : IDapper
    {
        private readonly IConfiguration _configuration;
        public DapperM(IConfiguration configuration) {
            _configuration = configuration;
        }
        public DbConnection GetDbConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DBConnectionString"));
        }
        public async void Insert<T>(string sql, T model)
        {
            using IDbConnection connection = GetDbConnection();
            connection.Open();
            await connection.ExecuteAsync(sql, model);
            connection.Close();

        }
    }
}
