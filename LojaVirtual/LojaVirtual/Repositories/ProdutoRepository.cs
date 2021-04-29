using LojaVirtual.DataBase;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace LojaVirtual.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        LojaVirtualContext _banco;
        private IConfiguration _configuration;

        public ProdutoRepository(LojaVirtualContext banco, IConfiguration configuration)
        {
            _banco = banco;
            _configuration = configuration;
        }

        public void Atualizar(Produto produto)
        {
            _banco.Update(produto);
            _banco.SaveChanges();
        }

        public void Cadastrar(Produto produto)
        {
            _banco.Add(produto);
            _banco.SaveChanges();
        }

        public void Excluir(int id)
        {
            Produto produto = GetProduto(id);

            if (produto != null)
            {
                _banco.Remove(produto);
                _banco.SaveChanges();
            }
        }

        public IPagedList<Produto> GetAllProdutos(int? pagina, string pesquisa)
        {
            return GetAllProdutos(pagina, pesquisa, "A");
        }

        public IPagedList<Produto> GetAllProdutos(int? pagina, string pesquisa, string ordenacao)
        {
            int numeroPagina = pagina ?? 1;
            var bancoProduto = _banco.Produtos.AsQueryable();

            if (!string.IsNullOrEmpty(pesquisa))
                bancoProduto = bancoProduto.Where(c => c.Nome.Contains(pesquisa.Trim()));

            if (ordenacao == "A")
                bancoProduto = bancoProduto.OrderBy(p => p.Nome);

            if (ordenacao == "ME")
                bancoProduto = bancoProduto.OrderBy(p => p.Valor);

            if (ordenacao == "MA")
                bancoProduto = bancoProduto.OrderByDescending(p => p.Valor);


            return bancoProduto.Include(i => i.Imagens).ToPagedList<Produto>(numeroPagina, _configuration.GetValue<int>("RegistroPorPagina"));
        }

        public Produto GetProduto(int id)
        {
            return _banco.Produtos.Include(i => i.Imagens).OrderBy(p => p.Nome).Where(p => p.Id == id).FirstOrDefault();
        }
    }
}
