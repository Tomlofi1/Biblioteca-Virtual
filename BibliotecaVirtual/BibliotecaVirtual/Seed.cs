using BibliotecaVirtual.Data;
using BibliotecaVirtual.Models;

namespace BibliotecaVirtual
{
    public class Seed
    {
        private readonly ContextoData contextoData;

        public Seed(ContextoData contextoData)
        {
            this.contextoData = contextoData;
        }

        public void SeedContextoData()
        {
            if (!contextoData.ClienteLivros.Any())
            {
                var autores = new List<Autor>
                {
                    new Autor
                    {
                        PrimeiroNome = "William",
                        SegundoNome = "Gibson"
                    },
                    new Autor
                    {
                        PrimeiroNome = "Sun",
                        SegundoNome = "Tzu"
                    },
                    new Autor
                    {
                        PrimeiroNome = "Thomas",
                        SegundoNome = "Edison"
                    },
                };
                contextoData.Autores.AddRange(autores);
                contextoData.SaveChanges();
                
                var livros = new List<Livros>
                {
                    new Livros
                    {
                        Titulo = "Neuromancer",
                        LancamentoDoLivro = DateTime.SpecifyKind(new DateTime(1984, 1, 7), DateTimeKind.Utc),
                        Genero = "Cyberpunk",
                        AutorId = autores[0].Id
                    },
                    new Livros
                    {
                        Titulo = "A arte da guerra",
                        LancamentoDoLivro = DateTime.SpecifyKind(new DateTime(500, 1, 1), DateTimeKind.Utc),
                        Genero = "Tratado",
                        AutorId = autores[1].Id
                    },
                    new Livros
                    {
                        Titulo = "Algoritmos",
                        LancamentoDoLivro = DateTime.SpecifyKind(new DateTime(1990, 2, 1), DateTimeKind.Utc),
                        Genero = "Matematica",
                        AutorId = autores[2].Id
                    }
                };
                contextoData.Livros.AddRange(livros);
                contextoData.SaveChanges();

                
                var clientes = new List<Cliente>
                {
                    new Cliente
                    {
                        PrimeiroNome = "Thomas",
                        SegundoNome = "Edson",
                        DataDeNascimento = DateTime.SpecifyKind(new DateTime(2000, 7, 11), DateTimeKind.Utc)
                    },
                    new Cliente
                    {
                        PrimeiroNome = "Mauricio",
                        SegundoNome = "Bernado",
                        DataDeNascimento = DateTime.SpecifyKind(new DateTime(2002, 10, 16), DateTimeKind.Utc)
                    }
                };

                contextoData.Clientes.AddRange(clientes);
                contextoData.SaveChanges();

                
                var clienteLivros = new List<ClienteLivro>
                {
                    new ClienteLivro
                    {
                        ClienteId = clientes[0].Id,
                        LivroId = livros[0].Id
                    },
                    new ClienteLivro
                    {
                        ClienteId = clientes[1].Id,
                        LivroId = livros[1].Id
                    },
                    new ClienteLivro
                    {
                        ClienteId = clientes[0].Id,
                        LivroId = livros[2].Id
                    }
                };

                contextoData.ClienteLivros.AddRange(clienteLivros);
                contextoData.SaveChanges();
            }
        }
    }
}
