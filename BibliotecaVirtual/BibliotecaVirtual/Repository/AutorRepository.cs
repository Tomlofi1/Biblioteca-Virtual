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
            _contexto.Autores.Add(autor);
            return Save();
        }

        public bool DeleteAutor(int id, Autor autor)
        {
            _contexto.Autores.Remove(autor);
            return Save();
        }

        public ICollection<Autor> GetAutors()
        {
            return _contexto.Autores.OrderBy(p => p.Id).ToList();
        }

        public ICollection<Autor> GetAutorsPorGenero(string genero)
        {
            return _contexto.Livros.Where(x => x.Genero == genero)
                                   .Select(x => x.Autor)
                                   .Distinct()
                                   .OrderBy(a => a.PrimeiroNome)
                                   .ToList();
        }

        public ICollection<Autor> GetAutorsPorLivro(Livros livros)
        {
            return _contexto.Autores.Where(a => a.Id == livros.AutorId).ToList();
        }

        public ICollection<Autor> GetAutorsPorNome(string PrimeiroNome)
        {
            return _contexto.Autores.Where(a => a.PrimeiroNome == PrimeiroNome).OrderBy(a => a.PrimeiroNome).ToList();
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
            _contexto.Autores.Update(autor);
            return Save();
        }
    }
}
