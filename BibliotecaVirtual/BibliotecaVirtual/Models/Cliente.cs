namespace BibliotecaVirtual.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string PrimeiroNome { get; set; }
        public string SegundoNome { get; set; }
        public DateTime DataDeNascimento { get; set; }
        public ICollection<ClienteLivro> ClienteLivros { get; set; }
    }
}
