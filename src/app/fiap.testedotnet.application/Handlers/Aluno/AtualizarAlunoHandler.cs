using fiap.testedotnet.domain.RequestResults;
using fiap.testedotnet.domain.Extensions;
using fiap.testedotnet.domain.Contracts.Repositories;
using MediatR;
using fiap.testedotnet.domain.Commands.Aluno;


namespace fiap.testedotnet.application.Handlers.Aluno
{
    public class AtualizarAlunoHandler : IRequestHandler<AtualizarAlunoCommand, RequestResult>
    {
        private readonly IAlunoRepository _repository;
        public AtualizarAlunoHandler(IAlunoRepository repository)
        {
            _repository = repository;
        }
        public async Task<RequestResult> Handle(AtualizarAlunoCommand command, CancellationToken cancellationToken)
        {
            List<string> validacoes = new List<string>();

            if (command.Validate(ref validacoes))
                return new RequestResult(false, validacoes);

            command.Senha = command.Senha.ToSHA256Hash();

            var result = await _repository.Atualizar(command);

            if (!result)
            {
                validacoes.Add("Ocorreu algum erro ao atualizar os dados do Aluno");
                return new RequestResult(false, validacoes);
            }

            validacoes.Add("Dados do Aluno atualizado com sucesso");
            return new RequestResult(true, validacoes);
        }
    }
}
