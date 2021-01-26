using LojaVirtual.Models;
using System.Collections.Generic;

namespace LojaVirtual.Repositories.Contracts
{
    public interface INewsLetterRepository
    {
        void Cadastrar(NewsLetterEmail newsLetter);
        IEnumerable<NewsLetterEmail> GetAllNewsLetter();
    }
}
