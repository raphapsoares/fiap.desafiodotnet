using fiap.testedotnet.api.Configurations;
using fiap.testedotnet.domain.Commands.Aluno;
using fiap.testedotnet.domain.Contracts.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace fiap.testedotnet.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Incluir(
            [FromBody] InserirAlunoCommand command,
            [FromServices] IMediator mediator)
        {
            var resultado = await mediator.Send(command);
            return resultado.CustomResponse();
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar(
            [FromBody] AtualizarAlunoCommand command,
            [FromServices] IMediator mediator)
        {
            var resultado = await mediator.Send(command);
            return resultado.CustomResponse();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter(
            [FromServices] IAlunoService alunoService,
            [FromRoute] int id)
        {
            var resultado = await alunoService.Obter(id);
            return resultado.CustomResponse();
        }

        [HttpGet("")]
        public async Task<IActionResult> ObterLista([FromServices] IAlunoService alunoService)
        {
            var resultado = await alunoService.ObterLista();
            return resultado.CustomResponse();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(
            [FromServices] IAlunoService alunoService,
            [FromRoute] int id)
        {
            var resultado = await alunoService.Deletar(id);
            return resultado.CustomResponse();
        }
    }
}
