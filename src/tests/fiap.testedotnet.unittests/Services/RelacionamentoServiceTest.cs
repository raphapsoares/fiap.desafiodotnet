using fiap.testedotnet.application.Services;
using fiap.testedotnet.domain.Contracts.Repositories;
using fiap.testedotnet.domain.Contracts.Services;
using fiap.testedotnet.domain.Entities;
using Moq.AutoMock;
using Moq;

namespace fiap.testedotnet.unittests.Services
{
    public class RelacionamentoServiceTest
    {
        private readonly AutoMocker _mocker;
        private readonly IRelacionamentoService _relacionamentoService;

        public RelacionamentoServiceTest()
        {
            _mocker = new AutoMocker();
            _relacionamentoService = _mocker.CreateInstance<RelacionamentoService>();
        }

        [Fact]
        public async Task Obter_DeveRetornarNulo()
        {
            int alunoId = 1, turmaId = 1;

            var resultado = await _relacionamentoService.Obter(alunoId, turmaId);

            Assert.NotNull(resultado);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task Obter_DeveRetornarResultado()
        {
            int alunoId = 1, turmaId = 1;

            _mocker.GetMock<IRelacionamentoRepository>().Setup(x => x.Obter(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new Relacionamento());

            var resultado = await _relacionamentoService.Obter(alunoId, turmaId);

            Assert.NotNull(resultado);
            Assert.True(resultado.Sucesso);

            _mocker.GetMock<IRelacionamentoRepository>().Verify(x => x.Obter(It.IsAny<int>(), It.IsAny<int>()), Times.Once);

        }

        [Fact]
        public async Task ObterLista_DeveRetornarNulo()
        {
            var resultado = await _relacionamentoService.ObterLista();

            Assert.NotNull(resultado);
            Assert.Null(resultado.Dados);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task ObterLista_DeveRetornarResultado()
        {
            var listaRelacionamento = new List<Relacionamento>();
            listaRelacionamento.Add(new Relacionamento());

            _mocker.GetMock<IRelacionamentoRepository>().Setup(x => x.ObterLista()).ReturnsAsync(listaRelacionamento);

            var resultado = await _relacionamentoService.ObterLista();

            Assert.NotNull(resultado);
            Assert.NotNull(resultado.Dados);
            Assert.True(resultado.Sucesso);
        }

        [Fact]
        public async Task Deletar_DeveRetornarIdInvalido()
        {
            int alunoId = 0, turmaId = 0;

            var resultado = await _relacionamentoService.Deletar(alunoId, turmaId);

            Assert.NotNull(resultado);
            Assert.Null(resultado.Dados);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task Deletar_DeveRetornarAlunoNaoExiste()
        {
            int alunoId = 1, turmaId = 0;

            var resultado = await _relacionamentoService.Deletar(alunoId, turmaId);

            Assert.NotNull(resultado);
            Assert.Null(resultado.Dados);
            Assert.False(resultado.Sucesso);
        }

        [Fact]
        public async Task Deletar_DeveRetornarSucesso()
        {
            int alunoId = 1, turmaId = 1;

            _mocker.GetMock<IRelacionamentoRepository>().Setup(x => x.Obter(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new Relacionamento());
            _mocker.GetMock<IRelacionamentoRepository>().Setup(x => x.Deletar(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(true);

            var resultado = await _relacionamentoService.Deletar(alunoId, turmaId);

            Assert.NotNull(resultado);
            Assert.Null(resultado.Dados);
            Assert.True(resultado.Sucesso);
        }

        [Fact]
        public async Task Deletar_DeveRetornarFalha()
        {
            int alunoId = 1, turmaId = 1;

            _mocker.GetMock<IRelacionamentoRepository>().Setup(x => x.Obter(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new Relacionamento());
            _mocker.GetMock<IRelacionamentoRepository>().Setup(x => x.Deletar(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(false);

            var resultado = await _relacionamentoService.Deletar(alunoId, turmaId);

            Assert.NotNull(resultado);
            Assert.Null(resultado.Dados);
            Assert.False(resultado.Sucesso);
        }
    }
}
