using fiap.testedotnet.domain.Contracts.Repositories;
using fiap.testedotnet.domain.Contracts.Services;
using fiap.testedotnet.infrastructure.Repositories;
using System.Data;
using System.Data.SqlClient;
using MediatR;
using fiap.testedotnet.application.Handlers.Aluno;
using fiap.testedotnet.application.Handlers;
using fiap.testedotnet.application.Handlers.Turma;
using fiap.testedotnet.application.Services;
using fiap.testedotnet.application.Handlers.Relacionamento;

namespace fiap.testedotnet.api.Configurations
{
    public static class DependencyMapping
    {
        private static readonly IConfiguration _configuration;
        public static void ServiceMapping(this IServiceCollection services)
        {
            services.AddScoped<IAlunoService, AlunoService>();
            services.AddScoped<ITurmaService, TurmaService>();
            services.AddScoped<IRelacionamentoService, RelacionamentoService>();
        }
        public static void RepositoryMapping(this IServiceCollection services)
        {
            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<ITurmaRepository, TurmaRepository>();
            services.AddScoped<IRelacionamentoRepository, RelacionamentoRepository>();
        }
        public static void CommandHandlerMapping(this IServiceCollection services)
        {
            services.AddScoped<AtualizarAlunoHandler>();
            services.AddScoped<InserirAlunoHandler>();
            services.AddMediatR(typeof(InserirAlunoHandler));
            services.AddMediatR(typeof(AtualizarAlunoHandler));

            services.AddScoped<AtualizarTurmaHandler>();
            services.AddScoped<InserirTurmaHandler>();
            services.AddMediatR(typeof(InserirTurmaHandler));
            services.AddMediatR(typeof(AtualizarTurmaHandler));

            services.AddScoped<InserirRelacionamentoHandler>();
            services.AddMediatR(typeof(InserirRelacionamentoHandler));
        }
        public static void ConfigMapping(this IServiceCollection services)
        {
            services.AddTransient<IDbConnection>(provider =>
            {
                string connectionString = "Data Source=localhost,1433;User ID=sa;Password=1q2w3e4r@#$;Initial Catalog=DesafioDotNet;TrustServerCertificate=True;";//_configuration.GetConnectionString("DefaultConnection");
                return new SqlConnection(connectionString);
            });
        }
    }
}
