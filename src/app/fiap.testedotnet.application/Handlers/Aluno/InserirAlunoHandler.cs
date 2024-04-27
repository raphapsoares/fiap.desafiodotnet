using fiap.testedotnet.domain.Commands.Aluno;
using fiap.testedotnet.domain.Contracts.Repositories;
using fiap.testedotnet.domain.Extensions;
using fiap.testedotnet.domain.RequestResults;
using MediatR;

namespace fiap.testedotnet.application.Handlers.Aluno
{
    public class InserirAlunoHandler : IRequestHandler<InserirAlunoCommand, RequestResult>
    {
        private readonly IAlunoRepository _repository;
        public InserirAlunoHandler(IAlunoRepository repository)
        {
            _repository = repository;
        }

        public async Task<RequestResult> Handle(InserirAlunoCommand command, CancellationToken cancellationToken)
        {
            List<string> mensagens = new List<string>();

            if (command.Validate(ref mensagens))
                return new RequestResult(false, mensagens);

            command.Senha = command.Senha.ToSHA256Hash();

            var resultado = await _repository.Inserir(command);

            if (!resultado)
            {
                mensagens.Add("Ocorreu algum erro ao inserir o Aluno");
                return new RequestResult(false, mensagens);
            }

            mensagens.Add("Aluno incluído com sucesso");
            return new RequestResult(true, mensagens);
        }
    }
}
