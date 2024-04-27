using fiap.testedotnet.application.Handlers.Aluno;
using fiap.testedotnet.domain.Commands.Aluno;
using fiap.testedotnet.domain.Contracts.Repositories;
using Moq;
using Moq.AutoMock;

namespace fiap.testedotnet.unittests.Handlers
{
    public class AtualizarAlunoHandlerTest
    {
        private readonly AutoMocker _mocker;
        private readonly AtualizarAlunoHandler _handler;

        public AtualizarAlunoHandlerTest()
        {
            _mocker = new AutoMocker();
            _handler = _mocker.CreateInstance<AtualizarAlunoHandler>();
        }

        [Fact]
        public async Task Handle_DeveRetornarTodosDadosInvalidos()
        {
            var command = new AtualizarAlunoCommand()
            {
                Id = 0,
                Nome = "",
                Senha = "",
                Usuario = ""
            };

            var resultado = await _handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.NotNull(resultado);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task Handle_DeveRetornarSenhaFraca()
        {
            var command = new AtualizarAlunoCommand()
            {
                Id = 1,
                Nome = "Teste",
                Senha = "12345",
                Usuario = "Teste"
            };

            var resultado = await _handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.NotNull(resultado);
            Assert.False(resultado.Sucesso);
            Assert.Equal(1, resultado?.Mensagens?.Count);
        }

        [Fact]
        public async Task Handle_DeveRetornarFalhaAoAtualizarAluno()
        {
            var command = new AtualizarAlunoCommand()
            {
                Id = 1,
                Nome = "Teste",
                Senha = "Teste@1234567890",
                Usuario = "Teste"
            };


            _mocker.GetMock<IAlunoRepository>().Setup(x => x.Atualizar(It.IsAny<AtualizarAlunoCommand>())).ReturnsAsync(false);

            var resultado = await _handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.NotNull(resultado);
            Assert.False(resultado.Sucesso);
            Assert.Equal(1, resultado?.Mensagens?.Count);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucessoAoAtualizarAluno()
        {
            var command = new AtualizarAlunoCommand()
            {
                Id = 1,
                Nome = "Teste",
                Senha = "Teste@1234567890",
                Usuario = "Teste"
            };


            _mocker.GetMock<IAlunoRepository>().Setup(x => x.Atualizar(It.IsAny<AtualizarAlunoCommand>())).ReturnsAsync(true);

            var resultado = await _handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.NotNull(resultado);
            Assert.True(resultado.Sucesso);
            Assert.Equal(1, resultado?.Mensagens?.Count);
        }
    }
}
