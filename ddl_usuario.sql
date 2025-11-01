-- Script de criação da tabela usuario
-- Database: hamburgueria
-- Tabela: usuario

USE hamburgueria;

-- Criar tabela usuario
CREATE TABLE IF NOT EXISTS usuario (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    login VARCHAR(50) NOT NULL UNIQUE,
    senha VARCHAR(255) NOT NULL,
    nivelAcesso VARCHAR(20) NOT NULL,
    ativo BOOLEAN DEFAULT TRUE,
    dataCriacao DATETIME DEFAULT CURRENT_TIMESTAMP,
    dataAtualizacao DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

-- Inserir usuário administrador padrão (senha: admin123)
INSERT INTO usuario (nome, login, senha, nivelAcesso) 
VALUES ('Administrador', 'admin', 'admin123', 'Gerente');

-- Inserir usuário atendente de teste (senha: atendente123)
INSERT INTO usuario (nome, login, senha, nivelAcesso) 
VALUES ('Atendente Teste', 'atendente', 'atendente123', 'Atendente');

-- Verificar dados inseridos
SELECT * FROM usuario;
