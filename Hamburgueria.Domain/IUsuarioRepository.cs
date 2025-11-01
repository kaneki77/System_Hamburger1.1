using System.Collections.Generic;

namespace Hamburgueria.Domain
{
    /// <summary>
    /// Interface para o repositório de Usuario
    /// </summary>
    public interface IUsuarioRepository
    {
        void Adicionar(Usuario usuario);
        void Atualizar(Usuario usuario);
        void Remover(int id);
        Usuario GetById(int id);
        Usuario GetByLogin(string login);
        List<Usuario> GetAll();
        bool ExisteLogin(string login);
    }
}
