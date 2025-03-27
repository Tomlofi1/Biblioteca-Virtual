using BibliotecaVirtual.Data;
using BibliotecaVirtual.DTO;
using BibliotecaVirtual.Interface;
using BibliotecaVirtual.Models;

namespace BibliotecaVirtual.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ContextoData _contexto;

        public ClienteRepository(ContextoData contextoData)
        {
            _contexto = contextoData;
        }

        public bool ClienteExiste(int id)
        {
            return _contexto.Clientes.Any(o => o.Id == id);
        }

        public bool CreateCliente(Cliente cliente)
        {
            _contexto.Add(cliente);
            return Save();
        }

        public bool DeleteCliente(int id, Cliente cliente)
        {
            _contexto.Remove(cliente);
            return Save();
        }

        public ICollection<Cliente> GetClientes()
        {
            return _contexto.Clientes.OrderBy(p => p.Id).ToList();
        }

        public ICollection<Cliente> GetClientesPelaDatadeNascimento(DateTime? dataInicial, DateTime? dataFinal)
        {
            if (!dataInicial.HasValue || !dataFinal.HasValue)
                return new List<Cliente>();

            return _contexto.Clientes.Where(p => p.DataDeNascimento >= dataInicial && p.DataDeNascimento <= dataFinal).ToList();
        }

        public Cliente GetClientesPeloId(int id)
        {
            return _contexto.Clientes.FirstOrDefault(p => p.Id == id);
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

        public bool UpdateCliente(int id,Cliente cliente)
        {
            var clienteExistente = _contexto.Clientes.Find(cliente.Id);

            if (clienteExistente == null)
            {
                return false;
            }

            clienteExistente.PrimeiroNome = cliente.PrimeiroNome.Trim();
            clienteExistente.SegundoNome = cliente.SegundoNome.Trim();
            clienteExistente.DataDeNascimento = cliente.DataDeNascimento;

            return Save();
        }
    }
}
