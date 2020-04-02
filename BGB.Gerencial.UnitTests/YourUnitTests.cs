using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BGB.Gerencial.UnitTests
{
    [TestClass]
    public class YourUnitTests
    {
        private int Idade { get; set; }

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            // Executes once for the test class. (Optional)
            //PessoaFaker.Idade = 20;
        }

        [TestInitialize]
        public void Setup()
        {
            // Runs before each test. (Optional)
            Idade = 30;
        }

        [ClassCleanup]
        public static void TestFixtureTearDown()
        {
            // Runs once after all tests in this class are executed. (Optional)
            // Not guaranteed that it executes instantly after all tests from the class.

           // PessoaFaker.Idade = 50;
        }

        [TestCleanup]
        public void TearDown()
        {
            // Runs after each test. (Optional)
            Idade = 40;
            //PessoaFaker.Idade = 0;
        }

        // Mark that this is a unit test method. (Required)
        [TestMethod]
        public void YouTestMethod()
        {
            //Assert.AreEqual(PessoaFaker.Idade, 20);
            Assert.AreEqual(Idade, 30);
            // Your test code goes here.
        }

        [TestMethod]
        public void YouTestMethod2()
        {
           // Assert.AreEqual(PessoaFaker.Idade, 20);
            Assert.AreEqual(Idade, 30);
            // Your test code goes here.
        }
    }
}
