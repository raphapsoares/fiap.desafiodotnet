using Dapper;
using fiap.testedotnet.domain.Commands.Aluno;
using fiap.testedotnet.domain.Contracts.Repositories;
using fiap.testedotnet.domain.Entities;
using fiap.testedotnet.domain.Scripts;
using System.Data;

namespace fiap.testedotnet.infrastructure.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly IDbConnection _connection;
        public AlunoRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<bool> Inserir(InserirAlunoCommand command)
        {
            string sql = SQLScripts.SQL_INSERIR_ALUNO;

            var param = new DynamicParameters();
            param.Add("@nome", command.Nome);
            param.Add("@usuario", command.Usuario);
            param.Add("@senha", command.Senha);

            var result = await _connection.ExecuteAsync(sql: sql, param: param);
            return result > 0 ? true : false;
        }
       
        public async Task<Aluno?> Obter(int id)
        {
            string sql = SQLScripts.SQL_OBTER_ALUNO;

            var param = new DynamicParameters();
            param.Add("@id", id);

            var result = await _connection.QueryFirstOrDefaultAsync<Aluno>(
                sql: sql,
                param: param);

            return result != null ? result : null;
        }

        public async Task<List<Aluno>> ObterLista()
        {
            string sql = SQLScripts.SQL_OBTER_LISTA_ALUNO;
            var result = await _connection.QueryAsync<Aluno>(sql: sql);
            return result.ToList();
        }

        public async Task<bool> Deletar(int id)
        {
            string sql = SQLScripts.SQL_DELETAR_ALUNO;

            var param = new DynamicParameters();
            param.Add("@id", id);

            var result = await _connection.ExecuteAsync(sql: sql, param: param);
            return result > 0 ? true : false;
        }

        public async Task<bool> Atualizar(AtualizarAlunoCommand command)
        {
            string sql = SQLScripts.SQL_ATUALIZAR_ALUNO;

            var param = new DynamicParameters();
            param.Add("@nome", command.Nome);
            param.Add("@usuario", command.Usuario);
            param.Add("@senha", command.Senha);
            param.Add("@id", command.Id);

            var result = await _connection.ExecuteAsync(sql: sql, param: param);
            return result > 0 ? true : false;
        }
    }
}
