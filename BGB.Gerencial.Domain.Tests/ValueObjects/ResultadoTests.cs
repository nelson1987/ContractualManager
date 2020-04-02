using BGB.Gerencial.Domain.Entities;
using BGB.Gerencial.Domain.Enums;
using BGB.Gerencial.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BGB.Gerencial.Domain.Tests.Services
{
    [TestClass]
    public class ResultadoTests
    {
        [TestMethod]
        [DataRow("2020-01-01", "2020-02-01", 3)] //01/01 - 31/01 - 01/02
        [DataRow("2020-01-01", "2020-12-31", 13)] //01/01 - 31/01 -...- 31/10 - 30/11 - 31/12
        [DataRow("2019-12-01", "2020-02-01", 3)] //31/12 - 31/01 - 01/02
        public void ContarResultadosPorContratoSemMovimento(string dataInicial, string dataFinal, int quantidadeResultado)
        {

            var Taxa = 0.0042;
            var Indice = IndiceContratoEnum.CDI;
            var DataInicial = DateTime.Parse("2020-01-01");
            var DataFinal = DateTime.Parse("2021-01-01");
            var Valor = 1000.00;

            Contrato contrato = new Contrato(Valor, Taxa, Indice, DataInicial, DataFinal);
            //ASSERT
            Assert.AreEqual(contrato.DatasPorLinha.Count, quantidadeResultado);
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
            Assert.AreEqual(contrato.Movimentos.Count, 0);
            //Assert.AreEqual(contrato.Resultados.Count, 0);
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
            Assert.AreEqual(contrato.Movimentos.Count, 0);
            //Assert.AreEqual(contrato.Resultados.Count, 0);
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
