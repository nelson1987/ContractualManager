using BGB.Gerencial.Application.Services;
using BGB.Gerencial.Application.Tests.Fakers;
using BGB.Gerencial.Data.Interfaces;
using BGB.Gerencial.Domain.Entities;
using BGB.Gerencial.Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace BGB.Gerencial.UnitTests
{
    [TestClass]
    public class ContratoPreTests
    {
        private ContratoApplication _contratoApplication { get; set; }
        private Mock<ICotacaoRepository> _mockRepository { get; set; }
        [TestMethod]
        public void DeveRetornarSaldoInicialOMesmoValorDoContrato()
        {
            var valorContrato = 1000.00;
            var taxaContratual = 0.0050;
            var indiceContrato = IndiceContratoEnum.PRE;
            var dataInicio = DateTime.Parse("2020-01-01");
            var dataFim = DateTime.Parse("2020-01-02");
            Contrato contrato = new Contrato(valorContrato, taxaContratual, indiceContrato, dataInicio, dataFim);

            _mockRepository = new Mock<ICotacaoRepository>();
            _contratoApplication = new ContratoApplication(_mockRepository.Object);
            _mockRepository
                .Setup(x => x.GetByPeriodo(contrato.Inicio, contrato.Fim))
                .Returns(CotacaoFaker.GetAll().Where(x => x.Data >= contrato.Inicio && x.Data <= contrato.Fim).AsQueryable());

            _contratoApplication.Calcular(contrato);

            Assert.AreEqual(contrato.Resultados[0].Data, DateTime.Parse("2020-01-01"));
            Assert.AreEqual(contrato.Resultados[0].SaldoInicial, valorContrato);
            Assert.AreEqual(contrato.Resultados[0].CustoInicial, valorContrato * -1);
            Assert.AreEqual(contrato.Resultados[0].SaldoInicial, contrato.Valor);
            Assert.AreEqual(contrato.Resultados[0].CustoInicial, contrato.Valor * -1);
        }
    }
}