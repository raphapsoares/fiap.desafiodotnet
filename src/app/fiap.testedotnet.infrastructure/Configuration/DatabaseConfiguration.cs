namespace fiap.testedotnet.infrastructure.Configuration
{
    public class DatabaseConfiguration
    {
        public string ConnectionString { get; private set; }

        public void LoadConfiguration()
        {
            // Aqui você pode implementar a lógica para carregar as configurações do banco de dados,
            // seja de um arquivo de configuração, variáveis de ambiente, etc.

            // Por simplicidade, vou apenas simular uma carga de configuração estática
            ConnectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
        }
    }
}
