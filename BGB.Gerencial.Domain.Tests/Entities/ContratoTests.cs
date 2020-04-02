using BGB.Gerencial.Domain.Entities;
using BGB.Gerencial.Domain.Enums;
using BGB.Gerencial.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BGB.Gerencial.Domain.Tests.Entities
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
            var Taxa = 0.0042;
            var Indice = IndiceContratoEnum.PRE;
            var DataInicial = DateTime.Parse("2020-01-01");
            var DataFinal = DateTime.Parse("2021-01-01");
            var Valor = 1000.00;

            Contrato contrato = new Contrato(Valor, Taxa, Indice, DataInicial, DataFinal);
            //ASSERT
            Assert.AreEqual(contrato.TaxaContratual, "0.42%");
        }

        [TestMethod]
        public void CriarContratoPre()
        {
            var Taxa = 0.0042;
            var Indice = IndiceContratoEnum.PRE;
            var DataInicial = DateTime.Parse("2020-01-01");
            var DataFinal = DateTime.Parse("2021-01-01");
            var Valor = 1000.00;

            Contrato contrato = new Contrato(Valor, Taxa, Indice, DataInicial, DataFinal);
            //ASSERT
            Assert.AreEqual(contrato.Resultados.Count, 0);
            Assert.AreEqual(contrato.Movimentos.Count, 0);
        }

        [TestMethod]
        public void CriarContratoCdi()
        {
            var Taxa = 0.0042;
            var Indice = IndiceContratoEnum.CDI;
            var DataInicial = DateTime.Parse("2020-01-01");
            var DataFinal = DateTime.Parse("2021-01-01");
            var Valor = 1000.00;

            Contrato contrato = new Contrato(Valor, Taxa, Indice, DataInicial, DataFinal);
            //ASSERT
            Assert.AreEqual(contrato.Resultados.Count, 0);
            Assert.AreEqual(contrato.Movimentos.Count, 0);
        }

        [TestMethod]
        //[ExpectedException(typeof(Exception), "Faltam fatores nesse cálculo.")]
        public void GerarResultadosDeContratoCdi()
        {
            var Taxa = 0.0042;
            var Indice = IndiceContratoEnum.CDI;
            var DataInicial = DateTime.Parse("2020-01-01");
            var DataFinal = DateTime.Parse("2021-01-01");
            var Valor = 1000.00;

            Contrato contrato = new Contrato(Valor, Taxa, Indice, DataInicial, DataFinal);
            //ASSERT
            //contrato.Calcular(_cotacoes);
            //Assert.AreEqual(contrato.Resultados.Count, 14);
        }

        [TestMethod]
        //[ExpectedException(typeof(Exception), "Falta cotações do dia 01/01/2020")]
        public void CriarResultadosDeContratoPre()
        {
            var Taxa = 0.0042;
            var Indice = IndiceContratoEnum.PRE;
            var DataInicial = DateTime.Parse("2020-01-01");
            var DataFinal = DateTime.Parse("2021-01-01");
            var Valor = 1000.00;

            Contrato contrato = new Contrato(Valor, Taxa, Indice, DataInicial, DataFinal);
            //ASSERT
            //contrato.Calcular(_cotacoes);
            //Assert.AreEqual(contrato.Resultados.Count, 14);
        }

        [TestMethod]
        //[ExpectedException(typeof(Exception), "Faltam fatores nesse cálculo.")]
        public void CriarResultadosDeContratoCdiComMovimentacao()
        {
            var Taxa = 0.0042;
            var Indice = IndiceContratoEnum.CDI;
            var DataInicial = DateTime.Parse("2020-01-01");
            var DataFinal = DateTime.Parse("2021-01-01");
            var Valor = 1000.00;

            Contrato contrato = new Contrato(Valor, Taxa, Indice, DataInicial, DataFinal);
            //ASSERT
            contrato.Adicionar(new Movimento() { });
            //contrato.Calcular(_cotacoes);
            //Assert.AreEqual(contrato.Resultados.Count, 14);
        }

        [TestMethod]
        //[ExpectedException(typeof(Exception), "Falta cotações do dia 01/01/2020")]
        public void CriarResultadosDeContratoPreComMovimentacao()
        {
            var Taxa = 0.0042;
            var Indice = IndiceContratoEnum.PRE;
            var DataInicial = DateTime.Parse("2020-01-01");
            var DataFinal = DateTime.Parse("2021-01-01");
            var Valor = 1000.00;

            Contrato contrato = new Contrato(Valor, Taxa, Indice, DataInicial, DataFinal);
            //ASSERT
            contrato.Adicionar(new Movimento() { });
            //contrato.Calcular(_cotacoes);
            //Assert.AreEqual(contrato.Resultados.Count, 14);
        }
    }
}
