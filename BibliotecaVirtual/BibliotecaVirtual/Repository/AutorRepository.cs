using BibliotecaVirtual.Data;
using BibliotecaVirtual.Interface;
using BibliotecaVirtual.Models;

namespace BibliotecaVirtual.Repository
{
    public class AutorRepository : IAutorRepository
    {
        private readonly ContextoData _contexto;

        public AutorRepository(ContextoData contextoData)
        {
            _contexto = contextoData;
        }

        public bool AutorExiste(int id)
        {
            return _contexto.Autores.Any(o => o.Id == id);
        }

        public bool CreateAutor(Autor autor)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAutor(int id, Autor autor)
        {
            throw new NotImplementedException();
        }

        public ICollection<Autor> GetAutors()
        {
            return _contexto.Autores.OrderBy(p => p.Id).ToList();
        }

        public ICollection<Autor> GetAutorsPorLivro(Livros livros)
        {
           
        }

        public ICollection<Autor> GetAutorsPorNome(string PrimeiroNome)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            try
            {
                _contexto.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateAutor(int id, Autor autor)
        {
            throw new NotImplementedException();
        }
    }
}
