using Dapper;
using fiap.testedotnet.domain.Commands.Relacionamento;
using fiap.testedotnet.domain.Contracts.Repositories;
using fiap.testedotnet.domain.Entities;
using fiap.testedotnet.domain.Scripts;
using System.Data;
using System.Linq;

namespace fiap.testedotnet.infrastructure.Repositories
{
    public class RelacionamentoRepository : IRelacionamentoRepository
    {
        private readonly IDbConnection _connection;
        public RelacionamentoRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task<bool> Deletar(int alunoId, int turmaId)
        {
            string sql = SQLScripts.SQL_DELETAR_RELACIONAMENTO;
            var param = new DynamicParameters();
            param.Add("@alunoId", alunoId);
            param.Add("@turmaId", turmaId);

            var result = await _connection.ExecuteAsync(sql: sql, param: param);
            return result > 0 ? true : false;
        }

        public async Task<bool> DeletarPorAluno(int alunoId)
        {
            string sql = SQLScripts.SQL_DELETAR_RELACIONAMENTO_POR_ALUNO;
            var param = new DynamicParameters();
            param.Add("@alunoId", alunoId);

            var result = await _connection.ExecuteAsync(sql: sql, param: param);
            return result > 0 ? true : false;
        }

        public async Task<bool> DeletarPorTurma(int turmaId)
        {
            string sql = SQLScripts.SQL_DELETAR_RELACIONAMENTO_POR_TURMA;
            var param = new DynamicParameters();
            param.Add("@turmaId", turmaId);

            var result = await _connection.ExecuteAsync(sql: sql, param: param);
            return result > 0 ? true : false;
        }

        public async Task<bool> Inserir(InserirRelacionamentoCommand command)
        {
            string sql = SQLScripts.SQL_INSERIR_RELACIONAMENTO;

            var param = new DynamicParameters();
            param.Add("@alunoId", command.AlunoId);
            param.Add("@turmaId", command.TurmaId);

            var result = await _connection.ExecuteAsync(sql: sql, param: param);
            return result > 0 ? true : false;
        }

        public async Task<Relacionamento?> Obter(int alunoId, int turmaId)
        {
            string sql = SQLScripts.SQL_OBTER_RELACIONAMENTO;

            var param = new DynamicParameters();
            param.Add("@alunoId", alunoId);
            param.Add("@turmaId", turmaId);

            var result = await _connection.QueryFirstOrDefaultAsync<Relacionamento>(
                sql: sql,
                param: param);

            return result != null ? result : null;
        }

        public async Task<List<Relacionamento>> ObterLista()
        {
            string sql = SQLScripts.SQL_OBTER_LISTA_RELACIONAMENTO;
            var result = await _connection.QueryAsync<Relacionamento>(sql: sql);
            return result.ToList();
        }

        public async Task<List<Relacionamento>> ObterListaPorAluno(int alunoId)
        {
            string sql = SQLScripts.SQL_OBTER_LISTA_RELACIONAMENTO_POR_ALUNO;
            var param = new DynamicParameters();
            param.Add("@alunoId", alunoId);

            var result = await _connection.QueryAsync<Relacionamento>(sql: sql, param: param);
            return result.ToList();
        }

        public async Task<List<Relacionamento>> ObterListaPorTurma(int turmaId)
        {
            string sql = SQLScripts.SQL_OBTER_LISTA_RELACIONAMENTO_POR_TURMA;
            var param = new DynamicParameters();
            param.Add("@turmaId", turmaId);

            var result = await _connection.QueryAsync<Relacionamento>(sql: sql, param: param);
            return result.ToList();
        }
    }
}
