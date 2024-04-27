using fiap.testedotnet.domain.RequestResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace fiap.testedotnet.api.Configurations
{
    public static class ApiExtension
    {
        public static ObjectResult CustomResponse(this RequestResult resultado)
        {
            if (!resultado.Sucesso)
                return new ObjectResult(resultado) { StatusCode = (int)HttpStatusCode.UnprocessableEntity };

            return new ObjectResult(resultado) { StatusCode = (int)HttpStatusCode.OK };
        }
    }
}
