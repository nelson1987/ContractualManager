using BGB.Gerencial.Application.Models;
using BGB.Gerencial.Domain.Entities;
using System.Collections.Generic;

namespace BGB.Gerencial.Application.Interfaces
{
    public interface IContratoApplication
    {
        List<ResultadoDTO> Calcular(Contrato contrato);
    }
}
