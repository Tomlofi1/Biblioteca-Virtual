namespace BibliotecaVirtual.Models
{
    public class ClienteLivro
    {
        public int LivroId { get; set; }
        public int ClienteId { get; set; }
        public Livros Livros { get; set; }
        public Cliente Clientes { get; set; }

    }
}
