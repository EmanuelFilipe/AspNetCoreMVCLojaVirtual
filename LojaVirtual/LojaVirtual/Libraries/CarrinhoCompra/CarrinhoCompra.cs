using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtual.Libraries.Cookie;
using LojaVirtual.Models.ProdutoAgregador;
using Newtonsoft.Json;

namespace LojaVirtual.Libraries.CarrinhoCompra
{
    public class CarrinhoCompra
    {
        private string _key = "Carrinho.Compras";
        private Cookie.Cookie _cookie;

        public CarrinhoCompra(Cookie.Cookie cookie)
        {
            _cookie = cookie;
        }

        public void Cadastrar(ProdutoItem item)
        {
            List<ProdutoItem> lista;

            if (_cookie.Existe(_key))
            {
                lista = Consultar();
                var itemLocalizado = lista.SingleOrDefault(i => i.Id == item.Id);

                if (itemLocalizado != null)
                    lista.Add(item);
                else
                    itemLocalizado.Quantidade += 1;
            }
            else
            {
                lista = new List<ProdutoItem>();
                lista.Add(item);
            }

            Salvar(lista);
        }

        public void Atualizar(ProdutoItem item)
        {
            var lista = Consultar();
            var itemLocalizado = lista.SingleOrDefault(i => i.Id == item.Id);

            if (itemLocalizado != null)
            {
                lista.Remove(itemLocalizado);
                Salvar(lista);
            }
        }

        public void Remover(ProdutoItem item)
        {
            var lista = Consultar();
            var itemLocalizado = lista.SingleOrDefault(i => i.Id == item.Id);

            if (itemLocalizado != null)
            {
                itemLocalizado.Quantidade = item.Quantidade;
                Salvar(lista);
            }
        }

        public List<ProdutoItem> Consultar()
        {
            if (_cookie.Existe(_key))
            {
                string valor = _cookie.Consultar(_key);
                return JsonConvert.DeserializeObject<List<ProdutoItem>>(valor);
            }
            else
            {
                return new List<ProdutoItem>();
            }
        }

        public bool Existe(string key)
        {
            return (_cookie.Existe(_key) ? false : true);
        }

        public void RemoverTodos()
        {
            _cookie.Remover(_key);
        }

        public void Salvar(List<ProdutoItem> lista)
        {
            string valor = JsonConvert.SerializeObject(lista);
            _cookie.Cadastrar(_key, valor);
        }
    }

}
