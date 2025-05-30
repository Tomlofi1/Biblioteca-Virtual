namespace BibliotecaVirtual.DTO
{
    public class Teste1
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataDeCriacao { get; set; } = DateTime.UtcNow;
    }
}
