using BGB.Gerencial.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BGB.Gerencial.Domain.Tests
{
    [TestClass]
    public class CalculoSaldoInicialTests
    {
        [TestMethod]
        [DataRow(0.0042, 1829846.61, 1845249.60)]
        public void CalcularSaldoInicialPreComAtraso(double taxaContratual, double saldoFinalResultadoAnterior, double valorEsperado)
        {
            Calculadora calculadora = new Calculadora();
            var saldo = Math.Round(calculadora.CalcularSaldoInicialPreComAtraso(taxaContratual, saldoFinalResultadoAnterior), 2);
            Assert.AreEqual(saldo, valorEsperado);
        }

        [TestMethod]
        [DataRow(0.0042, 1829846.61, 1849124.33)]
        public void CalcularSaldoInicialCdiComAtraso(double taxaContratual, double saldoFinalResultadoAnterior, double valorEsperado)
        {
            var cdiDatainicial = 18.25870568007;
            var cdiDataFinal = 18.2970461;

            Calculadora calculadora = new Calculadora();
            var saldo = Math.Round(calculadora.CalcularSaldoInicialCdiComAtraso(taxaContratual, saldoFinalResultadoAnterior, cdiDatainicial, cdiDataFinal), 2);
            Assert.AreEqual(saldo, valorEsperado);
        }

        [TestMethod]
        [DataRow(0.0042, 1829846.61, 1832148.84)]
        public void CalcularSaldoInicialPreSemAtraso(double taxaContratual, double saldoFinalResultadoAnterior, double valorEsperado)
        {
            Calculadora calculadora = new Calculadora();
            var saldo = Math.Round(calculadora.CalcularSaldoInicialPreSemAtraso(taxaContratual, saldoFinalResultadoAnterior, DateTime.Parse("2020-02-20"), DateTime.Parse("2020-02-29")), 2);
            Assert.AreEqual(saldo, valorEsperado);
        }

        [TestMethod]
        [DataRow(0.0042, 1829846.61, 1833627.58)]
        public void CalcularSaldoInicialCdiSemAtraso(double taxaContratual, double saldoFinalResultadoAnterior, double valorEsperado)
        {
            var cdiDatainicial = 18.25870568007;
            var cdiDataFinal = 18.27344247;
            Calculadora calculadora = new Calculadora();
            var saldo = Math.Round(calculadora.CalcularSaldoInicialCdiSemAtraso(taxaContratual, saldoFinalResultadoAnterior, cdiDatainicial, cdiDataFinal, DateTime.Parse("2020-02-20"), DateTime.Parse("2020-02-29")), 2);
            Assert.AreEqual(saldo, valorEsperado);
        }
    }
    [TestClass]
    public class ResultadoTests
    {
        [TestMethod]
        [DataRow(0.0042, 1829846.61, "2020-02-20", "2020-02-29", 1833627.58)]
        public void CriarResultadoInicial(double taxaContratual, double saldoFinalResultadoAnterior, string dataResultadoAtual, string dataResultadoAnterior, double valorEsperado)
        {
            var cdiDataInicial = 18.25870568007;
            var cdiDataFinal = 18.27344247;
            Resultado resultado = new Resultado();
            resultado.SaldoInicial = new Calculadora("CDI",taxaContratual, saldoFinalResultadoAnterior,cdiDataInicial,cdiDataFinal, DateTime.Parse(dataResultadoAtual), DateTime.Parse(dataResultadoAnterior), 0, 0);
            Assert.AreEqual(resultado.SaldoInicial.Valor, valorEsperado);
        }
    }
}
