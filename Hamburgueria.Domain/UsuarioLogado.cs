namespace Hamburgueria.Domain
{
    /// <summary>
    /// Classe estática para armazenar informações do usuário logado na sessão
    /// </summary>
    public static class UsuarioLogado
    {
        public static int Id { get; set; }
        public static string Nome { get; set; }
        public static string Login { get; set; }
        public static string NivelAcesso { get; set; }

        /// <summary>
        /// Limpa as informações do usuário logado (logout)
        /// </summary>
        public static void Limpar()
        {
            Id = 0;
            Nome = string.Empty;
            Login = string.Empty;
            NivelAcesso = string.Empty;
        }

        /// <summary>
        /// Define as informações do usuário logado
        /// </summary>
        public static void Definir(Usuario usuario)
        {
            Id = usuario.Id;
            Nome = usuario.Nome;
            Login = usuario.Login;
            NivelAcesso = usuario.NivelAcesso;
        }

        /// <summary>
        /// Verifica se há um usuário logado
        /// </summary>
        public static bool EstaLogado()
        {
            return Id > 0;
        }
    }
}
