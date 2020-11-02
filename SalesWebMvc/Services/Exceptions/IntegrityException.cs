using System;

namespace SalesWebMvc.Services.Exceptions
{
    //Classe de exceção personalizada de serviços para erros de integridade referencial
    public class IntegrityException : ApplicationException
    {
        public IntegrityException(string message) : base(message)
        {

        }
    }
}
