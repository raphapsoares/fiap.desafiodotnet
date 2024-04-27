using fiap.testedotnet.domain.Commands.Turma;
using fiap.testedotnet.domain.Contracts.Repositories;
using fiap.testedotnet.domain.RequestResults;
using MediatR;

namespace fiap.testedotnet.application.Handlers
{
    public class InserirTurmaHandler : IRequestHandler<InserirTurmaCommand, RequestResult>
    {
        private readonly ITurmaRepository _turmaRepository;

        public InserirTurmaHandler(ITurmaRepository turmaRepository)
        {
            _turmaRepository = turmaRepository;
        }

        public async Task<RequestResult> Handle(InserirTurmaCommand command, CancellationToken cancellationToken)
        {
            List<string> mensagens = new List<string>();

            if (command.Validate(ref mensagens))
                return new RequestResult(false, mensagens);

            var turmaExiste = await _turmaRepository.ObterPorNome(command.Turma.ToLower());

            if (turmaExiste != null)
                return new RequestResult(false, "Nome de turma já cadastrada.");

            var resultado = await _turmaRepository.Inserir(command);

            if (!resultado)
            {
                mensagens.Add("Ocorreu um erro ao inserir a Turma");
                return new RequestResult(false, mensagens);
            }

            mensagens.Add("Turma incluída com sucesso");
            return new RequestResult(true, mensagens);
        }
    }
}
