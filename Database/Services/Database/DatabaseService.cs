using SQLite;

namespace Database.Services.Database
{
    public class DatabaseService : IDatabaseService
    {
        public SQLiteAsyncConnection Database { get; set; }

        public void Connect(string path)
        {
            Database = new SQLiteAsyncConnection(path);
        }
    }
}