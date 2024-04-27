namespace fiap.testedotnet.ui.Models
{
    public class ResponseApi<T>
    {
        public ResponseApi()
        {
            
        }

        public bool Sucesso { get; set; }
        public List<string>? Mensagens { get; set; }
        public T? Dados { get; set; }
    }
}
