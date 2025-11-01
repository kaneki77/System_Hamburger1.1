-- Script de criação da tabela usuario (VERSÃO ALTERNATIVA)
-- Database: hamburgueria
-- Tabela: usuario
-- Esta versão NÃO remove a tabela existente, apenas cria se não existir

-- Selecionar o banco de dados
USE hamburgueria;

-- Criar tabela usuario (somente se não existir)
CREATE TABLE IF NOT EXISTS usuario (
    id INT NOT NULL AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    login VARCHAR(50) NOT NULL,
    senha VARCHAR(255) NOT NULL,
    nivelAcesso VARCHAR(20) NOT NULL,
    ativo TINYINT(1) DEFAULT 1,
    dataCriacao DATETIME DEFAULT CURRENT_TIMESTAMP,
    dataAtualizacao DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (id),
    UNIQUE KEY unique_login (login)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Inserir usuário administrador padrão (senha: admin123)
-- Usa INSERT IGNORE para não dar erro se já existir
INSERT IGNORE INTO usuario (nome, login, senha, nivelAcesso) 
VALUES ('Administrador', 'admin', 'admin123', 'Gerente');

-- Inserir usuário atendente de teste (senha: atendente123)
-- Usa INSERT IGNORE para não dar erro se já existir
INSERT IGNORE INTO usuario (nome, login, senha, nivelAcesso) 
VALUES ('Atendente Teste', 'atendente', 'atendente123', 'Atendente');

-- Verificar dados inseridos
SELECT * FROM usuario;
