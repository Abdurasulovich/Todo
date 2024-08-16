using SQLite;
using To_Do.DataAccess.Models;
using Todo.DataAccess;

namespace To_Do.DataAccess;

public class DataContext
{
    
    private SQLiteAsyncConnection _database;
    private async Task Init()
    {
        if(_database is not null) return;

        _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        await _database.CreateTableAsync<TodoModel>();
    }
    
    public async Task<AsyncTableQuery<T>> GetAll<T>() where T : class, new()
    {
        await Init();
        return _database.Table<T>();
    }

    public async Task<T> GetById<T>(long id) where T : class, new()
    {
        await Init();
        return await _database.FindAsync<T>(id);
    }
    public async Task<int> Save<T>(T model) where T : class, new()
    {
        await Init();
        return await _database.InsertAsync(model);
    }

    public async Task<int> Update<T>(T model) where T: class, new()
    {
        await Init();
        return await _database.UpdateAsync(model);
    }

    public async Task<int> Delete<T>(T model) where T: class, new()
    {
        await Init();
        if(model is not null)
        {
            return await _database.DeleteAsync(model);
        }
        return 0;
    }

}
