using fiap.testedotnet.domain.RequestResults;
using MediatR;

namespace fiap.testedotnet.domain.Commands.Relacionamento
{
    public class InserirRelacionamentoCommand : IRequest<RequestResult>
    {
        public int AlunoId { get; set; }
        public int TurmaId { get; set; }

        public bool Validate(ref List<string> validacoes)
        {
            if (validacoes == null)
                validacoes = new List<string>();

            if (AlunoId <= 0)
                validacoes.Add("Id do Aluno não é válido");

            if (TurmaId <= 0)
                validacoes.Add("Id do Aluno não é válido");

            return validacoes.Any();
        }
    }
}
