namespace BibliotecaVirtual.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string PrimeiroNome { get; set; }
        public string SegundoNome { get; set; }
        public ICollection<Livros> Livros { get; set; }
    }
}
