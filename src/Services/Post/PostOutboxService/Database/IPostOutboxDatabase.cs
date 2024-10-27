using System.Data;

namespace PostOutboxService.Database;

public interface IPostOutboxDatabase
{
    IDbConnection Connection { get; }
    bool DataReaderState { get; }

    void DataReaderBusy();
    void DataReaderReady();
    Task<int> ExecuteAsync(string sql);
    Task<IEnumerable<T>> QueryAsync<T>(string sql);
}