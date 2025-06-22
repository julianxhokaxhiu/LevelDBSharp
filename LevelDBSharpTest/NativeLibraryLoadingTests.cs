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
                    Console.WriteLine("Successfully loaded LevelDBSharp!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot load LevelDBSharp: " + ex.Message);
            }
        }
    }
}
