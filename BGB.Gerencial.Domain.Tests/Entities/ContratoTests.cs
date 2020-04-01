using BGB.Gerencial.Domain.Entities;
using BGB.Gerencial.Domain.Enums;
using BGB.Gerencial.Domain.ValueObjects;
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
        public void EscreverTaxaContratual()
        {
            Contrato contrato = new Contrato
            {
                Taxa = 0.0042,
                Indice = IndiceContratoEnum.PRE,
                DataInicial = DateTime.Parse("2020-01-01"),
                DataFinal = DateTime.Parse("2021-01-01"),
                Valor = 1000.00
            };
            //ASSERT
            Assert.AreEqual(contrato.TaxaContratual, "0.42%");
            Assert.AreEqual(contrato.Resultados.Count, 0);
            Assert.AreEqual(contrato.Movimentos.Count, 0);
        }

        [TestMethod]
        public void CriarContratoPre()
        {
            Contrato contrato = new Contrato
            {
                Taxa = 0.0042,
                Indice = IndiceContratoEnum.PRE,
                DataInicial = DateTime.Parse("2020-01-01"),
                DataFinal = DateTime.Parse("2021-01-01"),
                Valor = 1000.00
            };
            //ASSERT
            Assert.AreEqual(contrato.Resultados.Count, 0);
            Assert.AreEqual(contrato.Movimentos.Count, 0);
        }

        [TestMethod]
        public void CriarContratoCdi()
        {
            Contrato contrato = new Contrato
            {
                Taxa = 0.0042,
                Indice = IndiceContratoEnum.CDI,
                DataInicial = DateTime.Parse("2020-01-01"),
                DataFinal = DateTime.Parse("2021-01-01"),
                Valor = 1000.00
            };
            //ASSERT
            Assert.AreEqual(contrato.Resultados.Count, 0);
            Assert.AreEqual(contrato.Movimentos.Count, 0);
        }

        [TestMethod]
        //ExpectedException(typeof(Exception), "Faltam fatores nesse cálculo.")]
        public void GerarResultadosDeContratoCdi()
        {
            Contrato contrato = new Contrato
            {
                Taxa = 0.0042,
                Indice = IndiceContratoEnum.CDI,
                DataInicial = DateTime.Parse("2020-01-01"),
                DataFinal = DateTime.Parse("2021-01-01"),
                Valor = 1000.00
            };
            //ASSERT
            //contrato.Calcular(_cotacoes);
            //Assert.AreEqual(contrato.Resultados.Count, 14);
        }

        [TestMethod]
        //[ExpectedException(typeof(Exception), "Falta cotações do dia 01/01/2020")]
        public void CriarResultadosDeContratoPre()
        {
            Contrato contrato = new Contrato
            {
                Taxa = 0.0042,
                Indice = IndiceContratoEnum.PRE,
                DataInicial = DateTime.Parse("2020-01-01"),
                DataFinal = DateTime.Parse("2021-01-01"),
                Valor = 1000.00
            };
            //ASSERT
            //contrato.Calcular(_cotacoes);
            //Assert.AreEqual(contrato.Resultados.Count, 14);
        }

        [TestMethod]
        //[ExpectedException(typeof(Exception), "Faltam fatores nesse cálculo.")]
        public void CriarResultadosDeContratoCdiComMovimentacao()
        {
            Contrato contrato = new Contrato
            {
                Taxa = 0.0042,
                Indice = IndiceContratoEnum.CDI,
                DataInicial = DateTime.Parse("2020-01-01"),
                DataFinal = DateTime.Parse("2021-01-01"),
                Valor = 1000.00
            };
            //ASSERT
            contrato.Movimentos.Add(new Movimento() { });
            //contrato.Calcular(_cotacoes);
            //Assert.AreEqual(contrato.Resultados.Count, 14);
        }

        [TestMethod]
        //[ExpectedException(typeof(Exception), "Falta cotações do dia 01/01/2020")]
        public void CriarResultadosDeContratoPreComMovimentacao()
        {
            Contrato contrato = new Contrato
            {
                Taxa = 0.0042,
                Indice = IndiceContratoEnum.PRE,
                DataInicial = DateTime.Parse("2020-01-01"),
                DataFinal = DateTime.Parse("2021-01-01"),
                Valor = 1000.00
            };
            //ASSERT
            contrato.Movimentos.Add(new Movimento() { });
            //contrato.Calcular(_cotacoes);
            //Assert.AreEqual(contrato.Resultados.Count, 14);
        }
    }
}
