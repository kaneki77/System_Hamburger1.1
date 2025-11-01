namespace Hamburgueria.Domain
{
    /// <summary>
    /// Classe estática para gerenciar a sessão do usuário logado
    /// </summary>
    public static class SessaoUsuario
    {
        public static Usuario UsuarioLogado { get; set; }

        public static bool EstaLogado => UsuarioLogado != null;

        public static bool EhGerente => UsuarioLogado != null && UsuarioLogado.NivelAcesso == NivelAcesso.Gerente;

        public static bool EhAtendente => UsuarioLogado != null && UsuarioLogado.NivelAcesso == NivelAcesso.Atendente;

        public static void Logout()
        {
            UsuarioLogado = null;
        }
    }
}
