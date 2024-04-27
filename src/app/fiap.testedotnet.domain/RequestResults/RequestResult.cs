namespace fiap.testedotnet.domain.RequestResults
{
    public class RequestResult
    {
        public RequestResult(bool sucesso, List<string> mensagens)
        {
            Sucesso = sucesso;
            Mensagens = mensagens;
        }

        public RequestResult(bool sucesso, object? dados)
        {
            Sucesso = sucesso;
            Dados = dados;
        }

        public bool Sucesso { get; }
        public object? Dados { get; }
        public List<string>? Mensagens { get; }
    }
}
