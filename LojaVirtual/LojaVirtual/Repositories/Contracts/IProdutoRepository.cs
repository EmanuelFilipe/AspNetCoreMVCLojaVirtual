using LojaVirtual.Models;
using LojaVirtual.Models.ProdutoAgregador;
using System.Collections.Generic;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IProdutoRepository
    {
        void Cadastrar(Produto produto);
        void Atualizar(Produto produto);
        void Excluir(int id);
        Produto GetProduto(int id);
        IPagedList<Produto> GetAllProdutos(int? pagina, string pesquisa);
        IPagedList<Produto> GetAllProdutos(int? pagina, string pesquisa, string ordenacao, IEnumerable<Categoria> categorias);
    }
}
