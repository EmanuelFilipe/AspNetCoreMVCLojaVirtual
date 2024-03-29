﻿using LojaVirtual.Libraries.Login;
using LojaVirtual.Models;
using LojaVirtual.Models.Constantes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Filtro
{
    public class ColaboradorAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        LoginColaborador _loginColaborador;
        private string _tipoColaboradorAutorizado;

        public ColaboradorAutorizacaoAttribute(string tipoColaboradorAutorizado = ColaboradorTipoConstante.Comum)
        {
            _tipoColaboradorAutorizado = tipoColaboradorAutorizado;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginColaborador = (LoginColaborador)context.HttpContext.RequestServices.GetService(typeof(LoginColaborador));
            Colaborador colaborador = _loginColaborador.GetColaborador();

            if (colaborador == null)
                context.Result = new RedirectToActionResult("Login", "Home", null);
            else
            {
                if (colaborador.Tipo == ColaboradorTipoConstante.Comum && _tipoColaboradorAutorizado == ColaboradorTipoConstante.Gerente)
                {
                    context.Result = new ForbidResult();
                }
            }

        }
    }
}
