using fiap.testedotnet.application.Services;
using fiap.testedotnet.domain.Contracts.Repositories;
using fiap.testedotnet.domain.Contracts.Services;
using fiap.testedotnet.domain.Entities;
using Moq.AutoMock;
using Moq;

namespace fiap.testedotnet.unittests.Services
{
    public class TurmaServiceTest
    {
        private readonly AutoMocker _mocker;
        private readonly ITurmaService _turmaService;

        public TurmaServiceTest()
        {
            _mocker = new AutoMocker();
            _turmaService = _mocker.CreateInstance<TurmaService>();
        }

        [Fact]
        public async Task Obter_DeveRetornarNulo()
        {
            int turmaId = 1;

            var resultado = await _turmaService.Obter(turmaId);

            Assert.NotNull(resultado);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task Obter_DeveRetornarResultado()
        {
            int turmaId = 1;

            _mocker.GetMock<ITurmaRepository>().Setup(x => x.Obter(It.IsAny<int>())).ReturnsAsync(new Turma());

            var resultado = await _turmaService.Obter(turmaId);

            Assert.NotNull(resultado);
            Assert.True(resultado.Sucesso);

            _mocker.GetMock<ITurmaRepository>().Verify(x => x.Obter(It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public async Task ObterLista_DeveRetornarNulo()
        {
            var resultado = await _turmaService.ObterLista();

            Assert.NotNull(resultado);
            Assert.Null(resultado.Dados);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task ObterLista_DeveRetornarResultado()
        {
            var listaTurma = new List<Turma>();
            listaTurma.Add(new Turma());

            _mocker.GetMock<ITurmaRepository>().Setup(x => x.ObterLista()).ReturnsAsync(listaTurma);

            var resultado = await _turmaService.ObterLista();

            Assert.NotNull(resultado);
            Assert.NotNull(resultado.Dados);
            Assert.True(resultado.Sucesso);
        }

        [Fact]
        public async Task Deletar_DeveRetornarIdInvalido()
        {
            int turmaId = 0;

            var resultado = await _turmaService.Deletar(turmaId);

            Assert.NotNull(resultado);
            Assert.Null(resultado.Dados);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task Deletar_DeveRetornarAlunoNaoExiste()
        {
            int turmaId = 1;

            var resultado = await _turmaService.Deletar(turmaId);

            Assert.NotNull(resultado);
            Assert.Null(resultado.Dados);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task Deletar_DeveRetornarSucesso()
        {
            int turmaId = 1;

            _mocker.GetMock<ITurmaRepository>().Setup(x => x.Obter(It.IsAny<int>())).ReturnsAsync(new Turma());
            _mocker.GetMock<ITurmaRepository>().Setup(x => x.Deletar(It.IsAny<int>())).ReturnsAsync(true);

            var resultado = await _turmaService.Deletar(turmaId);

            Assert.NotNull(resultado);
            Assert.Null(resultado.Dados);
            Assert.True(resultado.Sucesso);
        }

        [Fact]
        public async Task Deletar_DeveRetornarFalha()
        {
            int turmaId = 1;

            _mocker.GetMock<ITurmaRepository>().Setup(x => x.Obter(It.IsAny<int>())).ReturnsAsync(new Turma());
            _mocker.GetMock<ITurmaRepository>().Setup(x => x.Deletar(It.IsAny<int>())).ReturnsAsync(false);

            var resultado = await _turmaService.Deletar(turmaId);

            Assert.NotNull(resultado);
            Assert.Null(resultado.Dados);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task Deletar_PossuiRelacionamentoAtivo_DeveRetornarSucesso()
        {
            int turmaId = 1;
            List<Relacionamento> listaRelacionamento = new List<Relacionamento>();
            listaRelacionamento.Add(new Relacionamento { AlunoId = 1, TurmaId = 1 });

            _mocker.GetMock<ITurmaRepository>().Setup(x => x.Obter(It.IsAny<int>())).ReturnsAsync(new Turma());
            _mocker.GetMock<ITurmaRepository>().Setup(x => x.Deletar(It.IsAny<int>())).ReturnsAsync(false);
            _mocker.GetMock<IRelacionamentoRepository>().Setup(x => x.ObterListaPorAluno(It.IsAny<int>())).ReturnsAsync(listaRelacionamento);
            _mocker.GetMock<IRelacionamentoRepository>().Setup(x => x.DeletarPorAluno(It.IsAny<int>())).ReturnsAsync(true);

            var resultado = await _turmaService.Deletar(turmaId);

            Assert.NotNull(resultado);
            Assert.Null(resultado.Dados);
            Assert.True(resultado.Sucesso);
        }
    }
}
