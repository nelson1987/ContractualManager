using BGB.Gerencial.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BGB.Gerencial.Domain.Tests.Services
{
    [TestClass]
    public class ListagemResultadosCustoInicialTests
    {
        public List<Cotacao> Cotacoes { get; set; }
        [TestInitialize]
        public void SetupInitialize()
        {
            Cotacoes = new List<Cotacao>();
            #region Cotacoes
            Cotacoes.Add(new Cotacao() { Fator = 18.1174743400, Data = DateTime.Parse("2019-12-16") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1205704300, Data = DateTime.Parse("2019-12-17") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1236670600, Data = DateTime.Parse("2019-12-18") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1267642100, Data = DateTime.Parse("2019-12-19") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1298618900, Data = DateTime.Parse("2019-12-20") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1329601100, Data = DateTime.Parse("2019-12-21") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1329601100, Data = DateTime.Parse("2019-12-22") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1329601100, Data = DateTime.Parse("2019-12-23") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1360588500, Data = DateTime.Parse("2019-12-24") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1391581200, Data = DateTime.Parse("2019-12-25") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1391581200, Data = DateTime.Parse("2019-12-26") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1422579200, Data = DateTime.Parse("2019-12-27") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1453582500, Data = DateTime.Parse("2019-12-28") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1453582500, Data = DateTime.Parse("2019-12-29") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1453582500, Data = DateTime.Parse("2019-12-30") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1484591100, Data = DateTime.Parse("2019-12-31") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1515605000, Data = DateTime.Parse("2020-01-01") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1515605000, Data = DateTime.Parse("2020-01-02") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1546624200, Data = DateTime.Parse("2020-01-03") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1577648700, Data = DateTime.Parse("2020-01-04") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1577648700, Data = DateTime.Parse("2020-01-05") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1577648700, Data = DateTime.Parse("2020-01-06") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1608678500, Data = DateTime.Parse("2020-01-07") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1639713600, Data = DateTime.Parse("2020-01-08") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1670754000, Data = DateTime.Parse("2020-01-09") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1701799800, Data = DateTime.Parse("2020-01-10") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1732850800, Data = DateTime.Parse("2020-01-11") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1732850800, Data = DateTime.Parse("2020-01-12") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1732850800, Data = DateTime.Parse("2020-01-13") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1763907100, Data = DateTime.Parse("2020-01-14") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1794968700, Data = DateTime.Parse("2020-01-15") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1826035700, Data = DateTime.Parse("2020-01-16") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1857107900, Data = DateTime.Parse("2020-01-17") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1888185500, Data = DateTime.Parse("2020-01-18") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1888185500, Data = DateTime.Parse("2020-01-19") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1888185500, Data = DateTime.Parse("2020-01-20") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1919268400, Data = DateTime.Parse("2020-01-21") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1950356500, Data = DateTime.Parse("2020-01-22") });
            Cotacoes.Add(new Cotacao() { Fator = 18.1981450000, Data = DateTime.Parse("2020-01-23") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2012548900, Data = DateTime.Parse("2020-01-24") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2043653000, Data = DateTime.Parse("2020-01-25") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2043653000, Data = DateTime.Parse("2020-01-26") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2043653000, Data = DateTime.Parse("2020-01-27") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2074762400, Data = DateTime.Parse("2020-01-28") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2105877200, Data = DateTime.Parse("2020-01-29") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2136997200, Data = DateTime.Parse("2020-01-30") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2168122600, Data = DateTime.Parse("2020-01-31") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2199253300, Data = DateTime.Parse("2020-02-01") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2199253300, Data = DateTime.Parse("2020-02-02") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2199253300, Data = DateTime.Parse("2020-02-03") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2230389400, Data = DateTime.Parse("2020-02-04") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2261530700, Data = DateTime.Parse("2020-02-05") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2292677400, Data = DateTime.Parse("2020-02-06") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2322094000, Data = DateTime.Parse("2020-02-07") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2351515300, Data = DateTime.Parse("2020-02-08") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2351515300, Data = DateTime.Parse("2020-02-09") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2351515300, Data = DateTime.Parse("2020-02-10") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2380941400, Data = DateTime.Parse("2020-02-11") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2410372200, Data = DateTime.Parse("2020-02-12") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2439807700, Data = DateTime.Parse("2020-02-13") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2469248000, Data = DateTime.Parse("2020-02-14") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2498693100, Data = DateTime.Parse("2020-02-15") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2498693100, Data = DateTime.Parse("2020-02-16") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2498693100, Data = DateTime.Parse("2020-02-17") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2528142900, Data = DateTime.Parse("2020-02-18") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2557597500, Data = DateTime.Parse("2020-02-19") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2587056800, Data = DateTime.Parse("2020-02-20") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2616520900, Data = DateTime.Parse("2020-02-21") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2645989700, Data = DateTime.Parse("2020-02-22") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2645989700, Data = DateTime.Parse("2020-02-23") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2645989700, Data = DateTime.Parse("2020-02-24") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2645989700, Data = DateTime.Parse("2020-02-25") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2645989700, Data = DateTime.Parse("2020-02-26") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2675463300, Data = DateTime.Parse("2020-02-27") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2704941600, Data = DateTime.Parse("2020-02-28") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2734424700, Data = DateTime.Parse("2020-02-29") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2734424700, Data = DateTime.Parse("2020-03-01") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2734424700, Data = DateTime.Parse("2020-03-02") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2763912600, Data = DateTime.Parse("2020-03-03") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2793405200, Data = DateTime.Parse("2020-03-04") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2822902600, Data = DateTime.Parse("2020-03-05") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2852404700, Data = DateTime.Parse("2020-03-06") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2881911600, Data = DateTime.Parse("2020-03-07") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2881911600, Data = DateTime.Parse("2020-03-08") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2881911600, Data = DateTime.Parse("2020-03-09") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2911423200, Data = DateTime.Parse("2020-03-10") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2940939700, Data = DateTime.Parse("2020-03-11") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2970460800, Data = DateTime.Parse("2020-03-12") });
            Cotacoes.Add(new Cotacao() { Fator = 18.2999986800, Data = DateTime.Parse("2020-03-13") });
            Cotacoes.Add(new Cotacao() { Fator = 18.3029517500, Data = DateTime.Parse("2020-03-14") });
            Cotacoes.Add(new Cotacao() { Fator = 18.3029517500, Data = DateTime.Parse("2020-03-15") });
            Cotacoes.Add(new Cotacao() { Fator = 18.3029517500, Data = DateTime.Parse("2020-03-16") });
            Cotacoes.Add(new Cotacao() { Fator = 18.3059053000, Data = DateTime.Parse("2020-03-17") });
            Cotacoes.Add(new Cotacao() { Fator = 18.3088593200, Data = DateTime.Parse("2020-03-18") });
            Cotacoes.Add(new Cotacao() { Fator = 18.3118138200, Data = DateTime.Parse("2020-03-19") });
            Cotacoes.Add(new Cotacao() { Fator = 18.3144190400, Data = DateTime.Parse("2020-03-20") });
            Cotacoes.Add(new Cotacao() { Fator = 18.3170246300, Data = DateTime.Parse("2020-03-21") });
            Cotacoes.Add(new Cotacao() { Fator = 18.3170246300, Data = DateTime.Parse("2020-03-22") });
            Cotacoes.Add(new Cotacao() { Fator = 18.3170246300, Data = DateTime.Parse("2020-03-23") });
            #endregion

        }

        [TestMethod]
        [DataRow("20/02/2020", "18/08/2020", "PRE", 0.0042, 1829846.61, -1831323.50)]
        [DataRow("12/02/2020", "10/08/2020", "PRE", 0.0042, 1732641.63, -1735719.67)]
        [DataRow("21/01/2020", "17/07/2020", "PRE", 0.0044, 1748913.83, -1751306.23)]
        [DataRow("23/12/2019", "19/06/2020", "PRE", 0.0045, 1811853.83, -1818677.88)]
        [DataRow("15/01/2020", "13/07/2020", "PRE", 0.0044, 3764258.40, -3771984.95)]
        public void ListarDatasPre(string dataInicial, string dataFinal, string indice, double taxa, double valorContrato, double valorEsperado)
        {
            Calculadora calculadora = new Calculadora(new Contrato()
            {
                DataInicial = DateTime.Parse(dataInicial),
                DataFinal = DateTime.Parse(dataFinal),
                Indice = indice,
                Taxa = taxa,
                Valor = valorContrato
            }, Cotacoes);
            var calculo = calculadora.Calcular();
            Assert.AreEqual(calculo[1].CustoInicial, valorEsperado);
        }

        [TestMethod]
        [DataRow("16/12/2019", "16/12/2022", "CDI", 0.0045, 2042610.95, -2046453.91)]
        public void ListarDatasCDIComMovimentacao(string dataInicial, string dataFinal, string indice, double taxa, double valorContrato, double valorEsperado)
        {
            List<Movimento> movimentos = new List<Movimento>();
            movimentos.Add(new Movimento() { Data = DateTime.Parse("2020-01-16"), Valor = -73324.55 });

            Calculadora calculadora = new Calculadora(new Contrato()
            {
                DataInicial = DateTime.Parse(dataInicial),
                DataFinal = DateTime.Parse(dataFinal),
                Indice = indice,
                Taxa = taxa,
                Valor = valorContrato,
                Movimentos = movimentos
            }, Cotacoes);
            var calculo = calculadora.Calcular();
            Assert.AreEqual(calculo[1].CustoInicial, valorEsperado);
        }

        [TestMethod]
        [DataRow("23/12/2019", "23/12/2022", "CDI", 0.0030, 7142299.66, -7169199.94)]
        public void ListarDatasCDISemMovimentacao(string dataInicial, string dataFinal, string indice, double taxa, double valorContrato, double valorEsperado)
        {
            List<Movimento> movimentos = new List<Movimento>();

            Calculadora calculadora = new Calculadora(new Contrato()
            {
                DataInicial = DateTime.Parse(dataInicial),
                DataFinal = DateTime.Parse(dataFinal),
                Indice = indice,
                Taxa = taxa,
                Valor = valorContrato,
                Movimentos = movimentos
            }, Cotacoes);
            var calculo = calculadora.Calcular();
            Assert.AreEqual(calculo[1].CustoInicial, valorEsperado);
        }

    }
}
