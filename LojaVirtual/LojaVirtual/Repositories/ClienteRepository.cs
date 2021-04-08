using System.Collections.Generic;
using System.Linq;
using LojaVirtual.DataBase;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using X.PagedList;

namespace LojaVirtual.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private LojaVirtualContext _banco;
        private IConfiguration _configuration;

        public ClienteRepository(LojaVirtualContext banco, IConfiguration configuration)
        {
            _banco = banco;
            _configuration = configuration;
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

        public IPagedList<Cliente> GetAllClientes(int? pagina, string pesquisa)
        {
            int numeroPagina = pagina ?? 1;
            var bancoCliente = _banco.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa))
            {
                bancoCliente = bancoCliente.Where(c => c.Nome.Contains(pesquisa.Trim()) || c.Email.Contains(pesquisa.Trim()));
            }

            return bancoCliente.ToPagedList<Cliente>(numeroPagina, _configuration.GetValue<int>("RegistroPorPagina"));
        }

        public Cliente GetCliente(int id)
        {
            return _banco.Clientes.Find(id);
        }

        public Cliente Login(string email, string senha)
        {
            return _banco.Clientes.DefaultIfEmpty(new Cliente())
                                  .Where(x => x.Email == email && x.Senha == senha).FirstOrDefault();
        }
    }
}
