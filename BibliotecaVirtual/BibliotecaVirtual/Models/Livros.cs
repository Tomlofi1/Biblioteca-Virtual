namespace BibliotecaVirtual.Models
{
    public class Livros
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public DateTime LancamentoDoLivro { get; set; }
        public ICollection<ClienteLivro> ClienteLivros { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
    }
}
