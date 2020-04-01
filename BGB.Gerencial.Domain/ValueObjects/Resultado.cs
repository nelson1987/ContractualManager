using BGB.Gerencial.Domain.Entities;
using BGB.Gerencial.Domain.Enums;
using System;

namespace BGB.Gerencial.Domain.ValueObjects
{
    public abstract class Resultado
    {
        public Resultado(DateTime data, Contrato contrato, Cotacao cotacaoCdi, Cotacao cotacaoTmc)
        {
            Data = data;
            CotacaoCdi = cotacaoCdi;
            CotacaoTmc = cotacaoTmc;
            Contrato = contrato;

            SaldoInicial = Contrato.Valor;
            CustoInicial = Contrato.Valor * -1;
            CustoInicialConciliacao = contrato.Valor * -1;
            ResultadoConciliacao = 0.00;

        }

        public Resultado(DateTime data, Contrato contrato, Cotacao cotacaoCdi, Cotacao cotacaoTmc, Resultado resultadoAnterior) : this(data, contrato, cotacaoCdi, cotacaoTmc)
        {
            ResultadoAnterior = resultadoAnterior;
            SetSaldoInicial();
            SetCustoInicial();
            SetCustoInicialConciliacao();
            SetResultadoConciliacao();
        }

        public Resultado(DateTime data, Contrato contrato, Cotacao cotacaoCdi, Cotacao cotacaoTmc, Resultado resultadoAnterior, Movimento movimento) : this(data, contrato, cotacaoCdi, cotacaoTmc, resultadoAnterior)
        {
            Movimento = movimento;
            SetSaldoInicial();
            SetCustoInicial();
            SetCustoInicialConciliacao();
            SetResultadoConciliacao();
        }

        public Contrato Contrato { get; set; }
        public Cotacao CotacaoCdi { get; set; }
        public Cotacao CotacaoTmc { get; set; }
        public Resultado ResultadoAnterior { get; set; }
        public Movimento Movimento { get; set; }
        public DateTime Data { get; set; }
        public int DiasAtraso { get; set; }
        public double SaldoInicial { get; set; }
        public double SaldoFinal
        {
            get
            {
                return SaldoInicial + (Movimento == null ? 0 : Movimento.Valor);
            }
        }
        public double CustoInicial { get; set; }
        public double CustoFinal
        {
            get
            {
                return Math.Round(CustoInicial - (Movimento == null ? 0 : Movimento.Valor), 2);
            }
        }
        public double ResultadoGerencial
        {
            get
            {
                return Math.Round(SaldoFinal + CustoFinal, 2);
            }
        }
        public double CustoInicialConciliacao { get; set; }
        public double CustoFinalConciliacao
        {
            get
            {
                return CustoInicialConciliacao - (Movimento == null ? 0.00 : Movimento.Valor);
            }
        }
        public double ResultadoConciliacao { get; set; }

        public abstract void SetCustoInicial();
        public abstract void SetCustoInicialConciliacao();
        public abstract double CalcularSaldoInicialComAtraso();
        public abstract double CalcularSaldoInicialSemAtraso();

        public void SetSaldoInicial()
        {
            if (ResultadoAnterior == null)
                SaldoInicial = Contrato.Valor;
            else if ((ResultadoAnterior.DiasAtraso >= 60 || Data > DateTime.Today) && Contrato.Indice != IndiceContratoEnum.CDI)
                SaldoInicial = ResultadoAnterior.SaldoFinal;
            else
                SaldoInicial = Math.Round(DiasAtraso >= 60 ? CalcularSaldoInicialComAtraso() : CalcularSaldoInicialSemAtraso(), 2);
        }

        public void SetResultadoConciliacao()
        {
            ResultadoConciliacao = Math.Round(SaldoFinal + CustoFinalConciliacao, 2);
        }
    }
}
