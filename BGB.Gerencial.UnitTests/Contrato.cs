using BGB.Gerencial.Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using EntidadesDeDominio = BGB.Gerencial.Domain.Entities;

namespace BGB.Gerencial.UnitTests
{
    [TestClass]
    public class Contrato
    {
        // Mark that this is a unit test method. (Required)
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DeveValidarContrato()
        {
            EntidadesDeDominio.Contrato contrato =
                new EntidadesDeDominio.Contrato(0.00, 0.00, IndiceContratoEnum.CDI, DateTime.MinValue, DateTime.MinValue);
        }

        // Mark that this is a unit test method. (Required)
        [TestMethod]
        public void DeveRetornarValoresIniciaisDeContrato()
        {
            var valorContrato = 1000.00;
            var taxaContratual = 0.0050;
            var indiceContrato = IndiceContratoEnum.PRE;
            var dataInicio = DateTime.Parse("2020-01-01");
            var dataFim = DateTime.Parse("2020-01-02");
            EntidadesDeDominio.Contrato contrato =
                new EntidadesDeDominio.Contrato(valorContrato, taxaContratual, indiceContrato, dataInicio, dataFim);
            Assert.AreEqual(contrato.Valor, valorContrato);
            Assert.AreEqual(contrato.Taxa, taxaContratual);
            Assert.AreEqual(contrato.Indice, indiceContrato);
            Assert.AreEqual(contrato.Inicio, dataInicio);
            Assert.AreEqual(contrato.Fim, dataFim);
        }

        // Mark that this is a unit test method. (Required)
        [TestMethod]
        public void DeveAdicionarMovimento()
        {
            var valorContrato = 1000.00;
            var taxaContratual = 0.0050;
            var indiceContrato = IndiceContratoEnum.PRE;
            var dataInicio = DateTime.Parse("2020-01-01");
            var dataFim = DateTime.Parse("2020-01-31");
            EntidadesDeDominio.Contrato contrato =
                new EntidadesDeDominio.Contrato(valorContrato, taxaContratual, indiceContrato, dataInicio, dataFim);
            Assert.AreEqual(contrato.Movimentos.Count, 0);
            Assert.AreEqual(contrato.Resultados.Count, 0);

            contrato.Adicionar(new Domain.ValueObjects.Movimento() { Valor = 100.00, Data = DateTime.Parse("2020-01-10") });
            Assert.AreEqual(contrato.Movimentos.Count, 1);
            Assert.AreEqual(contrato.Resultados.Count, 0);
        }

    }
}
