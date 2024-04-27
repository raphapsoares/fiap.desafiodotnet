using fiap.testedotnet.domain.Extensions;
using fiap.testedotnet.domain.RequestResults;
using MediatR;

namespace fiap.testedotnet.domain.Commands.Turma
{
    public class TurmaBaseCommand : IRequest<RequestResult>
    {
        public int CursoId { get; set; }
        public string Turma { get; set; }
        public int Ano { get; set; }

    }
}
