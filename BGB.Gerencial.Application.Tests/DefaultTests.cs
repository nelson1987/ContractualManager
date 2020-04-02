using BGB.Gerencial.Application.Models;
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
    public class DefaultTests
    {
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            _cotacoes = new List<Cotacao>();
            _movimentos = new List<Movimento>();
            _movimentos.Add(new Movimento() { Data = DateTime.Parse("2020-01-16"), Valor = -73324.55 });
            _movimentos.Add(new Movimento() { Data = DateTime.Parse("2020-02-17"), Valor = -73364.08 });
            _mockRepository = new Mock<ICotacaoRepository>();
            _contratoApplication = new ContratoApplication(_mockRepository.Object);

        }
        private List<Cotacao> _cotacoes { get; set; }
        private List<Movimento> _movimentos { get; set; }
        private List<ResultadoDTO> _resultados { get; set; }
        private ContratoApplication _contratoApplication { get; set; }
        private Mock<ICotacaoRepository> _mockRepository { get; set; }
        [TestInitialize]
        public void Setup()
        {
            _cotacoes = new List<Cotacao>();
            _movimentos = new List<Movimento>();
            _movimentos.Add(new Movimento() { Data = DateTime.Parse("2020-01-16"), Valor = -73324.55 });
            _movimentos.Add(new Movimento() { Data = DateTime.Parse("2020-02-17"), Valor = -73364.08 });
            _mockRepository = new Mock<ICotacaoRepository>();
            _contratoApplication = new ContratoApplication(_mockRepository.Object);
        }

        [TestMethod]
        public void CriarResultadosDeContratoCdiComMovimentacao()
        {
            Contrato contrato = new Contrato
            {
                Taxa = 0.0045,
                Indice = IndiceContratoEnum.CDI,
                DataInicial = DateTime.Parse("2019-12-16"),
                DataFinal = DateTime.Parse("2022-12-16"),
                Valor = 2042610.95
            };

            //ASSERT
            contrato.Movimentos.AddRange(_movimentos);

            _mockRepository
                .Setup(x => x.GetByPeriodo(contrato.DataInicial, contrato.DataFinal))
                .Returns(CotacaoFaker.GetAll().Where(x => x.Data >= contrato.DataInicial && x.Data <= contrato.DataFinal).AsQueryable());
            var resultados = _contratoApplication.Calcular(contrato);

            #region Asserts

            Assert.AreEqual(_resultados[0].Data.ToString("dd/MM/yyyy"), "31/12/2019");
            Assert.AreEqual(_resultados[0].ValorMovimento, 0.00);
            Assert.AreEqual(_resultados[0].SaldoInicial, 2042610.95);
            Assert.AreEqual(_resultados[0].SaldoFinal, 2042610.95);
            Assert.AreEqual(_resultados[0].CustoInicial, -2042610.95);
            Assert.AreEqual(_resultados[0].CustoFinal, -2042610.95);
            Assert.AreEqual(_resultados[0].ResultadoGerencial, 0.00, 0.02);
            Assert.AreEqual(_resultados[0].CustoInicialConciliacao, -2042610.95, 0.02);
            Assert.AreEqual(_resultados[0].CustoFinalConciliacao, -2042610.95, 0.02);
            Assert.AreEqual(_resultados[0].ResultadoConciliacao, 0.00, 0.02);
            Assert.AreEqual(_resultados[0].DiasAtraso, 0);

            Assert.AreEqual(_resultados[1].Data.ToString("dd/MM/yyyy"), "16/01/2020");
            Assert.AreEqual(_resultados[1].ValorMovimento, -73324.55);
            Assert.AreEqual(_resultados[1].SaldoInicial, 2051360.26);
            Assert.AreEqual(_resultados[1].SaldoFinal, 1978035.71);
            Assert.AreEqual(_resultados[1].CustoInicial, -2046453.91);
            Assert.AreEqual(_resultados[1].CustoFinal, -1973129.36);
            Assert.AreEqual(_resultados[1].ResultadoGerencial, 4906.34, 0.02);
            Assert.AreEqual(_resultados[1].CustoInicialConciliacao, -2046411.76, 0.02);
            Assert.AreEqual(_resultados[1].CustoFinalConciliacao, -1973087.21, 0.02);
            Assert.AreEqual(_resultados[1].ResultadoConciliacao, 4948.50, 0.02);
            Assert.AreEqual(_resultados[1].DiasAtraso, 0);

            Assert.AreEqual(_resultados[2].Data.ToString("dd/MM/yyyy"), "31/01/2020");
            Assert.AreEqual(_resultados[2].ValorMovimento, 0.00);
            Assert.AreEqual(_resultados[2].SaldoInicial, 1986211.12, 0.02);
            Assert.AreEqual(_resultados[2].SaldoFinal, 1986211.12, 0.02);
            Assert.AreEqual(_resultados[2].CustoInicial, -1976841.60);
            Assert.AreEqual(_resultados[2].CustoFinal, -1976841.60);
            Assert.AreEqual(_resultados[2].ResultadoGerencial, 9369.52, 0.02);
            Assert.AreEqual(_resultados[2].CustoInicialConciliacao, -1976758.42, 0.02);
            Assert.AreEqual(_resultados[2].CustoFinalConciliacao, -1976758.42, 0.02);
            Assert.AreEqual(_resultados[2].ResultadoConciliacao, 9452.70, 0.02);
            Assert.AreEqual(_resultados[2].DiasAtraso, 0);

            Assert.AreEqual(_resultados[3].Data.ToString("dd/MM/yyyy"), "17/02/2020");
            Assert.AreEqual(_resultados[3].ValorMovimento, -73364.08);
            Assert.AreEqual(_resultados[3].SaldoInicial, 1994884.49);
            Assert.AreEqual(_resultados[3].SaldoFinal, 1921520.41);
            Assert.AreEqual(_resultados[3].CustoInicial, -1980428.87);
            Assert.AreEqual(_resultados[3].CustoFinal, -1907064.79);
            Assert.AreEqual(_resultados[3].ResultadoGerencial, 14455.62, 0.02);
            Assert.AreEqual(_resultados[3].CustoInicialConciliacao, -1980304.83, 0.02);
            Assert.AreEqual(_resultados[3].CustoFinalConciliacao, -1906940.75, 0.02);
            Assert.AreEqual(_resultados[3].ResultadoConciliacao, 14579.66, 0.02);
            Assert.AreEqual(_resultados[3].DiasAtraso, 0);

            Assert.AreEqual(_resultados[4].Data.ToString("dd/MM/yyyy"), "29/02/2020");
            Assert.AreEqual(_resultados[4].ValorMovimento, 0.00);
            Assert.AreEqual(_resultados[4].SaldoInicial, 1927460.95, 0.02);
            Assert.AreEqual(_resultados[4].SaldoFinal, 1927460.95, 0.02);
            Assert.AreEqual(_resultados[4].CustoInicial, -1909528.12, 0.02);
            Assert.AreEqual(_resultados[4].CustoFinal, -1909528.12, 0.02);
            Assert.AreEqual(_resultados[4].ResultadoGerencial, 17932.83, 0.02);
            Assert.AreEqual(_resultados[4].CustoInicialConciliacao, -1909071.27, 0.02);
            Assert.AreEqual(_resultados[4].CustoFinalConciliacao, -1909071.27, 0.02);
            Assert.AreEqual(_resultados[4].ResultadoConciliacao, 18389.68, 0.02);
            Assert.AreEqual(_resultados[4].DiasAtraso, 0);

            Assert.AreEqual(_resultados[5].Data.ToString("dd/MM/yyyy"), "31/03/2020");
            //Assert.AreEqual(_resultados[5].ValorMovimento, 0.00);
            //Assert.AreEqual(_resultados[5].SaldoInicial, 1938925.58, 0.02);
            //Assert.AreEqual(_resultados[5].SaldoFinal, 1938925.58, 0.02);
            //Assert.AreEqual(_resultados[5].CustoInicial, -1911994.64, 0.02);
            //Assert.AreEqual(_resultados[5].CustoFinal, -1911994.64, 0.02);
            //Assert.AreEqual(_resultados[5].ResultadoGerencial, 26930.94, 0.02);
            //Assert.AreEqual(_resultados[5].CustoInicialConciliacao, -1909071.27, 0.02);
            //Assert.AreEqual(_resultados[5].CustoFinalConciliacao, -1909071.27, 0.02);
            //Assert.AreEqual(_resultados[5].ResultadoConciliacao, 29854.30, 0.02);
            //Assert.AreEqual(_resultados[5].DiasAtraso, 0);
            //Assert.AreEqual(_resultados[5].ValorMovimento, 0.00);
            //Assert.AreEqual(_resultados[5].SaldoInicial, 1938925.58, 0.02);
            //Assert.AreEqual(_resultados[5].SaldoFinal, 1938925.58, 0.02);
            //Assert.AreEqual(_resultados[5].CustoInicial, -1911994.64, 0.02);
            //Assert.AreEqual(_resultados[5].CustoFinal, -1911994.64, 0.02);
            //Assert.AreEqual(_resultados[5].ResultadoGerencial, 26930.94, 0.02);
            //Assert.AreEqual(_resultados[5].CustoInicialConciliacao, -1909071.27, 0.02);
            //Assert.AreEqual(_resultados[5].CustoFinalConciliacao, -1909071.27, 0.02);
            //Assert.AreEqual(_resultados[5].ResultadoConciliacao, 29854.30, 0.02);
            //Assert.AreEqual(_resultados[5].DiasAtraso, 0);

            Assert.AreEqual(_resultados[6].Data.ToString("dd/MM/yyyy"), "30/04/2020");
            //Assert.AreEqual(_resultados[6].ValorMovimento, 0.00);
            //Assert.AreEqual(_resultados[6].SaldoInicial, 1947650.74, 0.02);
            //Assert.AreEqual(_resultados[6].SaldoFinal, 1947650.74, 0.02);
            //Assert.AreEqual(_resultados[6].CustoInicial, -1911994.64, 0.02);
            //Assert.AreEqual(_resultados[6].CustoFinal, -1911994.64, 0.02);
            //Assert.AreEqual(_resultados[6].ResultadoGerencial, 35656.10, 0.02);
            //Assert.AreEqual(_resultados[6].CustoInicialConciliacao, -1909071.27, 0.02);
            //Assert.AreEqual(_resultados[6].CustoFinalConciliacao, -1909071.27, 0.02);
            //Assert.AreEqual(_resultados[6].ResultadoConciliacao, 38579.47, 0.02);
            //Assert.AreEqual(_resultados[6].DiasAtraso, 0);


            Assert.AreEqual(_resultados[7].Data.ToString("dd/MM/yyyy"), "31/05/2020");
            Assert.AreEqual(_resultados[8].Data.ToString("dd/MM/yyyy"), "30/06/2020");
            Assert.AreEqual(_resultados[9].Data.ToString("dd/MM/yyyy"), "31/07/2020");
            Assert.AreEqual(_resultados[10].Data.ToString("dd/MM/yyyy"), "31/08/2020");
            Assert.AreEqual(_resultados[11].Data.ToString("dd/MM/yyyy"), "30/09/2020");
            Assert.AreEqual(_resultados[12].Data.ToString("dd/MM/yyyy"), "31/10/2020");
            Assert.AreEqual(_resultados[13].Data.ToString("dd/MM/yyyy"), "30/11/2020");
            Assert.AreEqual(_resultados[14].Data.ToString("dd/MM/yyyy"), "31/12/2020");
            Assert.AreEqual(_resultados[15].Data.ToString("dd/MM/yyyy"), "31/01/2021");
            Assert.AreEqual(_resultados[16].Data.ToString("dd/MM/yyyy"), "28/02/2021");
            Assert.AreEqual(_resultados[17].Data.ToString("dd/MM/yyyy"), "31/03/2021");
            Assert.AreEqual(_resultados[18].Data.ToString("dd/MM/yyyy"), "30/04/2021");
            Assert.AreEqual(_resultados[19].Data.ToString("dd/MM/yyyy"), "31/05/2021");
            Assert.AreEqual(_resultados[20].Data.ToString("dd/MM/yyyy"), "30/06/2021");
            Assert.AreEqual(_resultados[21].Data.ToString("dd/MM/yyyy"), "31/07/2021");
            Assert.AreEqual(_resultados[22].Data.ToString("dd/MM/yyyy"), "31/08/2021");
            Assert.AreEqual(_resultados[23].Data.ToString("dd/MM/yyyy"), "30/09/2021");
            Assert.AreEqual(_resultados[24].Data.ToString("dd/MM/yyyy"), "31/10/2021");
            Assert.AreEqual(_resultados[25].Data.ToString("dd/MM/yyyy"), "30/11/2021");
            Assert.AreEqual(_resultados[26].Data.ToString("dd/MM/yyyy"), "31/12/2021");
            Assert.AreEqual(_resultados[27].Data.ToString("dd/MM/yyyy"), "31/01/2022");
            Assert.AreEqual(_resultados[28].Data.ToString("dd/MM/yyyy"), "28/02/2022");
            Assert.AreEqual(_resultados[29].Data.ToString("dd/MM/yyyy"), "31/03/2022");
            Assert.AreEqual(_resultados[30].Data.ToString("dd/MM/yyyy"), "30/04/2022");
            Assert.AreEqual(_resultados[31].Data.ToString("dd/MM/yyyy"), "31/05/2022");
            Assert.AreEqual(_resultados[32].Data.ToString("dd/MM/yyyy"), "30/06/2022");
            Assert.AreEqual(_resultados[33].Data.ToString("dd/MM/yyyy"), "31/07/2022");
            Assert.AreEqual(_resultados[34].Data.ToString("dd/MM/yyyy"), "31/08/2022");
            Assert.AreEqual(_resultados[35].Data.ToString("dd/MM/yyyy"), "30/09/2022");
            Assert.AreEqual(_resultados[36].Data.ToString("dd/MM/yyyy"), "31/10/2022");
            Assert.AreEqual(_resultados[37].Data.ToString("dd/MM/yyyy"), "30/11/2022");
            Assert.AreEqual(_resultados[38].Data.ToString("dd/MM/yyyy"), "16/12/2022");
            #endregion
        }
    }
}
