using System;
using System.Collections.Generic;
using System.Linq;
using BGB.Gerencial.Domain.Entities;
using BGB.Gerencial.Domain.Enums;
using BGB.Gerencial.Domain.Extensions;
using BGB.Gerencial.Domain.ValueObjects;

namespace BGB.Gerencial.Domain.Services
{
    public class CalculadoraContrato
    {
        private Contrato Contrato { get; set; }
        public CalculadoraContrato(Contrato contrato)
        {
            Contrato = contrato;
        }
        public void Calcular()
        {
            List<Resultado> resultados = new List<Resultado>();

            Contrato.Adicionar(resultados);
        }
        public List<DateTime> DatasPorLinha
        {
            get
            {
                var datas = new List<DateTime>();

                for (var diaAtual = Contrato.Inicio; diaAtual.Date <= Contrato.Fim; diaAtual = diaAtual.AddDays(1))
                {
                    if (datas.Count == 0)
                        datas.Add(diaAtual);

                    if (diaAtual == diaAtual.LastDayInMonth() && diaAtual.Date != Contrato.Fim)
                        datas.Add(diaAtual);

                    if (Contrato.Movimentos.Any(x => x.Data == diaAtual))
                        datas.Add(diaAtual);

                    if (diaAtual.Date == Contrato.Fim)
                    {
                        datas.Add(diaAtual);
                        //TODO:última data ser o último dia do mês -- datas.Add(diaAtual.LastDayInMonth());
                    }
                }

                if (Contrato.Inicio.Year < DateTime.Today.Year)
                {
                    if (datas.Any(x => x < new DateTime(DateTime.Today.Year - 1, 12, 31)))
                        datas = datas.Where(x => x >= new DateTime(DateTime.Today.Year - 1, 12, 31)).ToList();
                }
                return datas;
            }
        }
        //public List<Cotacao> Cotacoes { get; set; }

        //public CalculadoraContrato(List<Cotacao> cotacoes)
        //{
        //    Cotacoes = cotacoes;
        //}

        //public List<Resultado> Calcular(List<DateTime> datasPorLinha, Contrato contrato)
        //{
        //    var resultados = new List<Resultado>();
        //    foreach (DateTime dataAtual in datasPorLinha)
        //    {
        //        //if (dataAtual >= DateTime.Today)
        //        //    resultados.Add(resultados.LastOrDefault());

        //        var cotacaoCdiDiaria = Cotacoes.FirstOrDefault(x => x.Tipo == "CDI" && x.Data == dataAtual);
        //        if (cotacaoCdiDiaria == null)
        //            cotacaoCdiDiaria = Cotacoes.FirstOrDefault(x => x.Tipo == "CDI" && x.Data == new DateTime(2020, 03, 12));

        //        var cotacaoTmcDiaria = Cotacoes.FirstOrDefault(x => x.Tipo == "TMC" && x.Data == dataAtual && x.Data <= new DateTime(2020, 03, 12));
        //        if (cotacaoTmcDiaria == null)
        //            cotacaoTmcDiaria = Cotacoes.FirstOrDefault(x => x.Tipo == "TMC" && x.Data == new DateTime(2020, 03, 12));
        //        if (cotacaoTmcDiaria == null || cotacaoCdiDiaria == null)
        //            throw new Exception($"Falta cotações do dia {dataAtual.ToString("dd/MM/yyyy")}.");
        //        //TODO: descomentar .LastOrDefault();
        //        //Linha Inicial
        //        if (resultados.Count == 0)
        //        {
        //            resultados.Add(contrato.Indice == IndiceContratoEnum.PRE
        //                ? new ResultadoPre(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria) as Resultado
        //                : new ResultadoCdi(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria) as Resultado);
        //        }
        //        //Linha Sem Movimento
        //        if (!contrato.Movimentos.Any(x => x.Data == dataAtual) && !resultados.Any(x => x.Data == dataAtual))
        //        {
        //            resultados.Add(contrato.Indice == IndiceContratoEnum.PRE
        //                ? new ResultadoPre(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria, resultados.LastOrDefault()) as Resultado
        //                : new ResultadoCdi(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria, resultados.LastOrDefault()) as Resultado);
        //        }
        //        //Linha Com Movimento
        //        if (contrato.Movimentos.Any(x => x.Data == dataAtual))
        //        {
        //            foreach (Movimento item in contrato.Movimentos.Where(x => x.Data == dataAtual))
        //            {
        //                resultados.Add(contrato.Indice == IndiceContratoEnum.PRE
        //                    ? new ResultadoPre(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria, resultados.LastOrDefault(), item) as Resultado
        //                    : new ResultadoCdi(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria, resultados.LastOrDefault(), item) as Resultado);
        //            }
        //        }
        //    }
        //    if (contrato.DataInicial.Year < DateTime.Today.Year)
        //    {
        //        resultados[0].Data = new DateTime(DateTime.Today.Year - 1, 12, 31);
        //    }
        //    return resultados;
        //}
    }
}
