using System.IO;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace HairSalon
{
    public static class TestSetup
    {
        private static bool databaseInitialized = false;
        private static readonly object lockObject = new object();

        static TestSetup()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=hairsalon_test;";
        }

        public static void InitializeDatabase()
        {
            if (!TestSetup.databaseInitialized)
            {
                lock (TestSetup.lockObject)
                {
                    string databaseCreationSql = TestSetup.GetDatabaseCreationSql("theary_im_test.sql");
                    TestSetup.CreateDatabase(databaseCreationSql);
                    TestSetup.databaseInitialized = true;
                }
            }
        }

        private static void CreateDatabase(string sql)
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;user id=root;password=root;port=8889");
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        private static string GetDatabaseCreationSql(string fileName)
        {
            // Get path to the location of the test assembly/.dll
            string testAssemblyPath = Path.GetDirectoryName(Assembly.GetAssembly(typeof(TestSetup)).Location);

            return File.ReadAllText($"{testAssemblyPath}\\{fileName}");
        }
    }
}
