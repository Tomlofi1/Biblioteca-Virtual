using BibliotecaVirtual.DTO;
using BibliotecaVirtual.Models;

namespace BibliotecaVirtual.Interface
{
    public interface ILivrosRepository
    {
        ICollection<LivrosDTO> GetLivros();
        ICollection<LivrosDTO> GetLivrosPeloLancamento(DateTime dataInicial, DateTime dataFinal);
        ICollection<LivrosDTO> GetLivrosPeloGenero(string genero);
        ICollection<LivrosDTO> GetLivrosPeloNome(string nome);
        bool LivroExiste(int id);
        bool CreateLivro(Livros livros);
        bool UpdateLivro(int id, Livros livros);
        bool DeleteLivro(int id, Livros livros);
        bool Save();
    }
}
