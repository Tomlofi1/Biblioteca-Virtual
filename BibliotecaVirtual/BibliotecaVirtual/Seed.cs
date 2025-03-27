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
                
                var livros = new List<Livros>
                {
                    new Livros
                    {
                        Titulo = "Neuromancer",
                        LancamentoDoLivro = DateTime.SpecifyKind(new DateTime(1984, 1, 7), DateTimeKind.Utc),
                        Genero = "Cyberpunk"
                    },
                    new Livros
                    {
                        Titulo = "A arte da guerra",
                        LancamentoDoLivro = DateTime.SpecifyKind(new DateTime(500, 1, 1), DateTimeKind.Utc),
                        Genero = "Tratado"
                    },
                    new Livros
                    {
                        Titulo = "Algoritmos",
                        LancamentoDoLivro = DateTime.SpecifyKind(new DateTime(1990, 2, 1), DateTimeKind.Utc),
                        Genero = "Matematica"
                    }
                };

                contextoData.Livros.AddRange(livros);

                
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
