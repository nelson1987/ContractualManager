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
    public class ResultadoContratoPreTests
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
        public void CriarResultadosDeContratoPreSemMovimentacao()
        {
            var Taxa = 0.0053;
            var Indice = IndiceContratoEnum.PRE;
            var DataInicial = DateTime.Parse("2019-09-16");
            var DataFinal = DateTime.Parse("2020-03-13");
            var Valor = 1652537.89;

            Contrato contrato = new Contrato(Valor, Taxa, Indice, DataInicial, DataFinal, "0003444512");
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
            Assert.AreEqual(contrato.Resultados[3].Data.ToString("yyyy-MM-dd"), "2020-03-13");

            Assert.IsNull(contrato.Resultados[0].Movimento);
            Assert.IsNull(contrato.Resultados[1].Movimento);
            Assert.IsNull(contrato.Resultados[2].Movimento);
            Assert.IsNull(contrato.Resultados[3].Movimento);

            Assert.AreEqual(contrato.Resultados[0].SaldoInicial, 1652537.89);
            Assert.AreEqual(contrato.Resultados[0].SaldoFinal, 1652537.89);
            Assert.AreEqual(contrato.Resultados[1].SaldoInicial, 1661589.09);
            Assert.AreEqual(contrato.Resultados[1].SaldoFinal, 1661589.09);
            Assert.AreEqual(contrato.Resultados[2].SaldoInicial, 1670101.21);
            Assert.AreEqual(contrato.Resultados[2].SaldoFinal, 1670101.21);
            Assert.AreEqual(contrato.Resultados[3].SaldoInicial, 1673931.13);
            Assert.AreEqual(contrato.Resultados[3].SaldoFinal, 1673931.13);

            #endregion
        }

        [TestMethod]
        public void CriarResultadosDeContratoPreComMovimentacao()
        {
            var Taxa = 0.0054;
            var Indice = IndiceContratoEnum.PRE;
            var DataInicial = DateTime.Parse("2019-08-26");
            var DataFinal = DateTime.Parse("2022-02-21");
            var Valor = 2359649.57;
            //ASSERT
            Contrato contrato = new Contrato(Valor, Taxa, Indice, DataInicial, DataFinal, "0003450506");
            contrato.Adicionar(new Movimento() { Data = DateTime.Parse("2020-02-21"), Valor = -2381944.58 });
            //ASSERT
            _mockRepository = new Mock<ICotacaoRepository>();
            _contratoApplication = new ContratoApplication(_mockRepository.Object);
            _mockRepository
                .Setup(x => x.GetByPeriodo(contrato.Inicio, contrato.Fim))
                .Returns(CotacaoFaker.GetAll().Where(x => x.Data >= contrato.Inicio && x.Data <= contrato.Fim).AsQueryable());

            _contratoApplication.Calcular(contrato);

            Assert.AreEqual(contrato.Resultados[0].Data.ToString("yyyy-MM-dd"), "2019-12-31");
            Assert.IsNull(contrato.Resultados[0].Movimento);
            Assert.AreEqual(contrato.Resultados[0].SaldoInicial, 2359649.57);
            Assert.AreEqual(contrato.Resultados[0].SaldoFinal, 2359649.57);
            Assert.AreEqual(contrato.Resultados[0].CustoInicial, -2359649.57);
            Assert.AreEqual(contrato.Resultados[0].CustoFinal, -2359649.57);
            Assert.AreEqual(contrato.Resultados[0].ResultadoGerencial, 0.00);
            Assert.AreEqual(contrato.Resultados[0].CustoInicialConciliacao, -2359649.57);
            Assert.AreEqual(contrato.Resultados[0].CustoFinalConciliacao, -2359649.57);
            Assert.AreEqual(contrato.Resultados[0].ResultadoConciliacao, 0.00);

            Assert.AreEqual(contrato.Resultados[1].Data.ToString("yyyy-MM-dd"), "2020-01-31");
            Assert.IsNull(contrato.Resultados[1].Movimento);
            Assert.AreEqual(contrato.Resultados[1].SaldoInicial, 2372817.60);
            Assert.AreEqual(contrato.Resultados[1].SaldoFinal, 2372817.60);
            Assert.AreEqual(contrato.Resultados[1].CustoInicial, -2368536.80);
            Assert.AreEqual(contrato.Resultados[1].CustoFinal, -2368536.80);
            Assert.AreEqual(contrato.Resultados[1].ResultadoGerencial, 4280.80);
            Assert.AreEqual(contrato.Resultados[1].CustoInicialConciliacao, -2368438.95, 0.02);
            Assert.AreEqual(contrato.Resultados[1].CustoFinalConciliacao, -2368438.95, 0.02);
            Assert.AreEqual(contrato.Resultados[1].ResultadoConciliacao, 4378.65, 0.02);

            Assert.AreEqual(contrato.Resultados[2].Data.ToString("yyyy-MM-dd"), "2020-02-21");
            Assert.AreEqual(contrato.Resultados[2].Movimento.Valor, -2381944.58);

            //Assert.AreEqual(contrato.Resultados[2].SaldoInicial, 2381944.58); //TODO: VALIDAR COM AREA RESPONSAVEL
            //Assert.AreEqual(contrato.Resultados[2].SaldoFinal, 0.00); //TODO: VALIDAR COM AREA RESPONSAVEL - DEU -164,98
            //Assert.AreEqual(contrato.Resultados[2].CustoInicial, -2374366.84);
            //Assert.AreEqual(contrato.Resultados[2].CustoFinal, 7577.74);
            //Assert.AreEqual(contrato.Resultados[2].ResultadoGerencial, 7577.74);
            //Assert.AreEqual(contrato.Resultados[2].CustoInicialConciliacao, -2374202.42);
            //Assert.AreEqual(contrato.Resultados[2].CustoFinalConciliacao, 7742.16);
            //Assert.AreEqual(contrato.Resultados[2].ResultadoConciliacao, 7742.16);


            //Assert.AreEqual(contrato.Resultados[3].Data.ToString("yyyy-MM-dd"), "2020-02-29"); 
            //Assert.IsNull(contrato.Resultados[3].Movimento); 
            //Assert.AreEqual(contrato.Resultados[3].SaldoInicial, 0.00); 
            /////Assert.AreEqual(contrato.Resultados[0].CustoInicial, 0.00); 
            /////Assert.AreEqual(contrato.Resultados[0].CustoFinal, 7582.63); Assert.AreEqual(contrato.Resultados[0].CustoFinal, 7582.63); Assert.AreEqual(contrato.Resultados[0].ResultadoGerencial, 7582.63); Assert.AreEqual(contrato.Resultados[0].CustoInicialConciliacao, 7745.86); Assert.AreEqual(contrato.Resultados[0].CustoFinalConciliacao, 7745.86); Assert.AreEqual(contrato.Resultados[0].ResultadoConciliacao, 7745.86); 	

            //);

        }
    }
}
