using fiap.testedotnet.domain.Commands.Turma;
using fiap.testedotnet.domain.Contracts.Repositories;
using fiap.testedotnet.domain.RequestResults;
using MediatR;

namespace fiap.testedotnet.application.Handlers.Turma
{
    public class AtualizarTurmaHandler : IRequestHandler<AtualizarTurmaCommand, RequestResult>
    {
        private readonly ITurmaRepository _turmaRepository;

        public AtualizarTurmaHandler(ITurmaRepository turmaRepository)
        {
            _turmaRepository = turmaRepository;
        }

        public async Task<RequestResult> Handle(AtualizarTurmaCommand command, CancellationToken cancellationToken)
        {
            List<string> mensagens = new List<string>();

            if (command.Validate(ref mensagens))
                return new RequestResult(false, mensagens);

            var turmaExiste = await _turmaRepository.Obter(command.Id);

            if(turmaExiste == null)
                return new RequestResult(false, "Turma não existe ou já foi excluída");

            var nomeTurmaExiste = await _turmaRepository.ObterPorNome(command.Turma.ToLower());

            if (nomeTurmaExiste != null)
                return new RequestResult(false, "Este nome já está associado a outra turma");

            var resultado = await _turmaRepository.Atualizar(command);

            if (!resultado)
            {
                mensagens.Add("Ocorreu um erro ao atualizar a turma");
                return new RequestResult(false, mensagens);
            }

            mensagens.Add("Turma atualizada com sucesso");
            return new RequestResult(true, mensagens);
        }
    }
}
