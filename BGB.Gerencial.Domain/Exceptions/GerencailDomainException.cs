using System;

namespace BGB.Gerencial.Domain.Exceptions
{
    public class GerencailDomainException : Exception
    {
        public GerencailDomainException()
        {
        }

        public GerencailDomainException(string message) : base(message)
        {
        }

        public GerencailDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
