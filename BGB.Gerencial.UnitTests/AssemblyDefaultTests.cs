using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BGB.Gerencial.UnitTests
{
    [TestClass]
    public class AssemblyDefaultTests
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            // Executes once before the test run. (Optional)
            //PessoaFaker.Idade = 10;
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            // Executes once after the test run. (Optional)
            //PessoaFaker.Idade = 60;
        }
    }
}
