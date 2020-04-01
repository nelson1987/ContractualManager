using BGB.Gerencial.Application.Services;
using BGB.Gerencial.Application.Tests.Fakers;
using BGB.Gerencial.Data.Interfaces;
using BGB.Gerencial.Domain.Entities;
using BGB.Gerencial.Domain.Enums;
using BGB.Gerencial.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BGB.Gerencial.Application.Tests
{
    [TestClass]
    public class ResultadoPreTests
    {
        private List<Cotacao> _cotacoes { get; set; }
        private List<Movimento> _movimentos { get; set; }
        private ContratoApplication _contratoApplication { get; set; }
        private Mock<ICotacaoRepository> _mockRepository { get; set; }

        [TestInitialize]
        public void Setup()
        {
            _cotacoes = new List<Cotacao>();
            _movimentos = new List<Movimento>();
            _mockRepository = new Mock<ICotacaoRepository>();
            _contratoApplication = new ContratoApplication(_mockRepository.Object);
        }

        [TestMethod]
        public void CriarResultadosDeContratoPreSemMovimentacao()
        {
            Contrato contrato = new Contrato
            {
                Taxa = 0.0042,
                Indice = IndiceContratoEnum.PRE,
                DataInicial = DateTime.Parse("2020-02-12"),
                DataFinal = DateTime.Parse("2020-08-10"),
                Valor = 1732641.63
            };
            //ASSERT
            _mockRepository
                .Setup(x => x.GetByPeriodo(contrato.DataInicial, contrato.DataFinal))
                .Returns(CotacaoFaker.GetAll().Where(x => x.Data >= contrato.DataInicial && x.Data <= contrato.DataFinal).AsQueryable());
            var resultados = _contratoApplication.Calcular(contrato);

            Assert.AreEqual(resultados[0].Data.ToString("yyyy-MM-dd"), "2020-02-12");
            Assert.AreEqual(resultados[1].Data.ToString("yyyy-MM-dd"), "2020-02-29");
            Assert.AreEqual(resultados[2].Data.ToString("yyyy-MM-dd"), "2020-03-31");
            Assert.AreEqual(resultados[3].Data.ToString("yyyy-MM-dd"), "2020-04-30");
            Assert.AreEqual(resultados[4].Data.ToString("yyyy-MM-dd"), "2020-05-31");
            Assert.AreEqual(resultados[5].Data.ToString("yyyy-MM-dd"), "2020-06-30");
            Assert.AreEqual(resultados[6].Data.ToString("yyyy-MM-dd"), "2020-07-31");
            Assert.AreEqual(resultados[7].Data.ToString("yyyy-MM-dd"), "2020-08-10");
        }

        [TestMethod]
        public void CriarResultadosDeContratoPreComMovimentacao()
        {
            Contrato contrato = new Contrato
            {
                Taxa = 0.0042,
                Indice = IndiceContratoEnum.PRE,
                DataInicial = DateTime.Parse("2020-02-12"),
                DataFinal = DateTime.Parse("2020-08-10"),
                Valor = 1732641.63
            };
            //ASSERT
            _movimentos.Add(new Movimento() { Data = DateTime.Parse("2020-02-17"), Valor = -73364.08 });
            contrato.Movimentos.AddRange(_movimentos);

            _mockRepository
                .Setup(x => x.GetByPeriodo(contrato.DataInicial, contrato.DataFinal))
                .Returns(CotacaoFaker.GetAll().Where(x => x.Data >= contrato.DataInicial && x.Data <= contrato.DataFinal).AsQueryable());
            var resultados = _contratoApplication.Calcular(contrato);

            #region Asserts
            Assert.AreEqual(resultados[0].Data.ToString("yyyy-MM-dd"), "2020-02-12");
            Assert.AreEqual(resultados[1].Data.ToString("yyyy-MM-dd"), "2020-02-17");
            Assert.AreEqual(resultados[2].Data.ToString("yyyy-MM-dd"), "2020-02-29");
            Assert.AreEqual(resultados[3].Data.ToString("yyyy-MM-dd"), "2020-03-31");
            Assert.AreEqual(resultados[4].Data.ToString("yyyy-MM-dd"), "2020-04-30");
            Assert.AreEqual(resultados[5].Data.ToString("yyyy-MM-dd"), "2020-05-31");
            Assert.AreEqual(resultados[6].Data.ToString("yyyy-MM-dd"), "2020-06-30");
            Assert.AreEqual(resultados[7].Data.ToString("yyyy-MM-dd"), "2020-07-31");
            Assert.AreEqual(resultados[8].Data.ToString("yyyy-MM-dd"), "2020-08-10");
            //Assert.AreEqual(resultados[9].Data.ToString("yyyy-MM-dd"), "2020-08-31");

            //Assert.AreEqual(contrato.Resultados[0].SaldoInicial, 1732641.63);
            //Assert.AreEqual(contrato.Resultados[1].SaldoInicial, 1736761.57);
            //Assert.AreEqual(contrato.Resultados[2].SaldoInicial, 1736761.57);
            //Assert.AreEqual(contrato.Resultados[3].SaldoInicial, 1736761.57);
            //Assert.AreEqual(contrato.Resultados[4].SaldoInicial, 1736761.57);
            //Assert.AreEqual(contrato.Resultados[5].SaldoInicial, 1736761.57);
            //Assert.AreEqual(contrato.Resultados[6].SaldoInicial, 1736761.57);
            //Assert.AreEqual(contrato.Resultados[7].SaldoInicial, 1736761.57);

            //Assert.AreEqual(contrato.Resultados[0].SaldoFinal, 1732641.63);
            //Assert.AreEqual(contrato.Resultados[1].SaldoFinal, 1736761.57);
            //Assert.AreEqual(contrato.Resultados[2].SaldoFinal, 1736761.57);
            //Assert.AreEqual(contrato.Resultados[3].SaldoFinal, 1736761.57);
            //Assert.AreEqual(contrato.Resultados[4].SaldoFinal, 1736761.57);
            //Assert.AreEqual(contrato.Resultados[5].SaldoFinal, 1736761.57);
            //Assert.AreEqual(contrato.Resultados[6].SaldoFinal, 1736761.57);
            //Assert.AreEqual(contrato.Resultados[7].SaldoFinal, 1736761.57);

            //Assert.AreEqual(contrato.Resultados[0].CustoInicial, -1732641.63);
            //Assert.AreEqual(contrato.Resultados[1].CustoInicial, -1735719.67);
            //Assert.AreEqual(contrato.Resultados[2].CustoInicial, -1737961.68);
            //Assert.AreEqual(contrato.Resultados[3].CustoInicial, -1737961.68);
            //Assert.AreEqual(contrato.Resultados[4].CustoInicial, -1737961.68);
            //Assert.AreEqual(contrato.Resultados[5].CustoInicial, -1737961.68);
            //Assert.AreEqual(contrato.Resultados[6].CustoInicial, -1737961.68);
            //Assert.AreEqual(contrato.Resultados[7].CustoInicial, -1737961.68);

            //Assert.AreEqual(contrato.Resultados[0].CustoFinal, -1732641.63);
            //Assert.AreEqual(contrato.Resultados[1].CustoFinal, -1735719.67);
            //Assert.AreEqual(contrato.Resultados[2].CustoFinal, -1737961.68);
            //Assert.AreEqual(contrato.Resultados[3].CustoFinal, -1737961.68);
            //Assert.AreEqual(contrato.Resultados[4].CustoFinal, -1737961.68);
            //Assert.AreEqual(contrato.Resultados[5].CustoFinal, -1737961.68);
            //Assert.AreEqual(contrato.Resultados[6].CustoFinal, -1737961.68);
            //Assert.AreEqual(contrato.Resultados[7].CustoFinal, -1737961.68);

            //Assert.AreEqual(contrato.Resultados[0].CustoFinal, -1732641.63);
            //Assert.AreEqual(contrato.Resultados[1].CustoFinal, -1735719.67);
            //Assert.AreEqual(contrato.Resultados[2].CustoFinal, -1737961.68);
            //Assert.AreEqual(contrato.Resultados[3].CustoFinal, -1737961.68);
            //Assert.AreEqual(contrato.Resultados[4].CustoFinal, -1737961.68);
            //Assert.AreEqual(contrato.Resultados[5].CustoFinal, -1737961.68);
            //Assert.AreEqual(contrato.Resultados[6].CustoFinal, -1737961.68);
            //Assert.AreEqual(contrato.Resultados[7].CustoFinal, -1737961.68);

            //Assert.AreEqual(contrato.Resultados[0].ResultadoGerencial, 0.00);
            //Assert.AreEqual(contrato.Resultados[1].ResultadoGerencial, 1041.90);
            //Assert.AreEqual(contrato.Resultados[2].ResultadoGerencial, -1200.11);
            //Assert.AreEqual(contrato.Resultados[3].ResultadoGerencial, -1200.11);
            //Assert.AreEqual(contrato.Resultados[4].ResultadoGerencial, -1200.11);
            //Assert.AreEqual(contrato.Resultados[5].ResultadoGerencial, -1200.11);
            //Assert.AreEqual(contrato.Resultados[6].ResultadoGerencial, -1200.11);
            //Assert.AreEqual(contrato.Resultados[7].ResultadoGerencial, -1200.11);

            //Assert.AreEqual(contrato.Resultados[0].CustoInicialConciliacao, -1732641.63);
            //Assert.AreEqual(contrato.Resultados[1].CustoInicialConciliacao, -1735407.78);
            //Assert.AreEqual(contrato.Resultados[2].CustoInicialConciliacao, -1735407.78);
            //Assert.AreEqual(contrato.Resultados[3].CustoInicialConciliacao, -1735407.78);
            //Assert.AreEqual(contrato.Resultados[4].CustoInicialConciliacao, -1735407.78);
            //Assert.AreEqual(contrato.Resultados[5].CustoInicialConciliacao, -1735407.78);
            //Assert.AreEqual(contrato.Resultados[6].CustoInicialConciliacao, -1735407.78);
            //Assert.AreEqual(contrato.Resultados[7].CustoInicialConciliacao, -1735407.78);

            //Assert.AreEqual(contrato.Resultados[0].CustoFinalConciliacao, -1732641.63);
            //Assert.AreEqual(contrato.Resultados[1].CustoFinalConciliacao, -1735407.78);
            //Assert.AreEqual(contrato.Resultados[2].CustoFinalConciliacao, -1735407.78);
            //Assert.AreEqual(contrato.Resultados[3].CustoFinalConciliacao, -1735407.78);
            //Assert.AreEqual(contrato.Resultados[4].CustoFinalConciliacao, -1735407.78);
            //Assert.AreEqual(contrato.Resultados[5].CustoFinalConciliacao, -1735407.78);
            //Assert.AreEqual(contrato.Resultados[6].CustoFinalConciliacao, -1735407.78);
            //Assert.AreEqual(contrato.Resultados[7].CustoFinalConciliacao, -1735407.78);

            //Assert.AreEqual(contrato.Resultados[0].ResultadoConciliacao, 0.00);
            //Assert.AreEqual(contrato.Resultados[1].ResultadoConciliacao, 1353.79);
            //Assert.AreEqual(contrato.Resultados[2].ResultadoConciliacao, 1353.79);
            //Assert.AreEqual(contrato.Resultados[3].ResultadoConciliacao, 1353.79);
            //Assert.AreEqual(contrato.Resultados[4].ResultadoConciliacao, 1353.79);
            //Assert.AreEqual(contrato.Resultados[5].ResultadoConciliacao, 1353.79);
            //Assert.AreEqual(contrato.Resultados[6].ResultadoConciliacao, 1353.79);
            //Assert.AreEqual(contrato.Resultados[7].ResultadoConciliacao, 1353.79);
            #endregion
        }
    }
}
