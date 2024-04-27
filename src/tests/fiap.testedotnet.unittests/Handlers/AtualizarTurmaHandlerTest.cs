using fiap.testedotnet.domain.Commands.Turma;
using fiap.testedotnet.domain.Contracts.Repositories;
using fiap.testedotnet.domain.Entities;
using Moq.AutoMock;
using Moq;
using fiap.testedotnet.application.Handlers.Turma;

namespace fiap.testedotnet.unittests.Handlers
{
    public class AtualizarTurmaHandlerTest
    {
        private readonly AutoMocker _mocker;
        private readonly AtualizarTurmaHandler _handler;

        public AtualizarTurmaHandlerTest()
        {
            _mocker = new AutoMocker();
            _handler = _mocker.CreateInstance<AtualizarTurmaHandler>();
        }

        [Fact]
        public async Task Handle_DeveRetornarTodosDadosInvalidos()
        {
            var command = new AtualizarTurmaCommand()
            {
                Id = 0,
                Ano = 0,
                CursoId = 0,
                Turma = ""
            };

            var resultado = await _handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.NotNull(resultado);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task Handle_DeveRetornarTurmaNaoExiste()
        {
            var command = new AtualizarTurmaCommand()
            {
                Id = 1,
                Ano = 2024,
                CursoId = 1,
                Turma = "Curso Teste"
            };

            var resultado = await _handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.NotNull(resultado);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task Handle_DeveRetornarTurmaJaExiste()
        {
            var command = new AtualizarTurmaCommand()
            {
                Id = 1,
                Ano = 2024,
                CursoId = 1,
                Turma = "Curso Teste"
            };

            _mocker.GetMock<ITurmaRepository>().Setup(x => x.Obter(It.IsAny<int>())).ReturnsAsync(new Turma());
            _mocker.GetMock<ITurmaRepository>().Setup(x => x.ObterPorNome(It.IsAny<string>())).ReturnsAsync(new Turma());

            var resultado = await _handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.NotNull(resultado);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task Handle_DeveRetornarFalhaAoInserirTurma()
        {
            var command = new AtualizarTurmaCommand()
            {
                Id = 1,
                Ano = 2024,
                CursoId = 1,
                Turma = "Curso Teste"
            };

            _mocker.GetMock<ITurmaRepository>().Setup(x => x.Obter(It.IsAny<int>())).ReturnsAsync(new Turma());
            _mocker.GetMock<ITurmaRepository>().Setup(x => x.Atualizar(It.IsAny<AtualizarTurmaCommand>())).ReturnsAsync(false);

            var resultado = await _handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.NotNull(resultado);
            Assert.False(resultado.Sucesso);
            Assert.Equal(1, resultado?.Mensagens?.Count);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucessoAoInserirAluno()
        {
            var command = new AtualizarTurmaCommand()
            {
                Id = 1,
                Ano = 2024,
                CursoId = 1,
                Turma = "Curso Teste"
            };

            _mocker.GetMock<ITurmaRepository>().Setup(x => x.Obter(It.IsAny<int>())).ReturnsAsync(new Turma());
            _mocker.GetMock<ITurmaRepository>().Setup(x => x.Atualizar(It.IsAny<AtualizarTurmaCommand>())).ReturnsAsync(true);

            var resultado = await _handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.NotNull(resultado);
            Assert.True(resultado.Sucesso);
        }
    }
}
