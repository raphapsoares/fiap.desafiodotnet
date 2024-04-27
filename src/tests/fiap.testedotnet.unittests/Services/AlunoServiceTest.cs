using fiap.testedotnet.application.Services;
using fiap.testedotnet.domain.Contracts.Repositories;
using fiap.testedotnet.domain.Contracts.Services;
using fiap.testedotnet.domain.Entities;
using Moq;
using Moq.AutoMock;

namespace fiap.testedotnet.unittests.Services
{
    public class AlunoServiceTest
    {
        private readonly AutoMocker _mocker;
        private readonly IAlunoService _alunoService;

        public AlunoServiceTest()
        {
            _mocker = new AutoMocker();
            _alunoService = _mocker.CreateInstance<AlunoService>();
        }

        [Fact]
        public async Task Obter_DeveRetornarNulo()
        {
            int alunoId = 1;

            var resultado = await _alunoService.Obter(alunoId);

            Assert.NotNull(resultado);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task Obter_DeveRetornarResultado()
        {
            int alunoId = 1;

            _mocker.GetMock<IAlunoRepository>().Setup(x => x.Obter(It.IsAny<int>())).ReturnsAsync(new Aluno());

            var resultado = await _alunoService.Obter(alunoId);

            Assert.NotNull(resultado);
            Assert.True(resultado.Sucesso);

            _mocker.GetMock<IAlunoRepository>().Verify(x => x.Obter(It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public async Task ObterLista_DeveRetornarNulo()
        {
            var resultado = await _alunoService.ObterLista();

            Assert.NotNull(resultado);
            Assert.Null(resultado.Dados);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task ObterLista_DeveRetornarResultado()
        {
            var listaAluno = new List<Aluno>();
            listaAluno.Add(new Aluno());

            _mocker.GetMock<IAlunoRepository>().Setup(x => x.ObterLista()).ReturnsAsync(listaAluno);

            var resultado = await _alunoService.ObterLista();

            Assert.NotNull(resultado);
            Assert.NotNull(resultado.Dados);
            Assert.True(resultado.Sucesso);
        }

        [Fact]
        public async Task Deletar_DeveRetornarIdInvalido()
        {
            int alunoId = 0;

            var resultado = await _alunoService.Deletar(alunoId);

            Assert.NotNull(resultado);
            Assert.Null(resultado.Dados);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task Deletar_DeveRetornarAlunoNaoExiste()
        {
            int alunoId = 1;

            var resultado = await _alunoService.Deletar(alunoId);

            Assert.NotNull(resultado);
            Assert.Null(resultado.Dados);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task Deletar_DeveRetornarSucesso()
        {
            int alunoId = 1;

            _mocker.GetMock<IAlunoRepository>().Setup(x => x.Obter(It.IsAny<int>())).ReturnsAsync(new Aluno());
            _mocker.GetMock<IAlunoRepository>().Setup(x => x.Deletar(It.IsAny<int>())).ReturnsAsync(true);

            var resultado = await _alunoService.Deletar(alunoId);

            Assert.NotNull(resultado);
            Assert.Null(resultado.Dados);
            Assert.True(resultado.Sucesso);
        }

        [Fact]
        public async Task Deletar_DeveRetornarFalha()
        {
            int alunoId = 1;

            _mocker.GetMock<IAlunoRepository>().Setup(x => x.Obter(It.IsAny<int>())).ReturnsAsync(new Aluno());
            _mocker.GetMock<IAlunoRepository>().Setup(x => x.Deletar(It.IsAny<int>())).ReturnsAsync(false);

            var resultado = await _alunoService.Deletar(alunoId);

            Assert.NotNull(resultado);
            Assert.Null(resultado.Dados);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task Deletar_PossuiRelacionamentoAtivo_DeveRetornarSucesso()
        {
            int alunoId = 1;
            List<Relacionamento> listaRelacionamento = new List<Relacionamento>();
            listaRelacionamento.Add(new Relacionamento { AlunoId = 1, TurmaId = 1 });

            _mocker.GetMock<IAlunoRepository>().Setup(x => x.Obter(It.IsAny<int>())).ReturnsAsync(new Aluno());
            _mocker.GetMock<IAlunoRepository>().Setup(x => x.Deletar(It.IsAny<int>())).ReturnsAsync(true);
            _mocker.GetMock<IRelacionamentoRepository>().Setup(x => x.ObterListaPorAluno(It.IsAny<int>())).ReturnsAsync(listaRelacionamento);
            _mocker.GetMock<IRelacionamentoRepository>().Setup(x => x.DeletarPorAluno(It.IsAny<int>())).ReturnsAsync(true);

            var resultado = await _alunoService.Deletar(alunoId);

            Assert.NotNull(resultado);
            Assert.Null(resultado.Dados);
            Assert.True(resultado.Sucesso);
        }
    }
}
