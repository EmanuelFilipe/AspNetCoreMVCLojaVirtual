﻿using LojaVirtual.DataBase;
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
    }
}