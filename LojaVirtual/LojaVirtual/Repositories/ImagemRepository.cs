using LojaVirtual.DataBase;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories
{
    public class ImagemRepository : IImagemRepository
    {
        LojaVirtualContext _banco;

        public ImagemRepository(LojaVirtualContext banco)
        {
            _banco = banco;
        }


        public void Cadastrar(Imagem imagem)
        {
            _banco.Add(imagem);
            _banco.SaveChanges();
        }

        public void Excluir(int id)
        {
            Imagem imagem = _banco.Imagens.Find(id);

            if (imagem != null)
            {
                _banco.Remove(imagem);
                _banco.SaveChanges();
            }
        }

        public void ExcluirImagensDoProduto(int produtoId)
        {
            List<Imagem> imagens = _banco.Imagens.Where(p => p.ProdutoId == produtoId).ToList();

            if (imagens != null)
            {
                foreach (Imagem imagem in imagens)
                    _banco.Remove(imagem);

                _banco.SaveChanges();
            }
        }
    }
}
