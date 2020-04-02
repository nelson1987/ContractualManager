using BGB.Gerencial.Domain.Enums;
using BGB.Gerencial.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace BGB.Gerencial.Domain.Entities
{
    public class Contrato
    {
        public Contrato(double valor, double taxa, IndiceContratoEnum indice, DateTime inicio, DateTime fim)
        {
            Valor = valor;
            Indice = indice;
            Taxa = taxa;
            Inicio = inicio;
            Fim = fim;
            Movimentos = new List<Movimento>();
            Resultados = new List<Resultado>();
            IsValid();
        }
        public Contrato(double valor, double taxa, IndiceContratoEnum indice, DateTime inicio, DateTime fim, string codigoContrato)
            :this(valor, taxa, indice, inicio, fim)
        {
            Codigo = codigoContrato;
        }

        public string Codigo { get; private set; }
        public double Valor { get; private set; }
        public double Taxa { get; private set; }
        public IndiceContratoEnum Indice { get; private set; }
        public DateTime Inicio { get; private set; }
        public DateTime Fim { get; private set; }
        public List<Movimento> Movimentos { get; private set; }
        public List<Resultado> Resultados { get; private set; }

        public string TaxaContratual { get { return $"{Taxa * 100}%"; } }

        private void IsValid()
        {
            if (Valor == 0.00) throw new Exception();
            if (Taxa == 0.00) throw new Exception();
            //if (Indice == null) throw new Exception();
            if (Inicio == DateTime.MinValue) throw new Exception();
            if (Fim == DateTime.MinValue) throw new Exception();
        }

        public void Adicionar(Movimento movimento)
        {
            Movimentos.Add(movimento);
        }

        public void Adicionar(Resultado resultados)
        {
            if (Indice == IndiceContratoEnum.PRE && resultados is ResultadoCdi)
                throw new Exception();
            if (Indice == IndiceContratoEnum.CDI && resultados is ResultadoPre)
                throw new Exception();

            Resultados.Add(resultados);
        }
    }
}
