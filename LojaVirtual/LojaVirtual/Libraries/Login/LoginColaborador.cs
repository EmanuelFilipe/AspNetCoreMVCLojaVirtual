using LojaVirtual.Models;
using Newtonsoft.Json;

namespace LojaVirtual.Libraries.Login
{
    public class LoginColaborador
    {
        private string Key = "Login.Colaborador";
        private Sessao.Sessao _sessao;

        public LoginColaborador(Sessao.Sessao sessao)
        {
            _sessao = sessao;
        }

        public void Login(Colaborador cliente)
        {
            //serializar
            string colaboradorJSONString = JsonConvert.SerializeObject(cliente);
            _sessao.Cadastrar(Key, colaboradorJSONString);
        }

        public Colaborador GetColaborador()
        {
            if (_sessao.Existe(Key))
            {
                //deserializar
                string colaboradorJSONString = _sessao.Consultar(Key);
                return JsonConvert.DeserializeObject<Colaborador>(colaboradorJSONString);
            }
            else
                return null;
        }

        public void Logout()
        {
            _sessao.RemoverTodos();
        }
    }
}
