using BGB.Gerencial.Data.Interfaces;
using BGB.Gerencial.Domain.ValueObjects;
using System;
using System.Linq;

namespace BGB.Gerencial.Data.Repositories
{
    public class CotacaoRepository : ICotacaoRepository
    {
        public IQueryable<Cotacao> GetByPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            throw new NotImplementedException();
        }
    }
}
