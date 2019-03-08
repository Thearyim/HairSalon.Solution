using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HairSalon.Tests
{
    public static class TestInitialization
    {
        [AssemblyInitialize]
        public static void InitializeTests(TestContext context)
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=hairsalon_test;";
        }
    }
}
