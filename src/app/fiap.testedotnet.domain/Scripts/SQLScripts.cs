namespace fiap.testedotnet.domain.Scripts
{
    public static class SQLScripts
    {
        #region TABELAS

        public const string SQL_TABELA_ALUNO = @"
        CREATE TABLE aluno (
		id int identity primary key,
		nome varchar(255),
		usuario varchar(255),
		senha char(60))";

        public const string SQL_TABELA_TURMA = @"
	    CREATE TABLE turma (
		id int identity primary key,
		curso_id int,
		turma varchar(45),
		ano int)";

        public const string SQL_TABELA_RELACIONAMENTO = @"
	    CREATE TABLE aluno_turma (
		aluno_id int foreign key references aluno,
		turma_id int foreign key references turma)";

        #endregion TABELAS

        #region ALUNO

        public const string SQL_OBTER_ALUNO = @"
        SELECT 
        nome as Nome 
        ,usuario as Usuario
        FROM dbo.aluno 
        WHERE id = @id";

        public const string SQL_OBTER_LISTA_ALUNO = @"SELECT 
                         id AS Id,
                         nome as Nome,
                         usuario as Usuario
                         FROM aluno";

        public const string SQL_INSERIR_ALUNO = @"
        INSERT INTO dbo.aluno 
        VALUES (@nome, @usuario, @senha)";

        public const string SQL_DELETAR_ALUNO = @"
        DELETE FROM dbo.aluno 
        WHERE id = @id";

        public const string SQL_ATUALIZAR_ALUNO = @"
        UPDATE dbo.aluno 
        SET nome = @nome
        ,usuario = @usuario
        ,senha = @senha
        WHERE id = @id";

        #endregion ALUNO

        #region TURMA

        public const string SQL_OBTER_LISTA_TURMA = @"
        SELECT 
        id AS Id, 
        curso_id AS CursoId, 
        turma AS Descricao, 
        ano AS Ano
        FROM dbo.turma";

        public const string SQL_OBTER_TURMA = @"
        SELECT id AS Id, 
        curso_id AS CursoId, 
        turma AS Descricao, 
        ano AS Ano
        FROM dbo.turma
        WHERE id = @id";

        public const string SQL_INSERIR_TURMA = @"
        INSERT INTO dbo.turma
        VALUES (@cursoId, @turma, @ano)";

        public const string SQL_DELETAR_TURMA = @"
        DELETE FROM dbo.turma
        WHERE id = @id";

        public const string SQL_ATUALIZAR_TURMA = @"
        UPDATE dbo.turma 
        SET curso_id = @cursoId, 
        turma = @turma, 
        ano = @ano 
        WHERE id = @id";

        public const string SQL_OBTER_TURMA_POR_NOME = @"
        SELECT id AS Id, 
        curso_id AS CursoId, 
        turma AS Turma, 
        ano AS Ano
        FROM dbo.turma
        WHERE LOWER(turma) = @turma";

        #endregion TURMA

        #region RELACIONAMENTO

        public const string SQL_INSERIR_RELACIONAMENTO = @"
        INSERT INTO dbo.aluno_turma 
        VALUES (@alunoId, @turmaId)";

        public const string SQL_DELETAR_RELACIONAMENTO = @"
        DELETE FROM dbo.aluno_turma 
        WHERE aluno_id = @alunoId 
        AND turma_id = @turmaId";

        public const string SQL_DELETAR_RELACIONAMENTO_POR_TURMA = @"
        DELETE FROM dbo.aluno_turma 
        WHERE turma_id = @turmaId";

        public const string SQL_DELETAR_RELACIONAMENTO_POR_ALUNO = @"
        DELETE FROM dbo.aluno_turma 
        WHERE aluno_id = @alunoId";

        public const string SQL_OBTER_RELACIONAMENTO = @"
        SELECT 
        aluno_id AS AlunoId,
        turma_id AS TurmaId,
        nome AS Aluno,
        turma AS Turma
        FROM dbo.aluno_turma
	    INNER JOIN dbo.aluno ON dbo.aluno.id = aluno_turma.aluno_id
	    INNER JOIN dbo.turma ON dbo.turma.id = aluno_turma.turma_id
        WHERE aluno_id = @alunoId 
        AND turma_id = @turmaId";

        public const string SQL_OBTER_LISTA_RELACIONAMENTO = @"
        SELECT 
        aluno_id AS AlunoId,
        turma_id AS TurmaId,
        nome AS Aluno,
        turma AS Turma
        FROM dbo.aluno_turma
	    INNER JOIN dbo.aluno ON dbo.aluno.id = aluno_turma.aluno_id
	    INNER JOIN dbo.turma ON dbo.turma.id = aluno_turma.turma_id";

        public const string SQL_OBTER_LISTA_RELACIONAMENTO_POR_ALUNO = @"
        SELECT 
        aluno_id AS AlunoId,
        turma_id AS TurmaId,
        nome AS Aluno,
        turma AS Turma
        FROM dbo.aluno_turma
	    INNER JOIN dbo.aluno ON dbo.aluno.id = aluno_turma.aluno_id
	    INNER JOIN dbo.turma ON dbo.turma.id = aluno_turma.turma_id
        WHERE aluno_id = @alunoId";

        public const string SQL_OBTER_LISTA_RELACIONAMENTO_POR_TURMA = @"
        SELECT 
        aluno_id AS AlunoId,
        turma_id AS TurmaId,
        nome AS Aluno,
        turma AS Turma
        FROM dbo.aluno_turma
	    INNER JOIN dbo.aluno ON dbo.aluno.id = aluno_turma.aluno_id
	    INNER JOIN dbo.turma ON dbo.turma.id = aluno_turma.turma_id
        WHERE turma_id = @turmaId";

        #endregion RELACIONAMENTO
    }
}
