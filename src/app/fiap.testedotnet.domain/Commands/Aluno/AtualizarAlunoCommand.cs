using fiap.testedotnet.domain.Extensions;
using fiap.testedotnet.domain.RequestResults;
using MediatR;

namespace fiap.testedotnet.domain.Commands.Aluno
{
    public class AtualizarAlunoCommand : AlunoBaseCommand, IRequest<RequestResult>
    {
        public int Id { get; set; }
        public bool Validate(ref List<string> validacoes)
        {
            if (validacoes == null)
                validacoes = new List<string>();

            if (Id <= 0)
                validacoes.Add("Id do usuário é inválido");

            if (Nome.IsNullOrEmptyOrWhiteSpace())
                validacoes.Add("Nome do usuário está inválido");

            if (Senha.IsNullOrEmptyOrWhiteSpace())
                validacoes.Add("Senha inválida");

            if (!Senha.IsPasswordValid())
                validacoes.Add("Necessário que a senha tenha no mínimo 8 caracteres, uma letra maiúscula, uma letra minúscula, um número e um caractere especial.");

            if (Usuario.IsNullOrEmptyOrWhiteSpace())
                validacoes.Add("Usuário inválido");

            return validacoes.Any();
        }
    }
}
