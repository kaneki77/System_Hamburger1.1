-- Script para inserir usuários de teste na tabela usuario existente
-- Database: hamburgueria
-- Tabela: usuario

USE hamburgueria;

-- Inserir usuário administrador padrão (senha: admin123)
-- Usa INSERT IGNORE para não dar erro se já existir
INSERT IGNORE INTO usuario (nome, login, senha_hash, nivel_acesso) 
VALUES ('Administrador', 'admin', 'admin123', 'Gerente');

-- Inserir usuário atendente de teste (senha: atendente123)
-- Usa INSERT IGNORE para não dar erro se já existir
INSERT IGNORE INTO usuario (nome, login, senha_hash, nivel_acesso) 
VALUES ('Atendente Teste', 'atendente', 'atendente123', 'Atendente');

-- Verificar dados inseridos
SELECT * FROM usuario;
