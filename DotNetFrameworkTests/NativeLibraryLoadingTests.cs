using System;
using LevelDB;

namespace DotNetFrameworkTests
{
    class DotNetFrameworkTest
    {
        static void Main(string[] args)
        {
            try
            {
                using (new DB(new Options { CreateIfMissing = true }, "LoadingTest"))
                {
                    Console.WriteLine("Successfully loaded LevelDB.NET!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot load LevelDB.NET: " + ex.Message);
            }
        }
    }
}
