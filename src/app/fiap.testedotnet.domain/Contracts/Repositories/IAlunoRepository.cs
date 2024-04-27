using fiap.testedotnet.domain.Commands.Aluno;
using fiap.testedotnet.domain.Entities;


namespace fiap.testedotnet.domain.Contracts.Repositories
{
    public interface IAlunoRepository
    {
        Task<bool> Inserir(InserirAlunoCommand command);
        Task<bool> Atualizar(AtualizarAlunoCommand command);
        Task<bool> Deletar(int id);
        Task<Aluno?> Obter(int id);
        Task<List<Aluno>> ObterLista();
    }
}
