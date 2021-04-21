using LojaVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IImagemRepository
    {
        void Cadastrar(Imagem imagem);
        void CadastrarImagens(List<Imagem> listaDiretorioDefinitivo, int produtoId);
        void Excluir(int id);
        void ExcluirImagensDoProduto(int produtoId);
    }
}
