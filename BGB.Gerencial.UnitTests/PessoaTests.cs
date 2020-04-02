using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BGB.Gerencial.UnitTests
{
    public static class PessoaFaker
    {
        public static List<Pessoa> GetPessoas
        {
            get
            {
                return new List<Pessoa>() {
                new Pessoa(){ Idade = 20, Nome= "Ana"},
                new Pessoa(){ Idade = 40, Nome= "Eva"},
                new Pessoa(){ Idade = 60, Nome= "Maria"},
                new Pessoa(){ Idade = 30, Nome= "Adão"},
            };
            }
        }
    }
    public class Pessoa
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
    }

    [TestClass]
    public class PessoaTests
    {
        private int Idade { get; set; }

        [TestInitialize]
        public void Setup()
        {
            //Idade = 60;
            // Runs before each test. (Optional)
        }

        [TestCleanup]
        public void TearDown()
        {
            //PessoaFaker.GetPessoas.Remove(PessoaFaker.GetPessoas.FirstOrDefault(x => x.Idade == 20));
            // Runs after each test. (Optional)
            Idade = 0;
        }
        [TestMethod]
        [TestCategory("1")]
        public void YouTestMethod2()
        {
             Assert.AreEqual(Idade, 0);
            // Your test code goes here.
        }
        // Mark that this is a unit test method. (Required)
        [TestMethod]
        [TestCategory("2")]
        public void YouTestMethod()
        {
            Assert.AreEqual(PessoaFaker.GetPessoas.FirstOrDefault(x => x.Idade == 20).Idade, 20);
            //Assert.AreEqual(Idade, 0);
            // Your test code goes here.
        }

    }
}
