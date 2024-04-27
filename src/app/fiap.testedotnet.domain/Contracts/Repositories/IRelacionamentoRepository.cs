using fiap.testedotnet.domain.Commands.Relacionamento;
using fiap.testedotnet.domain.Entities;

namespace fiap.testedotnet.domain.Contracts.Repositories
{
    public interface IRelacionamentoRepository
    {
        Task<bool> Inserir(InserirRelacionamentoCommand command);
        Task<bool> Deletar(int alunoId, int turmaId);
        Task<bool> DeletarPorAluno(int alunoId);
        Task<bool> DeletarPorTurma(int turmaId);
        Task<Relacionamento?> Obter(int alunoId, int turmaId);
        Task<List<Relacionamento>> ObterLista();
        Task<List<Relacionamento>> ObterListaPorAluno(int alunoId);
        Task<List<Relacionamento>> ObterListaPorTurma(int turmaId);
    }
}
