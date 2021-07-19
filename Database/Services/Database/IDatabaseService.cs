using SQLite;

namespace Database.Services.Database
{
    public interface IDatabaseService
    {
        SQLiteAsyncConnection Database { get; set; }
        void Connect(string path);
    }
}