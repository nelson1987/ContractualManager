using BGB.Gerencial.Domain.Entities;
using BGB.Gerencial.Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BGB.Gerencial.Domain.Tests.Services
{
    [TestClass]
    public class ContratoTests
    {
        private List<Cotacao> _cotacoes { get; set; }
        [TestInitialize]
        public void Setup()
        {
            _cotacoes = new List<Cotacao>();
        }

        [TestMethod]
        public void CriarContratoPre()
        {
            Contrato contrato = new Contrato();
            contrato.Taxa = 0.0042;
            contrato.Indice = IndiceContratoEnum.PRE;
            contrato.DataInicial = DateTime.Parse("2020-01-01");
            contrato.DataFinal = DateTime.Parse("2021-01-01");
            contrato.Valor = 1000.00;
            //ASSERT
            Assert.AreEqual(contrato.Movimentos.Count, 0);
            Assert.AreEqual(contrato.Resultados.Count, 0);
        }

        [TestMethod]
        public void CriarContratoCdi()
        {
            Contrato contrato = new Contrato();
            contrato.Taxa = 0.0042;
            contrato.Indice = IndiceContratoEnum.CDI;
            contrato.DataInicial = DateTime.Parse("2020-01-01");
            contrato.DataFinal = DateTime.Parse("2021-01-01");
            contrato.Valor = 1000.00;
            //ASSERT
            Assert.AreEqual(contrato.Movimentos.Count, 0);
            Assert.AreEqual(contrato.Resultados.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Faltam fatores nesse cálculo.")]
        public void GerarResultadosDeContratoCdi()
        {
            Contrato contrato = new Contrato();
            contrato.Taxa = 0.0042;
            contrato.Indice = IndiceContratoEnum.CDI;
            contrato.DataInicial = DateTime.Parse("2020-01-01");
            contrato.DataFinal = DateTime.Parse("2021-01-01");
            contrato.Valor = 1000.00;
            //ASSERT
            contrato.Calcular(_cotacoes);
            //Assert.AreEqual(contrato.Resultados.Count, 14);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Falta cotações do dia 01/01/2020")]
        public void CriarResultadosDeContratoPre()
        {
            Contrato contrato = new Contrato();
            contrato.Taxa = 0.0042;
            contrato.Indice = IndiceContratoEnum.PRE;
            contrato.DataInicial = DateTime.Parse("2020-01-01");
            contrato.DataFinal = DateTime.Parse("2021-01-01");
            contrato.Valor = 1000.00;
            //ASSERT
            contrato.Calcular(_cotacoes);
            //Assert.AreEqual(contrato.Resultados.Count, 14);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Faltam fatores nesse cálculo.")]
        public void CriarResultadosDeContratoCdiComMovimentacao()
        {
            Contrato contrato = new Contrato();
            contrato.Taxa = 0.0042;
            contrato.Indice = IndiceContratoEnum.CDI;
            contrato.DataInicial = DateTime.Parse("2020-01-01");
            contrato.DataFinal = DateTime.Parse("2021-01-01");
            contrato.Valor = 1000.00;
            //ASSERT
            contrato.Movimentos.Add(new Movimento() { });
            contrato.Calcular(_cotacoes);
            //Assert.AreEqual(contrato.Resultados.Count, 14);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Falta cotações do dia 01/01/2020")]
        public void CriarResultadosDeContratoPreComMovimentacao()
        {
            Contrato contrato = new Contrato();
            contrato.Taxa = 0.0042;
            contrato.Indice = IndiceContratoEnum.PRE;
            contrato.DataInicial = DateTime.Parse("2020-01-01");
            contrato.DataFinal = DateTime.Parse("2021-01-01");
            contrato.Valor = 1000.00;
            //ASSERT
            contrato.Movimentos.Add(new Movimento() { });
            contrato.Calcular(_cotacoes);
            Assert.AreEqual(contrato.Resultados.Count, 14);
        }
    }
}
