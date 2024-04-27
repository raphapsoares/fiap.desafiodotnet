using Dapper;
using fiap.testedotnet.domain.Commands.Turma;
using fiap.testedotnet.domain.Contracts.Repositories;
using fiap.testedotnet.domain.Entities;
using fiap.testedotnet.domain.Scripts;
using System.Data;

namespace fiap.testedotnet.infrastructure.Repositories
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly IDbConnection _connection;
        public TurmaRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<bool> Atualizar(AtualizarTurmaCommand command)
        {
            string sql = SQLScripts.SQL_ATUALIZAR_TURMA;

            var param = new DynamicParameters();
            param.Add("@cursoId", command.CursoId);
            param.Add("@turma", command.Turma);
            param.Add("@ano", command.Ano);
            param.Add("@id", command.Id);

            var result = await _connection.ExecuteAsync(sql: sql, param: param);
            return result > 0 ? true : false;
        }

        public async Task<bool> Deletar(int id)
        {
            string sql = SQLScripts.SQL_DELETAR_TURMA;

            var param = new DynamicParameters();
            param.Add("@id", id);

            var result = await _connection.ExecuteAsync(sql: sql, param: param);
            return result > 0 ? true : false;
        }

        public async Task<bool> Inserir(InserirTurmaCommand command)
        {
            string sql = SQLScripts.SQL_INSERIR_TURMA;

            var param = new DynamicParameters();
            param.Add("@cursoId", command.CursoId);
            param.Add("@turma", command.Turma);
            param.Add("@ano", command.Ano);

            var result = await _connection.ExecuteAsync(sql: sql, param: param);
            return result > 0 ? true : false;
        }

        public async Task<Turma?> Obter(int id)
        {
            string sql = SQLScripts.SQL_OBTER_TURMA;

            var param = new DynamicParameters();
            param.Add("@id", id);

            var result = await _connection.QueryFirstOrDefaultAsync<Turma>(sql: sql, param: param);
            return result != null ? result : null;
        }

        public async Task<List<Turma>> ObterLista()
        {
            var sql = SQLScripts.SQL_OBTER_LISTA_TURMA;

            var result = await _connection.QueryAsync<Turma>(sql: sql);
            return result.ToList();
        }

        public async Task<Turma?> ObterPorNome(string turma)
        {
            string sql = SQLScripts.SQL_OBTER_TURMA_POR_NOME;

            var param = new DynamicParameters();
            param.Add("@turma", turma);

            var result = await _connection.QueryFirstOrDefaultAsync<Turma>(sql: sql, param: param);
            return result != null ? result : null;
        }
    }
}
