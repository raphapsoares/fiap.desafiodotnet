using System.ComponentModel.DataAnnotations;

namespace fiap.testedotnet.domain.Entities
{
    public class Aluno
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Usuario { get; set; }
        public string? Senha { get; set; }
    }
}
