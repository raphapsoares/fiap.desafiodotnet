using fiap.testedotnet.domain.Commands.Aluno;
using fiap.testedotnet.domain.Contracts.Repositories;
using Moq.AutoMock;
using Moq;
using fiap.testedotnet.application.Handlers;
using fiap.testedotnet.domain.Commands.Turma;
using fiap.testedotnet.domain.Entities;

namespace fiap.testedotnet.unittests.Handlers
{
    public class InserirTurmaHandlerTest
    {
        private readonly AutoMocker _mocker;
        private readonly InserirTurmaHandler _handler;

        public InserirTurmaHandlerTest()
        {
            _mocker = new AutoMocker();
            _handler = _mocker.CreateInstance<InserirTurmaHandler>();
        }

        [Fact]
        public async Task Handle_DeveRetornarTodosDadosInvalidos()
        {
            var command = new InserirTurmaCommand()
            {
                Ano = 0,
                CursoId = 0,
                Turma = ""
            };

            var resultado = await _handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.NotNull(resultado);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task Handle_DeveRetornarTurmaJaExiste()
        {
            var command = new InserirTurmaCommand()
            {
                Ano = 2024,
                CursoId = 1,
                Turma = "Curso Teste"
            };

            _mocker.GetMock<ITurmaRepository>().Setup(x => x.ObterPorNome(It.IsAny<string>())).ReturnsAsync(new Turma());

            var resultado = await _handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.NotNull(resultado);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task Handle_DeveRetornarFalhaAoInserirTurma()
        {
            var command = new InserirTurmaCommand()
            {
                Ano = 2024,
                CursoId = 1,
                Turma = "Curso Teste"
            };


            _mocker.GetMock<IAlunoRepository>().Setup(x => x.Inserir(It.IsAny<InserirAlunoCommand>())).ReturnsAsync(false);

            var resultado = await _handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.NotNull(resultado);
            Assert.False(resultado.Sucesso);
            Assert.Equal(1, resultado?.Mensagens?.Count);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucessoAoInserirAluno()
        {
            var command = new InserirTurmaCommand()
            {
                Ano = 2024,
                CursoId = 1,
                Turma = "Curso Teste"
            };
            _mocker.GetMock<ITurmaRepository>().Setup(x => x.Inserir(It.IsAny<InserirTurmaCommand>())).ReturnsAsync(true);

            var resultado = await _handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.NotNull(resultado);
            Assert.True(resultado.Sucesso);
        }
    }
}
