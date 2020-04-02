using BGB.Gerencial.Domain.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BGB.Gerencial.Domain.Tests.Extensions
{
    [TestClass]
    public class LastDayInMonthTests
    {

        [TestMethod]
        [DataRow("2020-01-01", "2020-01-31")]
        [DataRow("2020-02-05", "2020-02-29")]
        [DataRow("2019-02-05", "2019-02-28")]
        [DataRow("2020-03-29", "2020-03-31")]
        public void EscreverTaxaContratual(string data, string ultimoDiaMes)
        {
            DateTime dataBusca = DateTime.Parse(data).LastDayInMonth();
            DateTime dataEsperada = DateTime.Parse(ultimoDiaMes).LastDayInMonth();

            Assert.AreEqual(dataBusca, dataEsperada);
        }
    }
}
