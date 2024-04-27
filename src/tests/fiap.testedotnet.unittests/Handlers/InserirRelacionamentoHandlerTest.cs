using fiap.testedotnet.application.Handlers.Relacionamento;
using fiap.testedotnet.domain.Commands.Relacionamento;
using fiap.testedotnet.domain.Contracts.Repositories;
using fiap.testedotnet.domain.Entities;
using Moq;
using Moq.AutoMock;

namespace fiap.testedotnet.unittests.Handlers
{
    public class InserirRelacionamentoHandlerTest
    {
        private readonly AutoMocker _mocker;
        private readonly InserirRelacionamentoHandler _handler;

        public InserirRelacionamentoHandlerTest()
        {
            _mocker = new AutoMocker();
            _handler = _mocker.CreateInstance<InserirRelacionamentoHandler>();

        }

        [Fact]
        public async Task Handle_DeveRetornarTodosDadosInvalidos()
        {
            var command = new InserirRelacionamentoCommand()
            {
                AlunoId = 0,
                TurmaId = 0
            };

            var resultado = await _handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.NotNull(resultado);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task Handle_DeveRetornarAlunoTurmaNaoExistem()
        {
            var command = new InserirRelacionamentoCommand()
            {
                AlunoId = 1,
                TurmaId = 1
            };

            var resultado = await _handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.NotNull(resultado);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task Handle_DeveRetornarTurmaNaoExiste()
        {
            var command = new InserirRelacionamentoCommand()
            {
                AlunoId = 1,
                TurmaId = 1
            };

            _mocker.GetMock<IAlunoRepository>().Setup(x => x.Obter(It.IsAny<int>())).ReturnsAsync(new Aluno());

            var resultado = await _handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.NotNull(resultado);
            Assert.False(resultado.Sucesso);
        }


        [Fact]
        public async Task Handle_DeveRetornarAlunoNaoExiste()
        {
            var command = new InserirRelacionamentoCommand()
            {
                AlunoId = 1,
                TurmaId = 1
            };

            _mocker.GetMock<ITurmaRepository>().Setup(x => x.Obter(It.IsAny<int>())).ReturnsAsync(new Turma());

            var resultado = await _handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.NotNull(resultado);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task Handle_DeveRetornarRelacionamentoJaExiste()
        {
            var command = new InserirRelacionamentoCommand()
            {
                AlunoId = 1,
                TurmaId = 1
            };

            _mocker.GetMock<IAlunoRepository>().Setup(x => x.Obter(It.IsAny<int>())).ReturnsAsync(new Aluno());
            _mocker.GetMock<ITurmaRepository>().Setup(x => x.Obter(It.IsAny<int>())).ReturnsAsync(new Turma());

            var resultado = await _handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.NotNull(resultado);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task Handle_DeveRetornarFalhaAoInserirRelacionamento()
        {
            var command = new InserirRelacionamentoCommand()
            {
                AlunoId = 1,
                TurmaId = 1
            };

            _mocker.GetMock<IAlunoRepository>().Setup(x => x.Obter(It.IsAny<int>())).ReturnsAsync(new Aluno());
            _mocker.GetMock<ITurmaRepository>().Setup(x => x.Obter(It.IsAny<int>())).ReturnsAsync(new Turma());
            _mocker.GetMock<IRelacionamentoRepository>().Setup(x => x.Obter(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new Relacionamento());
            _mocker.GetMock<IRelacionamentoRepository>().Setup(x => x.Inserir(It.IsAny<InserirRelacionamentoCommand>())).ReturnsAsync(false);

            var resultado = await _handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.NotNull(resultado);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task Handle_DeveRetornarSucessoAoInserirRelacionamento()
        {
            var command = new InserirRelacionamentoCommand()
            {
                AlunoId = 1,
                TurmaId = 1
            };

            _mocker.GetMock<IAlunoRepository>().Setup(x => x.Obter(It.IsAny<int>())).ReturnsAsync(new Aluno());
            _mocker.GetMock<ITurmaRepository>().Setup(x => x.Obter(It.IsAny<int>())).ReturnsAsync(new Turma());
            _mocker.GetMock<IRelacionamentoRepository>().Setup(x => x.Inserir(It.IsAny<InserirRelacionamentoCommand>())).ReturnsAsync(true);

            var resultado = await _handler.Handle(command, It.IsAny<CancellationToken>());

            Assert.NotNull(resultado);
            Assert.True(resultado.Sucesso);
        }
    }
}
