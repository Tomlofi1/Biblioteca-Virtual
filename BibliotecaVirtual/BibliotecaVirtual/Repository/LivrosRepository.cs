using BibliotecaVirtual.Data;
using BibliotecaVirtual.DTO;
using BibliotecaVirtual.Interface;
using BibliotecaVirtual.Models;

namespace BibliotecaVirtual.Repository
{
    public class LivrosRepository : ILivrosRepository
    {
        private readonly ContextoData _contexto;
        public LivrosRepository(ContextoData contextoData) 
        {
            _contexto = contextoData;
        }

        public bool CreateLivro(Livros livros)
        {
            _contexto.Add(livros);
            return Save();
        }

        public bool DeleteLivro(int id, Livros livros)
        {
            _contexto.Remove(livros);
            return Save();
        }
        public bool LivroExiste(int id)
        {
            return _contexto.Livros.Any(l => l.Id == id);
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

        public bool UpdateLivro(int id, Livros livros)
        {
            var livroExistente = _contexto.Livros.Find(id);

            if (livroExistente == null)
            {
                
                return false;
            }
            livroExistente.Titulo = livros.Titulo?.Trim();
            livroExistente.Genero = livros.Genero?.Trim();
            livroExistente.LancamentoDoLivro = livros.LancamentoDoLivro;
            
            return Save();
        }

        ICollection<LivrosDTO> ILivrosRepository.GetLivros()
        {
            return _contexto.Livros
                .Select(livros => new LivrosDTO
                {
                    Id = livros.Id,
                    Titulo = livros.Titulo!,
                    Genero = livros.Genero!,
                    LancamentoDoLivro = livros.LancamentoDoLivro,
                })
                .OrderBy(l => l.Id)
                .ToList();
        }

        ICollection<LivrosDTO> ILivrosRepository.GetLivrosPeloGenero(string genero)
        {
            return _contexto.Livros
                .Select(livros => new LivrosDTO
                {
                    Titulo = livros.Titulo!,
                    Genero = livros.Genero!,
                })
                .Where(p => p.Genero.ToLower() == genero.ToLower()).ToList();
        }

        ICollection<LivrosDTO> ILivrosRepository.GetLivrosPeloLancamento(DateTime dataInicial, DateTime dataFinal)
        {
            return _contexto.Livros
                .Select(livros => new LivrosDTO
                {
                    Id = livros.Id,
                    Titulo = livros.Titulo!,
                    Genero = livros.Genero!,
                    LancamentoDoLivro = livros.LancamentoDoLivro,
                })
                .Where(p => p.LancamentoDoLivro >= dataInicial && p.LancamentoDoLivro <= dataFinal).ToList();
        }

        ICollection<LivrosDTO> ILivrosRepository.GetLivrosPeloNome(string nome)
        {
            return _contexto.Livros
                .Select(livros => new LivrosDTO
                {
                    Titulo=livros.Titulo!,
                    Genero =livros.Genero!,
                })
                .Where(o => o.Titulo.ToLower() == nome.ToLower()).ToList();
        }
    }
}
