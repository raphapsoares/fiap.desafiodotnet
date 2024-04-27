namespace fiap.testedotnet.ui.Models.Turma
{
    public class AtualizarTurmaRequestModel
    {
        public int Id { get; set; }
        public int CursoId { get; set; }
        public string Turma { get; set; }
        public int Ano { get; set; }
    }
}
