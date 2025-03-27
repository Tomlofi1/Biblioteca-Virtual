using BibliotecaVirtual.Models;

namespace BibliotecaVirtual.Interface
{
    public interface IClienteRepository
    {
        ICollection<Cliente> GetClientes();
        Cliente GetClientesPeloId(int id);
        ICollection<Cliente> GetClientesPelaDatadeNascimento(DateTime? dataInicial, DateTime? dataFinal);
        bool ClienteExiste(int id);
        bool CreateCliente(Cliente cliente);
        bool DeleteCliente(int id, Cliente cliente);
        bool UpdateCliente(int id, Cliente cliente);
        bool Save();
        
    }
}
