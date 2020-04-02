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
    public class ResultadoContratoCdiTests
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
        }

        [TestMethod]
        public void CriarResultadosDeContratoCdiSemMovimentacao()
        {
            var Taxa = 0.0030;
            var Indice = IndiceContratoEnum.CDI;
            var DataInicial = DateTime.Parse("2019-12-23");
            var DataFinal = DateTime.Parse("2022-12-23");
            var Valor = 7142299.66;
            Contrato contrato = new Contrato(Valor, Taxa, Indice, DataInicial, DataFinal);
            //ASSERT
            _mockRepository = new Mock<ICotacaoRepository>();
            _contratoApplication = new ContratoApplication(_mockRepository.Object);
            _mockRepository
                .Setup(x => x.GetByPeriodo(contrato.Inicio, contrato.Fim))
                .Returns(CotacaoFaker.GetAll().Where(x => x.Data >= contrato.Inicio && x.Data <= contrato.Fim).AsQueryable());

            _contratoApplication.Calcular(contrato);
            #region Asserts
            Assert.AreEqual(contrato.Resultados[0].Data.ToString("yyyy-MM-dd"), "2019-12-31");
            Assert.AreEqual(contrato.Resultados[1].Data.ToString("yyyy-MM-dd"), "2020-01-31");
            Assert.AreEqual(contrato.Resultados[2].Data.ToString("yyyy-MM-dd"), "2020-02-29");
            Assert.AreEqual(contrato.Resultados[3].Data.ToString("yyyy-MM-dd"), "2020-03-31");
            Assert.AreEqual(contrato.Resultados[4].Data.ToString("yyyy-MM-dd"), "2020-04-30");
            Assert.AreEqual(contrato.Resultados[5].Data.ToString("yyyy-MM-dd"), "2020-05-31");
            Assert.AreEqual(contrato.Resultados[6].Data.ToString("yyyy-MM-dd"), "2020-06-30");
            Assert.AreEqual(contrato.Resultados[7].Data.ToString("yyyy-MM-dd"), "2020-07-31");
            Assert.AreEqual(contrato.Resultados[8].Data.ToString("yyyy-MM-dd"), "2020-08-31");


            #endregion
        }

        [TestMethod]
        public void CriarResultadosDeContratoCdiComMovimentacao()
        {
            var Taxa = 0.0045;
            var Indice = IndiceContratoEnum.CDI;
            var DataInicial = DateTime.Parse("2019-12-16");
            var DataFinal = DateTime.Parse("2022-12-16");
            var Valor = 2042610.95;
            //ASSERT
            Contrato contrato = new Contrato(Valor, Taxa, Indice, DataInicial, DataFinal);
            contrato.Adicionar(new Movimento() { Data = DateTime.Parse("2020-01-16"), Valor = -73324.55 });
            contrato.Adicionar(new Movimento() { Data = DateTime.Parse("2020-02-17"), Valor = -73364.08 });
            //ASSERT
            _mockRepository = new Mock<ICotacaoRepository>();
            _contratoApplication = new ContratoApplication(_mockRepository.Object);
            _mockRepository
                .Setup(x => x.GetByPeriodo(contrato.Inicio, contrato.Fim))
                .Returns(CotacaoFaker.GetAll().Where(x => x.Data >= contrato.Inicio && x.Data <= contrato.Fim).AsQueryable());

            _contratoApplication.Calcular(contrato);

            #region Asserts

            Assert.AreEqual(contrato.Resultados[0].Data.ToString("dd/MM/yyyy"), "31/12/2019");
            Assert.AreEqual(contrato.Resultados[1].Data.ToString("dd/MM/yyyy"), "16/01/2020");
            Assert.AreEqual(contrato.Resultados[2].Data.ToString("dd/MM/yyyy"), "31/01/2020");
            Assert.AreEqual(contrato.Resultados[3].Data.ToString("dd/MM/yyyy"), "17/02/2020");
            Assert.AreEqual(contrato.Resultados[4].Data.ToString("dd/MM/yyyy"), "29/02/2020");
            Assert.AreEqual(contrato.Resultados[5].Data.ToString("dd/MM/yyyy"), "31/03/2020");
            Assert.AreEqual(contrato.Resultados[6].Data.ToString("dd/MM/yyyy"), "30/04/2020");
            Assert.AreEqual(contrato.Resultados[7].Data.ToString("dd/MM/yyyy"), "31/05/2020");
            Assert.AreEqual(contrato.Resultados[8].Data.ToString("dd/MM/yyyy"), "30/06/2020");
            Assert.AreEqual(contrato.Resultados[9].Data.ToString("dd/MM/yyyy"), "31/07/2020");
            Assert.AreEqual(contrato.Resultados[10].Data.ToString("dd/MM/yyyy"), "31/08/2020");
            Assert.AreEqual(contrato.Resultados[11].Data.ToString("dd/MM/yyyy"), "30/09/2020");
            Assert.AreEqual(contrato.Resultados[12].Data.ToString("dd/MM/yyyy"), "31/10/2020");
            Assert.AreEqual(contrato.Resultados[13].Data.ToString("dd/MM/yyyy"), "30/11/2020");
            Assert.AreEqual(contrato.Resultados[14].Data.ToString("dd/MM/yyyy"), "31/12/2020");
            Assert.AreEqual(contrato.Resultados[15].Data.ToString("dd/MM/yyyy"), "31/01/2021");
            Assert.AreEqual(contrato.Resultados[16].Data.ToString("dd/MM/yyyy"), "28/02/2021");
            Assert.AreEqual(contrato.Resultados[17].Data.ToString("dd/MM/yyyy"), "31/03/2021");
            Assert.AreEqual(contrato.Resultados[18].Data.ToString("dd/MM/yyyy"), "30/04/2021");
            Assert.AreEqual(contrato.Resultados[19].Data.ToString("dd/MM/yyyy"), "31/05/2021");
            Assert.AreEqual(contrato.Resultados[20].Data.ToString("dd/MM/yyyy"), "30/06/2021");
            Assert.AreEqual(contrato.Resultados[21].Data.ToString("dd/MM/yyyy"), "31/07/2021");
            Assert.AreEqual(contrato.Resultados[22].Data.ToString("dd/MM/yyyy"), "31/08/2021");
            Assert.AreEqual(contrato.Resultados[23].Data.ToString("dd/MM/yyyy"), "30/09/2021");
            Assert.AreEqual(contrato.Resultados[24].Data.ToString("dd/MM/yyyy"), "31/10/2021");
            Assert.AreEqual(contrato.Resultados[25].Data.ToString("dd/MM/yyyy"), "30/11/2021");
            Assert.AreEqual(contrato.Resultados[26].Data.ToString("dd/MM/yyyy"), "31/12/2021");
            Assert.AreEqual(contrato.Resultados[27].Data.ToString("dd/MM/yyyy"), "31/01/2022");
            Assert.AreEqual(contrato.Resultados[28].Data.ToString("dd/MM/yyyy"), "28/02/2022");
            Assert.AreEqual(contrato.Resultados[29].Data.ToString("dd/MM/yyyy"), "31/03/2022");
            Assert.AreEqual(contrato.Resultados[30].Data.ToString("dd/MM/yyyy"), "30/04/2022");
            Assert.AreEqual(contrato.Resultados[31].Data.ToString("dd/MM/yyyy"), "31/05/2022");
            Assert.AreEqual(contrato.Resultados[32].Data.ToString("dd/MM/yyyy"), "30/06/2022");
            Assert.AreEqual(contrato.Resultados[33].Data.ToString("dd/MM/yyyy"), "31/07/2022");
            Assert.AreEqual(contrato.Resultados[34].Data.ToString("dd/MM/yyyy"), "31/08/2022");
            Assert.AreEqual(contrato.Resultados[35].Data.ToString("dd/MM/yyyy"), "30/09/2022");
            Assert.AreEqual(contrato.Resultados[36].Data.ToString("dd/MM/yyyy"), "31/10/2022");
            Assert.AreEqual(contrato.Resultados[37].Data.ToString("dd/MM/yyyy"), "30/11/2022");
            Assert.AreEqual(contrato.Resultados[38].Data.ToString("dd/MM/yyyy"), "16/12/2022");

            Assert.IsNull(contrato.Resultados[0].Movimento);
            Assert.AreEqual(contrato.Resultados[1].Movimento.Valor, -73324.55);
            Assert.IsNull(contrato.Resultados[2].Movimento);
            Assert.AreEqual(contrato.Resultados[3].Movimento.Valor, -73364.08);
            Assert.IsNull(contrato.Resultados[4].Movimento);
            Assert.IsNull(contrato.Resultados[5].Movimento);

            Assert.AreEqual(contrato.Resultados[0].SaldoInicial, 2042610.95);
            Assert.AreEqual(contrato.Resultados[0].SaldoFinal, 2042610.95);
            Assert.AreEqual(contrato.Resultados[1].SaldoInicial, 2051360.26);
            Assert.AreEqual(contrato.Resultados[1].SaldoFinal, 1978035.71);
            Assert.AreEqual(contrato.Resultados[2].SaldoInicial, 1986211.12, 0.02);
            Assert.AreEqual(contrato.Resultados[2].SaldoFinal, 1986211.12, 0.02);
            Assert.AreEqual(contrato.Resultados[3].SaldoInicial, 1994884.49);
            Assert.AreEqual(contrato.Resultados[3].SaldoFinal, 1921520.41);
            Assert.AreEqual(contrato.Resultados[4].SaldoInicial, 1927460.95, 0.02);
            Assert.AreEqual(contrato.Resultados[4].SaldoFinal, 1927460.95, 0.02);
            Assert.AreEqual(contrato.Resultados[5].SaldoInicial, 1941042.70, 0.02);
            Assert.AreEqual(contrato.Resultados[5].SaldoFinal, 1941042.70, 0.02);

            /*
            Assert.AreEqual(contrato.Resultados[0].CustoInicial, -2042610.95);
            Assert.AreEqual(contrato.Resultados[0].CustoFinal, -2042610.95);
            Assert.AreEqual(contrato.Resultados[0].ResultadoGerencial, 0.00, 0.02);
            Assert.AreEqual(contrato.Resultados[0].CustoInicialConciliacao, -2042610.95, 0.02);
            Assert.AreEqual(contrato.Resultados[0].CustoFinalConciliacao, -2042610.95, 0.02);
            Assert.AreEqual(contrato.Resultados[0].ResultadoConciliacao, 0.00, 0.02);
            Assert.AreEqual(contrato.Resultados[0].DiasAtraso, 0);

            Assert.AreEqual(contrato.Resultados[1].CustoInicial, -2046453.91);
            Assert.AreEqual(contrato.Resultados[1].CustoFinal, -1973129.36);
            Assert.AreEqual(contrato.Resultados[1].ResultadoGerencial, 4906.34, 0.02);
            Assert.AreEqual(contrato.Resultados[1].CustoInicialConciliacao, -2046411.76, 0.02);
            Assert.AreEqual(contrato.Resultados[1].CustoFinalConciliacao, -1973087.21, 0.02);
            Assert.AreEqual(contrato.Resultados[1].ResultadoConciliacao, 4948.50, 0.02);
            Assert.AreEqual(contrato.Resultados[1].DiasAtraso, 0);

            Assert.AreEqual(contrato.Resultados[2].CustoInicial, -1976841.60);
            Assert.AreEqual(contrato.Resultados[2].CustoFinal, -1976841.60);
            Assert.AreEqual(contrato.Resultados[2].ResultadoGerencial, 9369.52, 0.02);
            Assert.AreEqual(contrato.Resultados[2].CustoInicialConciliacao, -1976758.42, 0.02);
            Assert.AreEqual(contrato.Resultados[2].CustoFinalConciliacao, -1976758.42, 0.02);
            Assert.AreEqual(contrato.Resultados[2].ResultadoConciliacao, 9452.70, 0.02);
            Assert.AreEqual(contrato.Resultados[2].DiasAtraso, 0);

            Assert.AreEqual(contrato.Resultados[3].SaldoInicial, 1994884.49);
            Assert.AreEqual(contrato.Resultados[3].SaldoFinal, 1921520.41);
            Assert.AreEqual(contrato.Resultados[3].CustoInicial, -1980428.87);
            Assert.AreEqual(contrato.Resultados[3].CustoFinal, -1907064.79);
            Assert.AreEqual(contrato.Resultados[3].ResultadoGerencial, 14455.62, 0.02);
            Assert.AreEqual(contrato.Resultados[3].CustoInicialConciliacao, -1980304.83, 0.02);
            Assert.AreEqual(contrato.Resultados[3].CustoFinalConciliacao, -1906940.75, 0.02);
            Assert.AreEqual(contrato.Resultados[3].ResultadoConciliacao, 14579.66, 0.02);
            Assert.AreEqual(contrato.Resultados[3].DiasAtraso, 0);


            Assert.IsNull(contrato.Resultados[4].Movimento);
            Assert.AreEqual(contrato.Resultados[4].SaldoInicial, 1927460.95, 0.02);
            Assert.AreEqual(contrato.Resultados[4].SaldoFinal, 1927460.95, 0.02);
            Assert.AreEqual(contrato.Resultados[4].CustoInicial, -1909528.12, 0.02);
            Assert.AreEqual(contrato.Resultados[4].CustoFinal, -1909528.12, 0.02);
            Assert.AreEqual(contrato.Resultados[4].ResultadoGerencial, 17932.83, 0.02);
            Assert.AreEqual(contrato.Resultados[4].CustoInicialConciliacao, -1909071.27, 0.02);
            Assert.AreEqual(contrato.Resultados[4].CustoFinalConciliacao, -1909071.27, 0.02);
            Assert.AreEqual(contrato.Resultados[4].ResultadoConciliacao, 18389.68, 0.02);
            Assert.AreEqual(contrato.Resultados[4].DiasAtraso, 0);

            Assert.IsNull(contrato.Resultados[5].Movimento);
            Assert.AreEqual(contrato.Resultados[5].SaldoInicial, 1938925.58, 0.02);
            Assert.AreEqual(contrato.Resultados[5].SaldoFinal, 1938925.58, 0.02);
            Assert.AreEqual(contrato.Resultados[5].CustoInicial, -1911994.64, 0.02);
            Assert.AreEqual(contrato.Resultados[5].CustoFinal, -1911994.64, 0.02);
            Assert.AreEqual(contrato.Resultados[5].ResultadoGerencial, 26930.94, 0.02);
            Assert.AreEqual(contrato.Resultados[5].CustoInicialConciliacao, -1909071.27, 0.02);
            Assert.AreEqual(contrato.Resultados[5].CustoFinalConciliacao, -1909071.27, 0.02);
            Assert.AreEqual(contrato.Resultados[5].ResultadoConciliacao, 29854.30, 0.02);
            Assert.AreEqual(contrato.Resultados[5].DiasAtraso, 0);
            
            Assert.IsNull(contrato.Resultados[6].Movimento);
            Assert.AreEqual(contrato.Resultados[6].SaldoInicial, 1947650.74, 0.02);
            Assert.AreEqual(contrato.Resultados[6].SaldoFinal, 1947650.74, 0.02);
            Assert.AreEqual(contrato.Resultados[6].CustoInicial, -1911994.64, 0.02);
            Assert.AreEqual(contrato.Resultados[6].CustoFinal, -1911994.64, 0.02);
            Assert.AreEqual(contrato.Resultados[6].ResultadoGerencial, 35656.10, 0.02);
            Assert.AreEqual(contrato.Resultados[6].CustoInicialConciliacao, -1909071.27, 0.02);
            Assert.AreEqual(contrato.Resultados[6].CustoFinalConciliacao, -1909071.27, 0.02);
            Assert.AreEqual(contrato.Resultados[6].ResultadoConciliacao, 38579.47, 0.02);
            Assert.AreEqual(contrato.Resultados[6].DiasAtraso, 0);
            */
            #endregion
        }
    }
}
