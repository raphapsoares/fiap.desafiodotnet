using fiap.testedotnet.domain.RequestResults;

namespace fiap.testedotnet.domain.Contracts.Services
{
    public interface ITurmaService
    {
        Task<RequestResult> Obter(int id);
        Task<RequestResult> ObterLista();
        Task<RequestResult> Deletar(int id);
    }
}
