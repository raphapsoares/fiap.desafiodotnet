using fiap.testedotnet.domain.Contracts.Repositories;
using fiap.testedotnet.domain.Contracts.Services;
using fiap.testedotnet.domain.RequestResults;

namespace fiap.testedotnet.application.Services
{
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;
        private readonly IRelacionamentoRepository _relacionamentoRepository;

        public TurmaService(ITurmaRepository turmaRepository,
            IRelacionamentoRepository relacionamentoRepository)
        {
            _turmaRepository = turmaRepository;
            _relacionamentoRepository = relacionamentoRepository;
        }
        public async Task<RequestResult> Deletar(int id)
        {
            List<string> validacoes = new List<string>();

            if (id <= 0)
            {
                validacoes.Add("Id da turma não é válido");
                return new RequestResult(false, validacoes);
            }

            var turmaExiste = await _turmaRepository.Obter(id);

            if (turmaExiste == null)
            {
                validacoes.Add("Turma não existe ou já foi excluida");
                return new RequestResult(false, validacoes);
            }

            var relacionamentoAlunoTurma = await _relacionamentoRepository.ObterListaPorTurma(id);

            if (relacionamentoAlunoTurma != null && relacionamentoAlunoTurma.Any())
            {
                var deletarRelacionamentos = await _relacionamentoRepository.DeletarPorTurma(id);

                if (!deletarRelacionamentos)
                {
                    validacoes.Add("Não foi possivel excluir a turma. Ocorreu um erro ao excluir o relacionamento entre aluno e aluno");
                    return new RequestResult(false, validacoes);
                }
            }

            var resultado = await _turmaRepository.Deletar(id);

            if (!resultado)
            {
                validacoes.Add("Ocorreu um erro ao excluir a turma");
                return new RequestResult(false, validacoes);
            }

            validacoes.Add("Turma excluida com sucesso!");
            return new RequestResult(true, validacoes);
        }
        public async Task<RequestResult> Obter(int id)
        {
            List<string> validacoes = new List<string>();

            if (id <= 0)
            {
                validacoes.Add("Id da turma não é válido");
                return new RequestResult(false, validacoes);
            }

            var result = await _turmaRepository.Obter(id);

            if (result == null)
            {
                validacoes.Add("Turma não foi encontrada");
                return new RequestResult(false, validacoes);
            }

            return new RequestResult(true, result);
        }
        public async Task<RequestResult> ObterLista()
        {
            List<string> validacoes = new List<string>();
            var resultado = await _turmaRepository.ObterLista();

            if (resultado == null || !resultado.Any())
            {
                validacoes.Add("Nenhuma turma encontrada na base de dados");
                return new RequestResult(false, validacoes);
            }

            return new RequestResult(true, resultado);
        }
    }
}
