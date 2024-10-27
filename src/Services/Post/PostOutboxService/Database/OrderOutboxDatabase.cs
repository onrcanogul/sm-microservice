using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace PostOutboxService.Database;

public class OrderOutboxDatabase(IConfiguration configuration)
{
    readonly IDbConnection _dbConnection = new SqlConnection(configuration.GetConnectionString("PostgresConnection"));

    public IDbConnection Connection
    {
        get
        {
            if(_dbConnection.State == ConnectionState.Closed)
                _dbConnection.Open();
            return _dbConnection;
        }
    }
    public bool dataReaderState = true;
    public async Task<IEnumerable<T>> QueryAsync<T>(string sql) => await _dbConnection.QueryAsync<T>(sql);
    public async Task<int> ExecuteAsync(string sql) => await _dbConnection.ExecuteAsync(sql);
    public void DataReaderBusy() => dataReaderState = false; //singleton!
    public void DataReaderReady() => dataReaderState = true;
    public bool DataReaderState => dataReaderState;
}