using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Hamburgueria.Domain;
using System;

namespace Hamburgueria.Data
{
    public class ProdutoCardapioRepository : IProdutoCardapioRepository
    {
        private readonly DbConnection _dbConnection = new DbConnection();

        public void Adicionar(ProdutoCardapio produto)
        {
            const string query = "INSERT INTO produto (nome, descricao, preco_venda, ativo, idCategoria) VALUES (@nome, @descricao, @preco_venda, @ativo, @idCategoria)";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", produto.Nome);
                    command.Parameters.AddWithValue("@descricao", produto.Descricao ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@preco_venda", produto.PrecoVenda);
                    command.Parameters.AddWithValue("@ativo", produto.Ativo);
                    command.Parameters.AddWithValue("@idCategoria", produto.IdCategoria);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Atualizar(ProdutoCardapio produto)
        {
            const string query = "UPDATE produto SET nome = @nome, descricao = @descricao, preco_venda = @preco_venda, ativo = @ativo, idCategoria = @idCategoria WHERE id = @id";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nome", produto.Nome);
                    command.Parameters.AddWithValue("@descricao", produto.Descricao ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@preco_venda", produto.PrecoVenda);
                    command.Parameters.AddWithValue("@ativo", produto.Ativo);
                    command.Parameters.AddWithValue("@idCategoria", produto.IdCategoria);
                    command.Parameters.AddWithValue("@id", produto.Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Remover(int id)
        {
            const string query = "DELETE FROM produto WHERE id = @id";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public ProdutoCardapio GetById(int id)
        {
            const string query = "SELECT id, nome, descricao, preco_venda, ativo, idCategoria FROM produto WHERE id = @id";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ProdutoCardapio
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("nome"),
                                Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? null : reader.GetString("descricao"),
                                PrecoVenda = reader.GetDecimal("preco_venda"),
                                Ativo = reader.GetBoolean("ativo"),
                                IdCategoria = reader.GetInt32("idCategoria")
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public List<ProdutoCardapio> GetAll()
        {
            const string query = "SELECT id, nome, descricao, preco_venda, ativo, idCategoria FROM produto";

            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        var produtos = new List<ProdutoCardapio>();
                        while (reader.Read())
                        {
                            produtos.Add(new ProdutoCardapio
                            {
                                Id = reader.GetInt32("id"),
                                Nome = reader.GetString("nome"),
                                Descricao = reader.IsDBNull(reader.GetOrdinal("descricao")) ? null : reader.GetString("descricao"),
                                PrecoVenda = reader.GetDecimal("preco_venda"),
                                Ativo = reader.GetBoolean("ativo"),
                                IdCategoria = reader.GetInt32("idCategoria")
                            });
                        }
                        return produtos;
                    }
                }
            }
        }
    }
}
