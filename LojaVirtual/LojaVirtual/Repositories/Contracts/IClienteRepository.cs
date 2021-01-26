using LojaVirtual.Models;
using System.Collections.Generic;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IClienteRepository
    {
        Cliente Login(string email, string senha);

        void Cadastrar(Cliente cliente);

        void Atualizar(Cliente cliente);

        void Excluir(int id);

        Cliente GetCliente(int id);

        IEnumerable<Cliente> GetAllClientes();
    }
}
