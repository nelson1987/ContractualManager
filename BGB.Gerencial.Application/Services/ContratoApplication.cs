using BGB.Gerencial.Application.Models;
using BGB.Gerencial.Domain.Entities;
using BGB.Gerencial.Domain.Enums;
using BGB.Gerencial.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BGB.Gerencial.Application.Services
{
    public class ContratoApplication
    {
        public List<ResultadoDTO> Calcular(List<Cotacao> cotacoes, Contrato contrato)
        {
            var resultados = new List<Resultado>();
            foreach (DateTime dataAtual in contrato.DatasPorLinha)
            {
                var cotacaoCdiDiaria = cotacoes.FirstOrDefault(x => x.Tipo == "CDI" && x.Data == dataAtual);
                if (cotacaoCdiDiaria == null)
                    cotacaoCdiDiaria = cotacoes.FirstOrDefault(x => x.Tipo == "CDI" && x.Data == new DateTime(2020, 03, 12));

                var cotacaoTmcDiaria = cotacoes.FirstOrDefault(x => x.Tipo == "TMC" && x.Data == dataAtual && x.Data <= new DateTime(2020, 03, 12));
                if (cotacaoTmcDiaria == null)
                    cotacaoTmcDiaria = cotacoes.FirstOrDefault(x => x.Tipo == "TMC" && x.Data == new DateTime(2020, 03, 12));
                if (cotacaoTmcDiaria == null || cotacaoCdiDiaria == null)
                    throw new Exception($"Falta cotações do dia {dataAtual.ToString("dd/MM/yyyy")}.");
                //TODO: descomentar .LastOrDefault();

                //Linha Inicial
                if (resultados.Count == 0)
                {
                    resultados.Add(contrato.Indice == IndiceContratoEnum.PRE
                        ? new ResultadoPre(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria) as Resultado
                        : new ResultadoCdi(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria) as Resultado);
                }
                //Linha Sem Movimento
                if (!contrato.Movimentos.Any(x => x.Data == dataAtual) && !resultados.Any(x => x.Data == dataAtual))
                {
                    resultados.Add(contrato.Indice == IndiceContratoEnum.PRE
                        ? new ResultadoPre(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria, resultados.LastOrDefault()) as Resultado
                        : new ResultadoCdi(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria, resultados.LastOrDefault()) as Resultado);
                }
                //Linha Com Movimento
                if (contrato.Movimentos.Any(x => x.Data == dataAtual))
                {
                    foreach (Movimento item in contrato.Movimentos.Where(x => x.Data == dataAtual))
                    {
                        resultados.Add(contrato.Indice == IndiceContratoEnum.PRE
                            ? new ResultadoPre(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria, resultados.LastOrDefault(), item) as Resultado
                            : new ResultadoCdi(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria, resultados.LastOrDefault(), item) as Resultado);
                    }
                }
            }
            if (contrato.DataInicial.Year < DateTime.Today.Year)
            {
                resultados[0].Data = new DateTime(DateTime.Today.Year - 1, 12, 31);
            }
            //return resultados;
            return resultados.Select(x => new ResultadoDTO()
            {
                Data = x.Data,
                DiasAtraso = x.DiasAtraso,
                ValorMovimento = x.Movimento != null ? x.Movimento.Valor : 0.00,
                ValorCdi = x.CotacaoCdi != null ? x.CotacaoCdi.Fator : 0.00,
                ValorTmc = x.CotacaoTmc != null ? x.CotacaoTmc.Fator : 0.00,
                SaldoInicial = x.SaldoInicial,
                SaldoFinal = x.SaldoFinal,
                CustoInicial = x.CustoInicial,
                CustoFinal = x.CustoFinal,
                ResultadoGerencial = x.ResultadoGerencial,
                CustoInicialConciliacao = x.CustoInicialConciliacao,
                CustoFinalConciliacao = x.CustoFinalConciliacao,
                ResultadoConciliacao = x.ResultadoConciliacao
            }).ToList();
        }
    }
}
