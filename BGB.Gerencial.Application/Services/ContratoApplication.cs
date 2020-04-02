using BGB.Gerencial.Application.Interfaces;
using BGB.Gerencial.Application.Models;
using BGB.Gerencial.Data.Interfaces;
using BGB.Gerencial.Domain.Entities;
using BGB.Gerencial.Domain.Enums;
using BGB.Gerencial.Domain.Extensions;
using BGB.Gerencial.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BGB.Gerencial.Application.Services
{
    public class ContratoApplication : IContratoApplication
    {
        private ICotacaoRepository _cotacaoRepository { get; set; }
        public ContratoApplication(ICotacaoRepository cotacaoRepository)
        {
            _cotacaoRepository = cotacaoRepository;
        }

        public void Calcular(Contrato contrato)
        {
            IQueryable<Cotacao> cotacoes = _cotacaoRepository.GetByPeriodo(contrato.Inicio, contrato.Fim);
            
            foreach (DateTime dataAtual in DatasDeResultado(contrato))
            {
                Cotacao cotacaoCdiDiaria = BuscarCotacaoDiaria(cotacoes.Where(x => x.Tipo == "CDI"), dataAtual);
                Cotacao cotacaoTmcDiaria = BuscarCotacaoDiaria(cotacoes.Where(x => x.Tipo == "TMC"), dataAtual);

                //Linha Inicial
                if (contrato.Resultados.Count == 0)
                    AdicionarLinhaInicial(contrato, dataAtual, cotacaoCdiDiaria, cotacaoTmcDiaria);

                //Linha Sem Movimento
                if (!contrato.Movimentos.Any(x => x.Data == dataAtual) && !contrato.Resultados.Any(x => x.Data == dataAtual))
                    AdicionarLinhaSemMovimento(contrato, dataAtual, cotacaoCdiDiaria, cotacaoTmcDiaria);

                //Linha Com Movimento
                if (contrato.Movimentos.Any(x => x.Data == dataAtual))
                {
                    foreach (Movimento item in contrato.Movimentos.Where(x => x.Data == dataAtual))
                    {
                        AdicionarLinhaComMovimento(contrato, dataAtual, cotacaoCdiDiaria, cotacaoTmcDiaria, item);
                    }
                }

            }

            SetPrimeiraLinha(contrato);

            //return resultados.Select(x => new ResultadoDTO()
            //{
            //    Data = x.Data,
            //    DiasAtraso = x.DiasAtraso,
            //    ValorMovimento = x.Movimento != null ? x.Movimento.Valor : 0.00,
            //    ValorCdi = x.CotacaoCdi != null ? x.CotacaoCdi.Fator : 0.00,
            //    ValorTmc = x.CotacaoTmc != null ? x.CotacaoTmc.Fator : 0.00,
            //    SaldoInicial = x.SaldoInicial,
            //    SaldoFinal = x.SaldoFinal,
            //    CustoInicial = x.CustoInicial,
            //    CustoFinal = x.CustoFinal,
            //    ResultadoGerencial = x.ResultadoGerencial,
            //    CustoInicialConciliacao = x.CustoInicialConciliacao,
            //    CustoFinalConciliacao = x.CustoFinalConciliacao,
            //    ResultadoConciliacao = x.ResultadoConciliacao
            //}).ToList();
        }

        #region Private Methods
        private Cotacao BuscarCotacaoDiaria(IEnumerable<Cotacao> cotacoes, DateTime dataAtual)
        {
            var cotacaoTmcDiaria = cotacoes.FirstOrDefault(x => x.Data == dataAtual);
            if (cotacaoTmcDiaria == null)
                cotacaoTmcDiaria = cotacoes.LastOrDefault(x => x.Data <= dataAtual);

            if (cotacaoTmcDiaria == null)
                throw new Exception($"Falta cotação do dia {dataAtual.ToString("dd/MM/yyyy")}.");

            return cotacaoTmcDiaria;
        }

        private void AdicionarLinhaComMovimento(Contrato contrato, DateTime dataAtual, Cotacao cotacaoCdiDiaria, Cotacao cotacaoTmcDiaria, Movimento item)
        {
            contrato.Adicionar(contrato.Indice == IndiceContratoEnum.PRE
                                        ? new ResultadoPre(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria, contrato.Resultados.LastOrDefault(), item) as Resultado
                                        : new ResultadoCdi(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria, contrato.Resultados.LastOrDefault(), item) as Resultado);
        }

        private void AdicionarLinhaSemMovimento(Contrato contrato, DateTime dataAtual, Cotacao cotacaoCdiDiaria, Cotacao cotacaoTmcDiaria)
        {
            contrato.Adicionar(contrato.Indice == IndiceContratoEnum.PRE
                ? new ResultadoPre(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria, contrato.Resultados.LastOrDefault()) as Resultado
                : new ResultadoCdi(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria, contrato.Resultados.LastOrDefault()) as Resultado);
        }

        private void AdicionarLinhaInicial(Contrato contrato, DateTime dataAtual, Cotacao cotacaoCdiDiaria, Cotacao cotacaoTmcDiaria)
        {
            contrato.Adicionar(contrato.Indice == IndiceContratoEnum.PRE
                ? new ResultadoPre(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria) as Resultado
                : new ResultadoCdi(dataAtual, contrato, cotacaoCdiDiaria, cotacaoTmcDiaria) as Resultado);
        }

        private void SetPrimeiraLinha(Contrato contrato)
        {
            ///Setar Primeira Linha como dia 31 de Dezembro do ano anterior ao atual
            ///Ex: Data Atual : 31-01-2020 - Primeira linha será o saldo do dia 31-12-2019
            if (contrato.Inicio.Year < DateTime.Today.Year)
            {
                contrato.Resultados[0].Data = new DateTime(DateTime.Today.Year - 1, 12, 31);
            }
        }

        private List<DateTime> DatasDeResultado(Contrato contrato)
        {
            var datas = new List<DateTime>();

            for (var diaAtual = contrato.Inicio; diaAtual.Date <= contrato.Fim; diaAtual = diaAtual.AddDays(1))
            {
                if (datas.Count == 0)
                    datas.Add(diaAtual);

                if (diaAtual == diaAtual.LastDayInMonth() && diaAtual.Date != contrato.Fim)
                    datas.Add(diaAtual);

                if (contrato.Movimentos.Any(x => x.Data == diaAtual))
                    datas.Add(diaAtual);

                if (diaAtual.Date == contrato.Fim)
                {
                    datas.Add(diaAtual);
                    //TODO:última data ser o último dia do mês -- datas.Add(diaAtual.LastDayInMonth());
                }
            }

            if (contrato.Inicio.Year < DateTime.Today.Year)
            {
                if (datas.Any(x => x < new DateTime(DateTime.Today.Year - 1, 12, 31)))
                    datas = datas.Where(x => x >= new DateTime(DateTime.Today.Year - 1, 12, 31)).ToList();
            }
            return datas;
        }
        #endregion
    }
}
