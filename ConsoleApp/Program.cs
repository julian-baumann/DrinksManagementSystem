using System;
using System.IO;
using Database;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            var databaseLocation = Path.Combine(path, "DMSDB.db3");

            DatabaseContext.SetDatabasePath(databaseLocation);
        }
    }
}