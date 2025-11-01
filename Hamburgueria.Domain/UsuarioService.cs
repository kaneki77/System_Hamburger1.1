using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Hamburgueria.Domain
{
    /// <summary>
    /// Serviço para gerenciar usuários com regras de negócio e autenticação
    /// </summary>
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Adiciona um novo usuário com senha criptografada
        /// </summary>
        public void Adicionar(string nome, string login, string senha, NivelAcesso nivelAcesso)
        {
            // Validações
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do usuário é obrigatório.");

            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("O login é obrigatório.");

            if (login.Length < 3)
                throw new ArgumentException("O login deve ter pelo menos 3 caracteres.");

            if (string.IsNullOrWhiteSpace(senha))
                throw new ArgumentException("A senha é obrigatória.");

            if (senha.Length < 6)
                throw new ArgumentException("A senha deve ter pelo menos 6 caracteres.");

            if (_usuarioRepository.ExisteLogin(login))
                throw new InvalidOperationException($"O login '{login}' já está em uso.");

            // Criar usuário com senha hash
            var usuario = new Usuario
            {
                Nome = nome,
                Login = login,
                SenhaHash = GerarHashSenha(senha),
                NivelAcesso = nivelAcesso
            };

            _usuarioRepository.Adicionar(usuario);
        }

        /// <summary>
        /// Atualiza um usuário existente
        /// </summary>
        public void Atualizar(int id, string nome, string login, string novaSenha, NivelAcesso nivelAcesso)
        {
            var usuario = _usuarioRepository.GetById(id);
            if (usuario == null)
                throw new InvalidOperationException("Usuário não encontrado.");

            // Validações
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("O nome do usuário é obrigatório.");

            if (string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("O login é obrigatório.");

            if (login.Length < 3)
                throw new ArgumentException("O login deve ter pelo menos 3 caracteres.");

            // Verificar se o login já existe para outro usuário
            var usuarioExistente = _usuarioRepository.GetByLogin(login);
            if (usuarioExistente != null && usuarioExistente.Id != id)
                throw new InvalidOperationException($"O login '{login}' já está em uso por outro usuário.");

            usuario.Nome = nome;
            usuario.Login = login;
            usuario.NivelAcesso = nivelAcesso;

            // Atualizar senha apenas se uma nova senha foi fornecida
            if (!string.IsNullOrWhiteSpace(novaSenha))
            {
                if (novaSenha.Length < 6)
                    throw new ArgumentException("A senha deve ter pelo menos 6 caracteres.");

                usuario.SenhaHash = GerarHashSenha(novaSenha);
            }

            _usuarioRepository.Atualizar(usuario);
        }

        /// <summary>
        /// Remove um usuário
        /// </summary>
        public void Remover(int id)
        {
            var usuario = _usuarioRepository.GetById(id);
            if (usuario == null)
                throw new InvalidOperationException("Usuário não encontrado.");

            _usuarioRepository.Remover(id);
        }

        /// <summary>
        /// Obtém um usuário por ID
        /// </summary>
        public Usuario GetById(int id)
        {
            return _usuarioRepository.GetById(id);
        }

        /// <summary>
        /// Obtém todos os usuários
        /// </summary>
        public List<Usuario> GetAll()
        {
            return _usuarioRepository.GetAll();
        }

        /// <summary>
        /// Autentica um usuário com login e senha
        /// </summary>
        public Usuario Autenticar(string login, string senha)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(senha))
                return null;

            var usuario = _usuarioRepository.GetByLogin(login);
            if (usuario == null)
                return null;

            // Verificar se a senha está correta
            var senhaHash = GerarHashSenha(senha);
            if (usuario.SenhaHash != senhaHash)
                return null;

            return usuario;
        }

        /// <summary>
        /// Gera o hash SHA256 da senha
        /// </summary>
        private string GerarHashSenha(string senha)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(senha);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
