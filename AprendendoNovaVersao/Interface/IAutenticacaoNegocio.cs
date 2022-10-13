using AprendendoNovaVersao.Model;

namespace AprendendoNovaVersao.Interface
{
    public interface IAutenticacaoNegocio
    {
        public string CriarTokenAutenticacao(Usuario usuario);
    }
}
