using fiap.testedotnet.api.Configurations;
using fiap.testedotnet.domain.Commands.Relacionamento;
using fiap.testedotnet.domain.Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace fiap.testedotnet.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelacionamentoController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Incluir(
            [FromBody] InserirRelacionamentoCommand command,
            [FromServices] IMediator mediator)
        {
            var resultado = await mediator.Send(command);
            return resultado.CustomResponse();
        }

        [HttpGet("aluno/{alunoId}/turma/{turmaId}")]
        public async Task<IActionResult> Obter(
            [FromServices] IRelacionamentoService relacionamentoService,
            [FromRoute] int alunoId,
            [FromRoute] int turmaId)
        {
            var resultado = await relacionamentoService.Obter(alunoId, turmaId);
            return resultado.CustomResponse();
        }

        [HttpGet("")]
        public async Task<IActionResult> ObterLista(
            [FromServices] IRelacionamentoService relacionamentoService)
        {
            var resultado = await relacionamentoService.ObterLista();
            return resultado.CustomResponse();
        }

        [HttpDelete("aluno/{alunoId}/turma/{turmaId}")]
        public async Task<IActionResult> Deletar(
            [FromServices] IRelacionamentoService relacionamentoService,
            [FromRoute] int alunoId,
            [FromRoute] int turmaId)
        {
            var resultado = await relacionamentoService.Deletar(alunoId, turmaId);
            return resultado.CustomResponse();
        }
    }
}
