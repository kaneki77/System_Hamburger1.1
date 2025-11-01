using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Hamburgueria.Domain;
using System;

namespace Hamburgueria.Data
{
    public class categoriaRepository : IcategoriaRepository
    {
        private readonly DbConnection _dbConnection = new DbConnection();

        public void Adicionar(categoria categoria)
        {
            const string query = "INSERT INTO categoria (Nome, Descricao) VALUES (@Nome, @Descricao)";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nome", categoria.Nome);
                    command.Parameters.AddWithValue("@Descricao", categoria.Descricao ?? (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Atualizar(categoria categoria)
        {
            const string query = "UPDATE categoria SET Nome = @Nome, Descricao = @Descricao WHERE id = @ID";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nome", categoria.Nome);
                    command.Parameters.AddWithValue("@Descricao", categoria.Descricao ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ID", categoria.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Remover(int id)
        {
            const string query = "DELETE FROM categoria WHERE id = @ID";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public categoria GetById(int id)
        {
            const string query = "SELECT id, Nome, Descricao FROM categoria WHERE id = @ID";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new categoria
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("Nome"),
                                Descricao = reader.IsDBNull(reader.GetOrdinal("Descricao")) ? null : reader.GetString("Descricao")
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public List<categoria> GetAll()
        {
            var categorias = new List<categoria>();
            const string query = "SELECT id, Nome, Descricao FROM categoria";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categorias.Add(new categoria
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("Nome"),
                                Descricao = reader.IsDBNull(reader.GetOrdinal("Descricao")) ? null : reader.GetString("Descricao")
                            });
                        }
                    }
                }
            }
            return categorias;
        }
    }
}
