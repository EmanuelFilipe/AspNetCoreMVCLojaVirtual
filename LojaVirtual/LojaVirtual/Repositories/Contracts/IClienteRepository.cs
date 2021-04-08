using LojaVirtual.Models;
using System.Collections.Generic;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IClienteRepository
    {
        Cliente Login(string email, string senha);

        void Cadastrar(Cliente cliente);

        void Atualizar(Cliente cliente);

        void Excluir(int id);

        Cliente GetCliente(int id);

        IPagedList<Cliente> GetAllClientes(int? pagina, string pesquisa);
    }
}
