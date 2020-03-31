using System;

namespace BGB.Gerencial.Domain.ValueObjects
{
    public class Cotacao
    {
        public DateTime Data { get; set; }
        public double Fator { get; set; }
        public string Tipo { get; set; }
    }
}
