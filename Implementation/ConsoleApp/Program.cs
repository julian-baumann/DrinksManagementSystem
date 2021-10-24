using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            AppCore.Initialize();

            var migration = new Migration();
            await migration.MigrateAll();
        }
    }
}