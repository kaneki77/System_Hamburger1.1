-- Script de criação da tabela usuario
-- Database: hamburgueria
-- Tabela: usuario

-- Selecionar o banco de dados
USE hamburgueria;

-- Remover a tabela se já existir (para recriar do zero)
DROP TABLE IF EXISTS usuario;

-- Criar tabela usuario
CREATE TABLE usuario (
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
INSERT INTO usuario (nome, login, senha, nivelAcesso) 
VALUES ('Administrador', 'admin', 'admin123', 'Gerente');

-- Inserir usuário atendente de teste (senha: atendente123)
INSERT INTO usuario (nome, login, senha, nivelAcesso) 
VALUES ('Atendente Teste', 'atendente', 'atendente123', 'Atendente');

-- Verificar dados inseridos
SELECT * FROM usuario;
