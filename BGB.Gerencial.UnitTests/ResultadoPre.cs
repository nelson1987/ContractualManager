using BGB.Gerencial.Domain.Enums;
using BGB.Gerencial.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using EntidadesDeDominio = BGB.Gerencial.Domain.Entities;
using ObjetoDeValorDeDominio = BGB.Gerencial.Domain.ValueObjects;

namespace BGB.Gerencial.UnitTests
{
    [TestClass]
    public class ResultadoPre
    {
        [TestMethod]
        public void DeveRetornarSaldoInicialOMesmoValorDoContrato()
        {
            var valorContrato = 1000.00;
            var taxaContratual = 0.0050;
            var indiceContrato = IndiceContratoEnum.PRE;
            var dataInicio = DateTime.Parse("2020-01-01");
            var dataFim = DateTime.Parse("2020-01-02");
            EntidadesDeDominio.Contrato contrato =
                new EntidadesDeDominio.Contrato(valorContrato, taxaContratual, indiceContrato, dataInicio, dataFim);

            DateTime data = DateTime.Parse("2020-01-01");
            Cotacao cotacaoCdi = new Cotacao();
            Cotacao cotacaoTmc = new Cotacao();
            ObjetoDeValorDeDominio.ResultadoPre resultado = new ObjetoDeValorDeDominio.ResultadoPre(data, contrato, cotacaoCdi, cotacaoTmc);
            Assert.AreEqual(resultado.Data, data);
            Assert.AreEqual(resultado.SaldoInicial, valorContrato);
            Assert.AreEqual(resultado.CustoInicial, valorContrato * -1);
            Assert.AreEqual(resultado.SaldoInicial, contrato.Valor);
            Assert.AreEqual(resultado.CustoInicial, contrato.Valor * -1);
        }
    }
}