using fiap.testedotnet.domain.RequestResults;
using MediatR;

namespace fiap.testedotnet.domain.Commands.Aluno
{
    public class AlunoBaseCommand : IRequest<RequestResult>
    {
        public required string Nome { get; set; }
        public required string Usuario { get; set; }
        public required string Senha { get; set; }

    }
}
