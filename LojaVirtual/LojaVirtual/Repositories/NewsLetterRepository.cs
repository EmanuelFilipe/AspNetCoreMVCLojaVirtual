using LojaVirtual.DataBase;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Repositories
{
    public class NewsLetterRepository : INewsLetterRepository
    {
        private LojaVirtualContext _banco;

        public NewsLetterRepository(LojaVirtualContext banco)
        {
            _banco = banco;
        } 

        public void Cadastrar(NewsLetterEmail newsLetter)
        {
            _banco.NewsLetterEmails.Add(newsLetter);
            _banco.SaveChanges();
        }

        public IEnumerable<NewsLetterEmail> GetAllNewsLetter()
        {
            return _banco.NewsLetterEmails.ToList();
        }
    }
}
