using fiap.testedotnet.domain.Commands.Relacionamento;
using fiap.testedotnet.domain.Contracts.Repositories;
using fiap.testedotnet.domain.RequestResults;
using MediatR;

namespace fiap.testedotnet.application.Handlers.Relacionamento
{
    public class InserirRelacionamentoHandler : IRequestHandler<InserirRelacionamentoCommand, RequestResult>
    {
        private readonly IRelacionamentoRepository _relacionamentoRepository;
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _turmaRepository;

        public InserirRelacionamentoHandler(IRelacionamentoRepository relacionamentoRepository,
            IAlunoRepository alunoRepository,
            ITurmaRepository turmaRepository)
        {
            _relacionamentoRepository = relacionamentoRepository;
            _alunoRepository = alunoRepository;
            _turmaRepository = turmaRepository;
        }

        public async Task<RequestResult> Handle(InserirRelacionamentoCommand command, CancellationToken cancellationToken)
        {
            List<string> validacoes = new List<string>();

            if (command.Validate(ref validacoes))
                return new RequestResult(false, validacoes);

            var alunoExiste = await _alunoRepository.Obter(command.AlunoId);
            var turmaExiste = await _turmaRepository.Obter(command.TurmaId);

            if (alunoExiste == null && turmaExiste == null)
                return new RequestResult(false, "Aluno ou turma não existem");

            var relacionamentoExiste = await _relacionamentoRepository.Obter(command.AlunoId, command.TurmaId);

            if (relacionamentoExiste != null)
            {
                validacoes.Add("Aluno já associado a turma");
                return new RequestResult(false, validacoes);
            }

            var resultado = await _relacionamentoRepository.Inserir(command);

            if (!resultado)
            {
                validacoes.Add("Ocorreu um erro ao associar o aluno a turma");
                return new RequestResult(false, validacoes);
            }

            validacoes.Add("Aluno matriculado!");
            return new RequestResult(true, validacoes);
        }
    }
}
