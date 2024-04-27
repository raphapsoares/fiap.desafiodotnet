using fiap.testedotnet.domain.Contracts.Repositories;
using fiap.testedotnet.domain.Contracts.Services;
using fiap.testedotnet.domain.RequestResults;


namespace fiap.testedotnet.application.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IRelacionamentoRepository _relacionamentoRepository;

        public AlunoService(IAlunoRepository alunoRepository, IRelacionamentoRepository relacionamentoRepository)
        {
            _alunoRepository = alunoRepository;
            _relacionamentoRepository = relacionamentoRepository;
        }

        public async Task<RequestResult> Deletar(int id)
        {
            List<string> validacoes = new List<string>();

            if (id <= 0)
            {
                validacoes.Add("Id do aluno não é válido");
                return new RequestResult(false, validacoes);
            }

            var alunoExiste = await _alunoRepository.Obter(id);

            if (alunoExiste == null)
            {
                validacoes.Add("Aluno não existe ou já foi excluido");
                return new RequestResult(false, validacoes);
            }

            var relacionamentoAluno = await _relacionamentoRepository.ObterListaPorAluno(id);

            if(relacionamentoAluno != null && relacionamentoAluno.Any())
            {
                var deletarRelacionamentos = await _relacionamentoRepository.DeletarPorAluno(id);

                if(!deletarRelacionamentos)
                {
                    validacoes.Add("Não foi possivel excluir o aluno. Ocorreu um erro ao excluir seu relacionamento com a instituição");
                    return new RequestResult(false, validacoes);
                }
            }

            var resultado = await _alunoRepository.Deletar(id);

            if (!resultado)
            {
                validacoes.Add("Ocorreu um erro ao excluir o aluno");
                return new RequestResult(true, validacoes);
            }

            validacoes.Add("Aluno excluido com sucesso!");
            return new RequestResult(true, validacoes);
        }
        public async Task<RequestResult> Obter(int id)
        {
            List<string> validacoes = new List<string>();
            var resultado = await _alunoRepository.Obter(id);

            if (resultado == null)
            {
                validacoes.Add("Aluno não existe ou já foi deletado");
                return new RequestResult(false, validacoes);
            }

            return new RequestResult(true, resultado);
        }
        public async Task<RequestResult> ObterLista()
        {
            List<string> validacoes = new List<string>();
            var resultado = await _alunoRepository.ObterLista();

            if (resultado == null || !resultado.Any())
            {
                validacoes.Add("Nenhum aluno encontrado na base de dados");
                return new RequestResult(false, validacoes);
            }

            return new RequestResult(true, resultado);
        }

    }
}
