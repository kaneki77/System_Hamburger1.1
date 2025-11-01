using System;

namespace Hamburgueria.Domain
{
    /// <summary>
    /// Entidade Usuario - Representa um usuário do sistema com controle de acesso
    /// </summary>
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string SenhaHash { get; set; }
        public NivelAcesso NivelAcesso { get; set; }
    }

    /// <summary>
    /// Enum para níveis de acesso do sistema
    /// </summary>
    public enum NivelAcesso
    {
        Atendente = 0,
        Gerente = 1
    }
}
