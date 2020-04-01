using BGB.Gerencial.Domain.ValueObjects;
using System;
using System.Linq;

namespace BGB.Gerencial.Data.Interfaces
{
    public interface ICotacaoRepository
    {
        IQueryable<Cotacao> GetByPeriodo(DateTime dataInicial, DateTime dataFinal);
    }
}
