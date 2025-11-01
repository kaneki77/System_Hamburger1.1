using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Hamburgueria.Domain;

namespace Hamburgueria.Data
{
    /// <summary>
    /// Repositório para acesso aos dados de Usuario
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DbConnection _dbConnection = new DbConnection();

        public void Adicionar(Usuario usuario)
        {
            const string query = @"INSERT INTO usuario (nome, login, senha_hash, nivel_acesso) 
                                   VALUES (@Nome, @Login, @SenhaHash, @NivelAcesso)";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nome", usuario.Nome);
                    command.Parameters.AddWithValue("@Login", usuario.Login);
                    command.Parameters.AddWithValue("@SenhaHash", usuario.SenhaHash);
                    command.Parameters.AddWithValue("@NivelAcesso", usuario.NivelAcesso.ToString());
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Atualizar(Usuario usuario)
        {
            const string query = @"UPDATE usuario 
                                   SET nome = @Nome, login = @Login, senha_hash = @SenhaHash, nivel_acesso = @NivelAcesso 
                                   WHERE id_usuario = @Id";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nome", usuario.Nome);
                    command.Parameters.AddWithValue("@Login", usuario.Login);
                    command.Parameters.AddWithValue("@SenhaHash", usuario.SenhaHash);
                    command.Parameters.AddWithValue("@NivelAcesso", usuario.NivelAcesso.ToString());
                    command.Parameters.AddWithValue("@Id", usuario.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Remover(int id)
        {
            const string query = "DELETE FROM usuario WHERE id_usuario = @Id";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Usuario GetById(int id)
        {
            const string query = "SELECT id_usuario, nome, login, senha_hash, nivel_acesso FROM usuario WHERE id_usuario = @Id";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapUsuario(reader);
                        }
                        return null;
                    }
                }
            }
        }

        public Usuario GetByLogin(string login)
        {
            const string query = "SELECT id_usuario, nome, login, senha_hash, nivel_acesso FROM usuario WHERE login = @Login";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapUsuario(reader);
                        }
                        return null;
                    }
                }
            }
        }

        public List<Usuario> GetAll()
        {
            var usuarios = new List<Usuario>();
            const string query = "SELECT id_usuario, nome, login, senha_hash, nivel_acesso FROM usuario";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuarios.Add(MapUsuario(reader));
                        }
                    }
                }
            }
            return usuarios;
        }

        public bool ExisteLogin(string login)
        {
            const string query = "SELECT COUNT(*) FROM usuario WHERE login = @Login";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    var count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        private Usuario MapUsuario(MySqlDataReader reader)
        {
            return new Usuario
            {
                Id = reader.GetInt32("id_usuario"),
                Nome = reader.GetString("nome"),
                Login = reader.GetString("login"),
                SenhaHash = reader.GetString("senha_hash"),
                NivelAcesso = (NivelAcesso)Enum.Parse(typeof(NivelAcesso), reader.GetString("nivel_acesso"))
            };
        }
    }
}
