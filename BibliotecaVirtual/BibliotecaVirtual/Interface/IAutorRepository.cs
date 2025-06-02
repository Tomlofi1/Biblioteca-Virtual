using BibliotecaVirtual.DTO;
using BibliotecaVirtual.Models;

namespace BibliotecaVirtual.Interface
{
    public interface IAutorRepository
    {
        ICollection<Autor> GetAutors();
        ICollection<Autor> GetAutorsPorLivro(Livros livros);
        ICollection<Autor> GetAutorsPorNome(string PrimeiroNome);
        ICollection<Autor> GetAutorsPorGenero(string genero);
        bool AutorExiste(int id);
        bool CreateAutor(Autor autor);
        bool DeleteAutor(int id, Autor autor);
        bool UpdateAutor(int id, Autor autor);        
        bool Save();


    }
}
