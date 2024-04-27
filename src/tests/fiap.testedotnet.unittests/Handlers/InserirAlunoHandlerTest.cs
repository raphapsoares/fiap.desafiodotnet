using fiap.testedotnet.application.Handlers.Aluno;
using fiap.testedotnet.domain.Commands.Aluno;
using fiap.testedotnet.domain.Contracts.Repositories;
using Moq;
using Moq.AutoMock;

namespace fiap.testedotnet.unittests.Handlers
{
    public class InserirAlunoHandlerTest
    {
        private readonly AutoMocker _mocker;
        private readonly InserirAlunoHandler _handler;

        public InserirAlunoHandlerTest()
        {
            _mocker = new AutoMocker();
            _handler = _mocker.CreateInstance<InserirAlunoHandler>();
        }

        [Fact]
        public async Task Handle_DeveRetornarTodosDadosInvalidos()
        {
            var command = new InserirAlunoCommand()
            {
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
            var command = new InserirAlunoCommand()
            {
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
        public async Task Handle_DeveRetornarFalhaAoInserirAluno()
        {
            var command = new InserirAlunoCommand()
            {
                Nome = "Teste",
                Senha = "Teste@1234567890",
                Usuario = "Teste"
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
            var command = new InserirAlunoCommand()
            {
                Nome = "Teste",
                Senha = "Teste@1234567890",
                Usuario = "Teste"
            };


            _mocker.GetMock<IAlunoRepository>().Setup(x => x.Inserir(It.IsAny<InserirAlunoCommand>())).ReturnsAsync(true);

            var resultado = await _handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.NotNull(resultado);
            Assert.True(resultado.Sucesso);
        }

    }
}
