using LojaVirtual.DataBase;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        LojaVirtualContext _banco;

        public CategoriaRepository(LojaVirtualContext banco)
        {
            _banco = banco;
        }

        public void Atualizar(Categoria categoria)
        {
            _banco.Update(categoria);
            _banco.SaveChanges();
        }

        public void Cadastrar(Categoria categoria)
        {
            _banco.Add(categoria);
            _banco.SaveChanges();
        }

        public void Excluir(int id)
        {
            Categoria categoria = GetCategoria(id);

            if (categoria != null)
            {
                _banco.Remove(categoria);
                _banco.SaveChanges();
            }
        }

        public IEnumerable<Categoria> GetAllCategorias()
        {
            return _banco.Categorias.ToList();
        }

        public Categoria GetCategoria(int id)
        {
            return _banco.Categorias.Find(id);
        }
    }
}
