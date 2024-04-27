using fiap.testedotnet.domain.Extensions;

namespace fiap.testedotnet.domain.Commands.Turma
{
    public class AtualizarTurmaCommand : TurmaBaseCommand
    {
        public int Id { get; set; }

        public bool Validate(ref List<string> validacoes)
        {
            if (Id <= 0)
                validacoes.Add("Id da turma é inválido");

            if (validacoes == null)
                validacoes = new List<string>();

            if (CursoId <= 0)
                validacoes.Add("Id do curso está inválido");

            if (Turma.IsNullOrEmptyOrWhiteSpace())
                validacoes.Add("Nome da turma está inválido");

            if (Ano < DateTime.Now.Year)
                validacoes.Add("O ano de cadastro não pode ser menor que o atual");

            return validacoes.Any();
        }
    }
}
