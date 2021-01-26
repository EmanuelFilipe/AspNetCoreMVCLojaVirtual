using System.Collections.Generic;
using System.Linq;
using LojaVirtual.DataBase;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;

namespace LojaVirtual.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private LojaVirtualContext _banco;

        public ClienteRepository(LojaVirtualContext banco)
        {
            _banco = banco;
        }

        public void Atualizar(Cliente cliente)
        {
            _banco.Update(cliente);
            _banco.SaveChanges();
        }

        public void Cadastrar(Cliente cliente)
        {
            _banco.Add(cliente);
            _banco.SaveChanges();
        }

        public void Excluir(int id)
        {
            Cliente cliente = GetCliente(id);

            if (cliente != null)
            {
                _banco.Remove(cliente);
                _banco.SaveChanges();
            }
        }

        public IEnumerable<Cliente> GetAllClientes()
        {
            return _banco.Clientes.ToList();
        }

        public Cliente GetCliente(int id)
        {
            return _banco.Clientes.Find(id);
        }

        public Cliente Login(string email, string senha)
        {
            return _banco.Clientes.DefaultIfEmpty(new Cliente())
                                  .Where(x => x.Email == email && x.Senha == senha).SingleOrDefault();
        }
    }
}
