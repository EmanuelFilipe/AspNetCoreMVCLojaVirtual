using LojaVirtual.DataBase;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace LojaVirtual.Repositories
{
    public class ColaboradorRepository : IColaboradorRepository
    {
        private LojaVirtualContext _banco;
        private IConfiguration _configuration;

        public ColaboradorRepository(LojaVirtualContext banco, IConfiguration configuration)
        {
            _banco = banco;
            _configuration = configuration;
        }

        public void Atualizar(Colaborador colaborador)
        {
            _banco.Update(colaborador);
            
            // informa ao EF que o campo 'Senha' não deve ser modificado
            _banco.Entry(colaborador).Property(c => c.Senha).IsModified = false;

            _banco.SaveChanges();
        }

        public void AtualizarSenha(Colaborador colaborador)
        {
            _banco.Update(colaborador);

            // informa ao EF que o campos abaixo não devem ser modificados
            _banco.Entry(colaborador).Property(c => c.Nome).IsModified = false;
            _banco.Entry(colaborador).Property(c => c.Email).IsModified = false;
            _banco.Entry(colaborador).Property(c => c.Tipo).IsModified = false;

            _banco.SaveChanges();
        }

        public void Cadastrar(Colaborador colaborador)
        {
            _banco.Add(colaborador);
            _banco.SaveChanges();
        }

        public void Excluir(int id)
        {
            Colaborador colaborador = GetColaborador(id);

            if (colaborador != null)
            {
                _banco.Remove(colaborador);
                _banco.SaveChanges();
            }
        }

        public IPagedList<Colaborador> GetAllColaboradores(int? pagina)
        {
            int numeroPagina = pagina ?? 1;
            return _banco.Colaboradores.Where(c => c.Tipo != "G").ToPagedList<Colaborador>(numeroPagina, _configuration.GetValue<int>("RegistroPorPagina"));
        }

        public Colaborador GetColaborador(int id)
        {
            return _banco.Colaboradores.Find(id);
        }

        public List<Colaborador> GetColaboradorPorEmail(string email)
        {
            return _banco.Colaboradores.Where(c => c.Email == email).AsNoTracking().ToList();
        }

        public Colaborador Login(string email, string senha)
        {
            return _banco.Colaboradores.DefaultIfEmpty(new Colaborador())
                                  .Where(x => x.Email == email && x.Senha == senha).FirstOrDefault();
        }
    }
}
