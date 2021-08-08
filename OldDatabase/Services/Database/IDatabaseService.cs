using SQLite;

namespace OldDatabase.Services.Database
{
    public interface IDatabaseService
    {
        SQLiteAsyncConnection Database { get; set; }
        void Connect(string path);
    }
}