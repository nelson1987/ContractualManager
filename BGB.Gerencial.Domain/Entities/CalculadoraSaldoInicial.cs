using System;

namespace BGB.Gerencial.Domain.Entities
{
    public class CalculadoraSaldoInicial
    {
        private Contrato Contrato { get; set; }
        private Resultado Atual { get; set; }
        private Resultado Anterior { get; set; }
        public CalculadoraSaldoInicial(Contrato contrato, Resultado resultadoAtual, Resultado resultadoAnterior)
        {
            //IndiceContratual = contrato.Indice;
            //TaxaContratual = contrato.Taxa;
            //SaldoFinalResultadoAnterior = resultadoAnterior.SaldoFinal;
            //CdiDataInicial = resultadoAtual.FatorCdi;
            //CdiDataFinal = resultadoAnterior.FatorCdi;
            //DataResultadoAnterior = resultadoAnterior.Data;
            //DiasAtrasoResultadoAnterior = resultadoAnterior.DiasAtraso;
            //DataResultadoAtual = resultadoAtual.Data;
            //DiasAtrasoResultadoAtual = resultadoAtual.DiasAtraso;
            Contrato = contrato;
            Atual = resultadoAtual;
            Anterior = resultadoAnterior;
        }

        public double Valor
        {
            get
            {
                if (string.IsNullOrEmpty(Contrato.Indice))
                {
                    return 0;
                }
                else if (Anterior.DiasAtraso >= 60 || Atual.Data > DateTime.Today)
                {
                    return Anterior.SaldoFinal;
                }

                return Contrato.Indice == "PRE"
                    ? CalcularSaldoInicialPRE()
                    : CalcularSaldoInicialCDI();
            }
        }

        #region
        public double CalcularSaldoInicialPreComAtraso()
        {
            var taxaExponencial = Math.Pow((1 + Contrato.Taxa), 2.00);
            return Anterior.SaldoFinal * taxaExponencial;
        }
        public double CalcularSaldoInicialCdiComAtraso()
        {
            //(D16/PROCV($A16;'Mini CDI'!$A$1:$B$4018;2))*(PROCV($A16+60;'Mini CDI'!$A$1:$B$4018;2)*(1+$B$12)^(2))
            var taxaExponencial = Math.Pow((1 + Contrato.Taxa), 2.00);
            return (Anterior.SaldoFinal / Atual.FatorCdi) * (Anterior.FatorCdi * taxaExponencial);
        }
        public double CalcularSaldoInicialPreSemAtraso()
        {
            //D16*((1+$B$12)^(($A17-$A16)/30))
            var diferencaDias = Convert.ToDouble(Atual.Data.Subtract(Anterior.Data).Days);
            var dias = (diferencaDias / 30.00);
            var fatorTempo = Math.Pow((1.00 + Contrato.Taxa), dias);
            return Anterior.SaldoFinal * fatorTempo;
        }
        public double CalcularSaldoInicialCdiSemAtraso()
        {
            //D16/PROCV($A16;'Mini CDI'!$A$1:$B$4018;2)*PROCV($A17;'Mini CDI'!$A$1:$B$4018;2)*((1+$B$12)^(($A17-$A16)/30)))
            var diferencaDias = Convert.ToDouble(Atual.Data.Subtract(Anterior.Data).Days);
            var dias = diferencaDias / 30.00;
            var fatorTempo = Math.Pow((1.00 + Contrato.Taxa), dias);
            return (Anterior.SaldoFinal / Anterior.FatorCdi) * (Atual.FatorCdi * fatorTempo);
        }
        #endregion

        public double CalcularSaldoInicialCDI()
        {
            return Atual.DiasAtraso >= 60
                ? CalcularSaldoInicialCdiComAtraso()
                : CalcularSaldoInicialCdiSemAtraso();
        }

        public double CalcularSaldoInicialPRE()
        {
            return Atual.DiasAtraso >= 60
                ? CalcularSaldoInicialPreComAtraso()
                : CalcularSaldoInicialPreSemAtraso();
        }
    }
}
