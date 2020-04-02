using BGB.Gerencial.Application.Tests.Fakers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BGB.Gerencial.Application.Tests
{
    [TestClass]
    public class AssemblyDefaultTests
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            // Executes once before the test run. (Optional)
            //PessoaFaker.Idade = 10;
            CotacaoFaker.GetAll();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            // Executes once after the test run. (Optional)
            //PessoaFaker.Idade = 60;
        }
    }
}
