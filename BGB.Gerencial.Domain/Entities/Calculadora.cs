using BGB.Gerencial.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BGB.Gerencial.Domain.Entities
{
    public class Calculadora
    {
        private Contrato _contrato { get; set; }
        private List<Cotacao> Cotacoes { get; set; }

        public Calculadora(Contrato contrato, List<Cotacao> cotacoes)
        {
            _contrato = contrato;
            Cotacoes = cotacoes;
        }

        public List<DateTime> Datas
        {
            get
            {
                var datas = new List<DateTime>();

                for (var diaAtual = _contrato.DataInicial; diaAtual.Date <= _contrato.DataFinal; diaAtual = diaAtual.AddDays(1))
                {
                    if (datas.Count == 0)
                        datas.Add(diaAtual);

                    if (diaAtual == diaAtual.LastDayInMonth())
                        datas.Add(diaAtual);

                    //if (diaAtual == diaAtual.LastDayInYear())
                    //    resultados.Add(new Resultado() { Data = diaAtual });

                    if (_contrato.Movimentos.Any(x => x.Data == diaAtual))
                        datas.Add(diaAtual);

                    if (diaAtual.Date == _contrato.DataFinal)
                        datas.Add(diaAtual);
                }

                if (_contrato.DataInicial.Year < DateTime.Today.Year)
                {
                    if (datas.Any(x => x < new DateTime(DateTime.Today.Year - 1, 12, 31)))
                        datas = datas.Where(x => x >= new DateTime(DateTime.Today.Year - 1, 12, 31)).ToList();
                }
                return datas;
            }
        }

        public List<Resultado> Calcular()
        {
            var resultados = new List<Resultado>();
            foreach (var dataAtual in Datas)
            {
                if (dataAtual >= DateTime.Today)
                    resultados.Add(resultados.LastOrDefault());

                var cotacaoDiaria = Cotacoes.FirstOrDefault(x => x.Data == dataAtual);
                if (cotacaoDiaria == null)
                    cotacaoDiaria = Cotacoes.FirstOrDefault(x => x.Data == new DateTime(2020, 03, 12));
                //TODO: descomentar .LastOrDefault();
                //Linha Inicial
                if (resultados.Count == 0)
                    resultados.Add(GerarPrimeiroResultado(dataAtual, _contrato, cotacaoDiaria));
                //Linha Sem Movimento
                if (!_contrato.Movimentos.Any(x => x.Data == dataAtual) && !resultados.Any(x => x.Data == dataAtual))
                    resultados.Add(GerarLinhaSemMovimento(dataAtual, _contrato, cotacaoDiaria, resultados.LastOrDefault()));
                //Linha Com Movimento
                if (_contrato.Movimentos.Any(x => x.Data == dataAtual))
                {
                    foreach (Movimento item in _contrato.Movimentos.Where(x => x.Data == dataAtual))
                    {
                        resultados.Add(GerarLinhaComMovimento(dataAtual, _contrato, cotacaoDiaria, resultados.LastOrDefault(), item));
                    }
                }
            }
            if (_contrato.DataInicial.Year < DateTime.Today.Year)
            {
                resultados[0].Data = new DateTime(DateTime.Today.Year - 1, 12, 31);
            }
            return resultados;
        }

        public Resultado GerarPrimeiroResultado(DateTime data, Contrato contrato, Cotacao cotacao)
        {
            return new Resultado()
            {
                Data = data,
                FatorCdi = cotacao == null ? 0.00 : cotacao.Fator,
                Movimentacao = 0.00,
                SaldoInicial = contrato.Valor,
                SaldoFinal = contrato.Valor,
                CustoInicial = contrato.Valor * -1,
                CustoFinal = contrato.Valor * -1,
                ResultadoCustoInicial = contrato.Valor * -1,
                ResultadoCustoFinal = contrato.Valor * -1,
                ResultadoAcumulado = contrato.Valor * -1
            };
        }

        public Resultado GerarLinhaSemMovimento(DateTime data, Contrato contrato, Cotacao cotacao, Resultado resultadoAnterior)
        {
            var resultado = new Resultado()
            {
                Data = data,
                FatorCdi = cotacao == null ? 0 : cotacao.Fator,
                Movimentacao = 0.00
            };
            resultado.CalcularSaldoInicial(_contrato, resultadoAnterior);
            resultado.SaldoFinal = resultado.SaldoInicial;
            resultado.CalcularCustoInicial(_contrato, resultadoAnterior);
            resultado.CustoFinal = resultado.CustoInicial;
            resultado.ResultadoCustoInicial = contrato.Valor * -1;
            resultado.ResultadoCustoFinal = contrato.Valor * -1;
            resultado.ResultadoAcumulado = contrato.Valor * -1;
            return resultado;
        }

        public Resultado GerarLinhaComMovimento(DateTime data, Contrato contrato, Cotacao cotacao, Resultado resultadoAnterior, Movimento movimentoAtual)
        {
            var resultado = new Resultado()
            {
                Data = data,
                FatorCdi = cotacao == null ? 0.00 : cotacao.Fator,
                Movimentacao = movimentoAtual.Valor
            };
            resultado.CalcularSaldoInicial(_contrato, resultadoAnterior);
            resultado.SaldoFinal = resultado.SaldoInicial + movimentoAtual.Valor;
            resultado.CalcularCustoInicial(_contrato, resultadoAnterior);
            resultado.CustoFinal = resultado.CustoInicial + movimentoAtual.Valor;
            resultado.ResultadoCustoInicial = contrato.Valor * -1;
            resultado.ResultadoCustoFinal = contrato.Valor * -1;
            resultado.ResultadoAcumulado = contrato.Valor * -1;
            return resultado;
        }

    }
}
