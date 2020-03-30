using System;

namespace BGB.Gerencial.Domain.Entities
{
    public class ResultadoCdi : Resultado
    {
        public ResultadoCdi(DateTime data, Contrato contrato, Cotacao cotacaoCdi, Cotacao cotacaoTmc) : base(data, contrato, cotacaoCdi, cotacaoTmc)
        {
            SaldoInicial = Contrato.Valor;
            CustoInicial = Contrato.Valor * -1;
            CustoInicialConciliacao = contrato.Valor * -1;
            ResultadoConciliacao = 0.00;
        }

        public ResultadoCdi(DateTime data, Contrato contrato, Cotacao cotacaoCdi, Cotacao cotacaoTmc, Resultado resultado) : base(data, contrato, cotacaoCdi, cotacaoTmc, resultado)
        {
            SetSaldoInicial();
            SetCustoInicial();
            SetCustoInicialConciliacao();
            SetResultadoConciliacao();
        }

        public ResultadoCdi(DateTime data, Contrato contrato, Cotacao cotacaoCdi, Cotacao cotacaoTmc, Resultado resultado, Movimento movimento) : base(data, contrato, cotacaoCdi, cotacaoTmc, resultado, movimento)
        {
            SetSaldoInicial();
            SetCustoInicial();
            SetCustoInicialConciliacao();
            SetResultadoConciliacao();
        }

        public override void SetCustoInicialConciliacao()
        {
            //=I16/PROCV($A16;'Mini CDI - RESULTADO'!$E$1:$F$1700;2)*PROCV($A17;'Mini CDI - RESULTADO'!$E$1:$F$1700;2);
            var custo = (ResultadoAnterior.CustoFinalConciliacao / ResultadoAnterior.CotacaoTmc.Fator) * (CotacaoTmc.Fator == 0 ? 1 : CotacaoTmc.Fator);
            CustoInicialConciliacao = Math.Round(custo, 2);
        }

        public override void SetCustoInicial()
        {
            //    //=F16/PROCV($A16;'Mini CDI'!$A$1:$B$4018;2)*PROCV($A17;'Mini CDI'!$A$1:$B$4018;2)
            var custo = (ResultadoAnterior.CustoFinal / ResultadoAnterior.CotacaoCdi.Fator) * (CotacaoCdi.Fator == 0 ? 1 : CotacaoCdi.Fator);
            CustoInicial = Math.Round(custo, 2);
        }

        public override double CalcularSaldoInicialComAtraso()
        {
            //(D16/PROCV($A16;'Mini CDI'!$A$1:$B$4018;2))*(PROCV($A16+60;'Mini CDI'!$A$1:$B$4018;2)*(1+$B$12)^(2))
            var taxaExponencial = Math.Pow((1 + Contrato.Taxa), 2.00);
            return (ResultadoAnterior.SaldoFinal / CotacaoCdi.Fator) * (ResultadoAnterior.CotacaoCdi.Fator * taxaExponencial);
        }

        public override double CalcularSaldoInicialSemAtraso()
        {
            //D16/PROCV($A16;'Mini CDI'!$A$1:$B$4018;2)*PROCV($A17;'Mini CDI'!$A$1:$B$4018;2)*((1+$B$12)^(($A17-$A16)/30)))
            var diferencaDias = Convert.ToDouble(Data.Subtract(ResultadoAnterior.Data).Days);
            var dias = diferencaDias / 30.00;
            var fatorTempo = Math.Pow((1.00 + Contrato.Taxa), dias);

            if (ResultadoAnterior.CotacaoCdi == null || CotacaoCdi == null)
                throw new Exception("Faltam fatores nesse cálculo.");

            return (ResultadoAnterior.SaldoFinal / ResultadoAnterior.CotacaoCdi.Fator) * (CotacaoCdi.Fator * fatorTempo);
        }
    }

}
