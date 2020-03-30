using System;

namespace BGB.Gerencial.Domain.Entities
{
    public class Cotacao
    {
        public DateTime Data { get; set; }
        public double Fator { get; set; }
        public string Tipo { get; set; }
    }
}
