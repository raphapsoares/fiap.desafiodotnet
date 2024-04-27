using System.ComponentModel.DataAnnotations;

namespace fiap.testedotnet.domain.Entities
{
    public class Turma
    {
        [Key]
        public int Id { get; set; }
        public int CursoId { get; set; }
        public string? Descricao { get; set; }
        public int Ano { get; set; }
    }
}
