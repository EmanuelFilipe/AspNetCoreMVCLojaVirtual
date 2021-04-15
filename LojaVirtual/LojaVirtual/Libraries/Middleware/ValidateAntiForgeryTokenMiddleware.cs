using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Middleware
{
    public class ValidateAntiForgeryTokenMiddleware
    {
        private RequestDelegate _next;
        private IAntiforgery _antiforgery;

        public ValidateAntiForgeryTokenMiddleware(RequestDelegate next, IAntiforgery antiforgery)
        {
            _next = next;
            _antiforgery = antiforgery;
        }

        public async Task Invoke(HttpContext context)
        {
            var header = context.Request.Headers["x-requested-with"];
            bool ajax = header == "XMLHttpRequest";

            if ((HttpMethods.IsPost(context.Request.Method)) && 
               !context.Request.Path.ToString().Contains("Excluir") &&
               !(context.Request.Form.Files.Count == 1 && ajax))
            {
                await _antiforgery.ValidateRequestAsync(context);
            }

            await _next(context);
        }
    }
}
