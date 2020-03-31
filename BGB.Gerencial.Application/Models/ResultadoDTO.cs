using System;

namespace BGB.Gerencial.Application.Models
{
    public class ResultadoDTO
    {
        public DateTime Data { get; set; }
        public int DiasAtraso { get; set; }
        public double ValorMovimento { get; set; }
        public double ValorCdi { get; set; }
        public double ValorTmc { get; set; }
        public double SaldoInicial { get; set; }
        public double SaldoFinal { get; set; }
        public double CustoInicial { get; set; }
        public double CustoFinal { get; set; }
        public double ResultadoGerencial { get; set; }
        public double CustoInicialConciliacao { get; set; }
        public double CustoFinalConciliacao { get; set; }
        public double ResultadoConciliacao { get; set; }
    }
}
