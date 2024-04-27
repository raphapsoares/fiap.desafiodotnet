using fiap.testedotnet.domain.RequestResults;

namespace fiap.testedotnet.domain.Contracts.Services
{
    public interface IRelacionamentoService
    {
        Task<RequestResult> Obter(int alunoId, int turmaId);
        Task<RequestResult> ObterLista();
        Task<RequestResult> Deletar(int alunoId, int turmaId);
    }
}
