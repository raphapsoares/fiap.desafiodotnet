using fiap.testedotnet.domain.Contracts.Repositories;
using fiap.testedotnet.domain.Contracts.Services;
using fiap.testedotnet.domain.RequestResults;

namespace fiap.testedotnet.application.Services
{
    public class RelacionamentoService : IRelacionamentoService
    {
        private readonly IRelacionamentoRepository _relacionamentoRepository;

        public RelacionamentoService(IRelacionamentoRepository relacionamentoRepository)
        {
            _relacionamentoRepository = relacionamentoRepository;
        }
        public async Task<RequestResult> Deletar(int alunoId, int turmaId)
        {
            List<string> validacoes = new List<string>();

            if (alunoId <= 0 || turmaId <= 0)
            {
                validacoes.Add("Id do aluno ou turma é inválido");
                return new RequestResult(false, validacoes);
            }

            var relacaoExiste = await _relacionamentoRepository.Obter(alunoId, turmaId);

            if(relacaoExiste == null)
            {
                validacoes.Add("Aluno já não está mais associado a turma");
                return new RequestResult(false, validacoes);
            }

            var resultado = await _relacionamentoRepository.Deletar(alunoId, turmaId);

            if (!resultado)
            {
                validacoes.Add("Ocorreu um erro ao desassociar o aluno da turma");
                return new RequestResult(false, validacoes);
            }

            validacoes.Add("Relação inativada com sucesso!");
            return new RequestResult(true, validacoes);
        }
        public async Task<RequestResult> Obter(int alunoId, int turmaId)
        {
            List<string> validacoes = new List<string>();

            if (alunoId <= 0 || turmaId <= 0)
            {
                validacoes.Add("Id do aluno ou turma é inválido");
                return new RequestResult(false, validacoes);
            }

            var resultado = await _relacionamentoRepository.Obter(alunoId, turmaId);

            if(resultado == null)
            {
                validacoes.Add("Não foi encontrada relação do aluno com a turma");
                return new RequestResult(false, validacoes);
            }

            return new RequestResult(true, resultado);
        }

        public async Task<RequestResult> ObterLista()
        {
            List<string> validacoes = new List<string>();
            var resultado = await _relacionamentoRepository.ObterLista();

            if (resultado == null || !resultado.Any())
            {
                validacoes.Add("Nenhuma relação encontrada");
                return new RequestResult(false, validacoes);
            }

            return new RequestResult(true, resultado);
        }
    }
}
