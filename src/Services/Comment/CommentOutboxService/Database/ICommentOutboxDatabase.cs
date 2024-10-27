using System.Data;

namespace CommentOutboxService.Database;

public interface ICommentOutboxDatabase
{
    IDbConnection Connection { get; }
    bool DataReaderState { get; }

    void DataReaderBusy();
    void DataReaderReady();
    Task<int> ExecuteAsync(string sql);
    Task<IEnumerable<T>> QueryAsync<T>(string sql);
}