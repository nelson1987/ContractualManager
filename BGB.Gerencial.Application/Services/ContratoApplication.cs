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
                    cotacaoCdiDiaria = cotacoes.LastOrDefault(x => x.Tipo == "CDI");

                var cotacaoTmcDiaria = cotacoes.FirstOrDefault(x => x.Tipo == "TMC" && x.Data == dataAtual);
                if (cotacaoTmcDiaria == null)
                    cotacaoTmcDiaria = cotacoes.LastOrDefault(x => x.Tipo == "TMC" && x.Data >= dataAtual);

                if (cotacaoTmcDiaria == null || cotacaoCdiDiaria == null)
                    throw new Exception($"Falta cotações do dia {dataAtual.ToString("dd/MM/yyyy")}.");

                //Linha Inicial
                if (resultados.Count == 0)
                    AdicionarLinhaInicial(contrato, resultados, dataAtual, cotacaoCdiDiaria, cotacaoTmcDiaria);

                //Linha Sem Movimento
                if (!contrato.Movimentos.Any(x => x.Data == dataAtual) && !resultados.Any(x => x.Data == dataAtual))
                    AdicionarLinhaSemMovimento(contrato, resultados, dataAtual, cotacaoCdiDiaria, cotacaoTmcDiaria);

                //Linha Com Movimento
                if (contrato.Movimentos.Any(x => x.Data == dataAtual))
                {
                    foreach (Movimento item in contrato.Movimentos.Where(x => x.Data == dataAtual))
                    {
                        AdicionarLinhaComMovimento(contrato, resultados, dataAtual, cotacaoCdiDiaria, cotacaoTmcDiaria, item);
                    }
                }

            }

            SetPrimeiraLinha(contrato, resultados);
                
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

        private void AdicionarLinhaComMovimento(Contrato contrato, List<Resultado> resultados, DateTime dataAtual, Cotacao cotacaoCdiDiaria, Cotacao cotacaoTmcDiaria, Movimento item)
        {
            resultados.Add(contrato.Indice == IndiceContratoEnum.PRE
                                        ? new ResultadoPre(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria, resultados.LastOrDefault(), item) as Resultado
                                        : new ResultadoCdi(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria, resultados.LastOrDefault(), item) as Resultado);
        }

        private void AdicionarLinhaSemMovimento(Contrato contrato, List<Resultado> resultados, DateTime dataAtual, Cotacao cotacaoCdiDiaria, Cotacao cotacaoTmcDiaria)
        {
            resultados.Add(contrato.Indice == IndiceContratoEnum.PRE
                ? new ResultadoPre(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria, resultados.LastOrDefault()) as Resultado
                : new ResultadoCdi(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria, resultados.LastOrDefault()) as Resultado);
        }

        private void AdicionarLinhaInicial(Contrato contrato, List<Resultado> resultados, DateTime dataAtual, Cotacao cotacaoCdiDiaria, Cotacao cotacaoTmcDiaria)
        {
            resultados.Add(contrato.Indice == IndiceContratoEnum.PRE
                ? new ResultadoPre(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria) as Resultado
                : new ResultadoCdi(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria) as Resultado);
        }

        private void SetPrimeiraLinha(Contrato contrato, List<Resultado> resultados)
        {
            ///Setar Primeira Linha como dia 31 de Dezembro do ano anterior ao atual
            ///Ex: Data Atual : 31-01-2020 - Primeira linha será o saldo do dia 31-12-2019
            if (contrato.DataInicial.Year < DateTime.Today.Year)
            {
                resultados[0].Data = new DateTime(DateTime.Today.Year - 1, 12, 31);
            }
        }
    }
}
