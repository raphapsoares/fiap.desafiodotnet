using fiap.testedotnet.api.Configurations;
using fiap.testedotnet.domain.Commands.Turma;
using fiap.testedotnet.domain.Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace fiap.testedotnet.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TurmaController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Incluir(
            [FromBody] InserirTurmaCommand command,
            [FromServices] IMediator mediator)
        {
            var resultado = await mediator.Send(command);
            return resultado.CustomResponse();
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(
            [FromBody] AtualizarTurmaCommand command,
            [FromServices] IMediator mediator)
        {
            var resultado = await mediator.Send(command);
            return resultado.CustomResponse();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(
            [FromServices] ITurmaService turmaService,
            [FromRoute] int id)
        {
            var resultado = await turmaService.Obter(id);
            return resultado.CustomResponse();
        }

        [HttpGet("")]
        public async Task<IActionResult> ObterLista([FromServices] ITurmaService turmaService)
        {
            var resultado = await turmaService.ObterLista();
            return resultado.CustomResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(
            [FromServices] ITurmaService turmaService,
            [FromRoute] int id)
        {
            var resultado = await turmaService.Deletar(id);
            return resultado.CustomResponse();
        }
    }
}
