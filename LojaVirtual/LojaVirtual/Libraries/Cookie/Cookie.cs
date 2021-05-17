using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace LojaVirtual.Libraries.Cookie
{
    public class Cookie
    {
        private IHttpContextAccessor _context;

        public Cookie(IHttpContextAccessor context)
        {
            _context = context;
        }

        public void Cadastrar(string key, string valor)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(7);

            _context.HttpContext.Response.Cookies.Append(key, valor, options);
        }

        public void Atualizar(string key, string valor)
        {
            if (Existe(key))
                Remover(key);

            Cadastrar(key, valor);
        }

        public void Remover(string key)
        {
            _context.HttpContext.Response.Cookies.Delete(key);
        }

        public string Consultar(string key)
        {
            return _context.HttpContext.Request.Cookies[key];
        }

        public bool Existe(string key)
        {
            return (_context.HttpContext.Request.Cookies[key] == null ? false : true);
        }

        public void RemoverTodos()
        {
            var listaCookies = _context.HttpContext.Request.Cookies.ToList();

            foreach (var cookie in listaCookies)
            {
                Remover(cookie.Key);
            }
        }
    }
}
