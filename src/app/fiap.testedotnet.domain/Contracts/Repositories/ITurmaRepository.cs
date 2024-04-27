using fiap.testedotnet.domain.Commands.Turma;
using fiap.testedotnet.domain.Entities;

namespace fiap.testedotnet.domain.Contracts.Repositories
{
    public interface ITurmaRepository
    {
        Task<bool> Inserir(InserirTurmaCommand command);
        Task<bool> Atualizar(AtualizarTurmaCommand command);
        Task<bool> Deletar(int id);
        Task<Turma?> Obter(int id);
        Task<List<Turma>> ObterLista();
        Task<Turma?> ObterPorNome(string turma);
    }
}
