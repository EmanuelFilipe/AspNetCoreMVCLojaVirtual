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
    public class CategoriaRepository : ICategoriaRepository
    {
        LojaVirtualContext _banco;
        private IConfiguration _configuration;
        private List<Categoria> listaCategoriaRecursiva = new List<Categoria>();
        private List<Categoria> categorias;



        public CategoriaRepository(LojaVirtualContext banco, IConfiguration configuration)
        {
            _banco = banco;
            _configuration = configuration;
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

        public IPagedList<Categoria> GetAllCategorias(int? pagina)
        {
            int numeroPagina = pagina ?? 1;
            return _banco.Categorias.Include(c => c.CategoriaPai)
                                    .ToPagedList<Categoria>(numeroPagina, _configuration.GetValue<int>("RegistroPorPagina"));
        }

        public IEnumerable<Categoria> GetAllCategorias()
        {
            return _banco.Categorias.ToList();
        }

        public Categoria GetCategoria(int id)
        {
            return _banco.Categorias.Find(id);
        }

        public Categoria GetCategoria(string slug)
        {
            return _banco.Categorias.Where(c => c.Slug == slug).FirstOrDefault();
        }

        public IEnumerable<Categoria> GetCategoriasRecursivas(Categoria categoriaPai)
        {
            if (categorias == null)
            {
                categorias = GetAllCategorias().ToList();
            }
            
            if (!listaCategoriaRecursiva.Exists(c => c.Id == categoriaPai.Id))
            {
                listaCategoriaRecursiva.Add(categoriaPai);
            }

            var listaCategoriaFilho = categorias.Where(c => c.CategoriaPaiId == categoriaPai.Id);

            if (listaCategoriaFilho.Count() > 0)
            {
                listaCategoriaRecursiva.AddRange(listaCategoriaFilho.ToList());
                foreach (var categoria in listaCategoriaFilho)
                {
                    GetCategoriasRecursivas(categoria);
                }
            }

            return listaCategoriaRecursiva;
        }
    }
}
