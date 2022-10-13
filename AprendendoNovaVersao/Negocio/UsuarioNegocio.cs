using AprendendoNovaVersao.Interface;
using AprendendoNovaVersao.Model;

namespace AprendendoNovaVersao.Negocio
{
    public class UsuarioNegocio : IUsuarioNegocio
    {
        private readonly IPadraoBD _padraoBD;
        public UsuarioNegocio(IPadraoBD padraoBD)
        {
            _padraoBD = padraoBD;
        }
        public Usuario SalvarUsuario(Usuario usuario)
        {
            if(usuario.Id != 0)
            {
                throw new Exception("O campo Id não deve estar preenchido.");
            }
            return _padraoBD.Salvar(usuario);
        }
    }
}
