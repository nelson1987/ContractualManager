using BGB.Gerencial.Domain.Enums;
using BGB.Gerencial.Domain.Extensions;
using BGB.Gerencial.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BGB.Gerencial.Domain.Entities
{
    public class Contrato
    {
        public Contrato()
        {
            Movimentos = new List<Movimento>();
            Resultados = new List<Resultado>();
        }

        public double Taxa { get; set; }
        public IndiceContratoEnum Indice { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public double Valor { get; set; }
        public List<Movimento> Movimentos { get; set; }
        public List<Resultado> Resultados { get; set; }

        public void Calcular(List<Cotacao> cotacoes)
        {
            CalculadoraContrato calculadora = new CalculadoraContrato(cotacoes);
            Resultados = calculadora.Calcular(DatasPorLinha, this);
        }

        public List<DateTime> DatasPorLinha
        {
            get
            {
                var datas = new List<DateTime>();

                for (var diaAtual = DataInicial; diaAtual.Date <= DataFinal; diaAtual = diaAtual.AddDays(1))
                {
                    if (datas.Count == 0)
                        datas.Add(diaAtual);

                    if (diaAtual == diaAtual.LastDayInMonth())
                        datas.Add(diaAtual);

                    //if (diaAtual == diaAtual.LastDayInYear())
                    //    resultados.Add(new Resultado() { Data = diaAtual });

                    if (Movimentos.Any(x => x.Data == diaAtual))
                        datas.Add(diaAtual);

                    if (diaAtual.Date == DataFinal)
                        datas.Add(diaAtual);
                }

                if (DataInicial.Year < DateTime.Today.Year)
                {
                    if (datas.Any(x => x < new DateTime(DateTime.Today.Year - 1, 12, 31)))
                        datas = datas.Where(x => x >= new DateTime(DateTime.Today.Year - 1, 12, 31)).ToList();
                }
                return datas;
            }
        }
    }
}
