using System.Data;

namespace OutboxShared.Database;

public interface IOutboxDatabase
{
    IDbConnection Connection { get; }
    bool DataReaderState { get; }

    void DataReaderBusy();
    void DataReaderReady();
    Task<int> ExecuteAsync(string sql);
    Task<IEnumerable<T>> QueryAsync<T>(string sql);
}