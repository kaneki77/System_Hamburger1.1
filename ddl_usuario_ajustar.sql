-- Script para ajustar a estrutura da tabela usuario existente
-- Database: hamburgueria
-- Tabela: usuario

-- Selecionar o banco de dados
USE hamburgueria;

-- Adicionar coluna 'senha' se não existir
SET @col_exists = 0;
SELECT COUNT(*) INTO @col_exists 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_SCHEMA = 'hamburgueria' 
  AND TABLE_NAME = 'usuario' 
  AND COLUMN_NAME = 'senha';

SET @query = IF(@col_exists = 0, 
    'ALTER TABLE usuario ADD COLUMN senha VARCHAR(255) NOT NULL AFTER login', 
    'SELECT "Coluna senha já existe" AS Status');
PREPARE stmt FROM @query;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

-- Adicionar coluna 'nivelAcesso' se não existir
SET @col_exists = 0;
SELECT COUNT(*) INTO @col_exists 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_SCHEMA = 'hamburgueria' 
  AND TABLE_NAME = 'usuario' 
  AND COLUMN_NAME = 'nivelAcesso';

SET @query = IF(@col_exists = 0, 
    'ALTER TABLE usuario ADD COLUMN nivelAcesso VARCHAR(20) NOT NULL AFTER senha', 
    'SELECT "Coluna nivelAcesso já existe" AS Status');
PREPARE stmt FROM @query;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

-- Adicionar coluna 'ativo' se não existir
SET @col_exists = 0;
SELECT COUNT(*) INTO @col_exists 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_SCHEMA = 'hamburgueria' 
  AND TABLE_NAME = 'usuario' 
  AND COLUMN_NAME = 'ativo';

SET @query = IF(@col_exists = 0, 
    'ALTER TABLE usuario ADD COLUMN ativo TINYINT(1) DEFAULT 1 AFTER nivelAcesso', 
    'SELECT "Coluna ativo já existe" AS Status');
PREPARE stmt FROM @query;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

-- Adicionar coluna 'dataCriacao' se não existir
SET @col_exists = 0;
SELECT COUNT(*) INTO @col_exists 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_SCHEMA = 'hamburgueria' 
  AND TABLE_NAME = 'usuario' 
  AND COLUMN_NAME = 'dataCriacao';

SET @query = IF(@col_exists = 0, 
    'ALTER TABLE usuario ADD COLUMN dataCriacao DATETIME DEFAULT CURRENT_TIMESTAMP AFTER ativo', 
    'SELECT "Coluna dataCriacao já existe" AS Status');
PREPARE stmt FROM @query;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

-- Adicionar coluna 'dataAtualizacao' se não existir
SET @col_exists = 0;
SELECT COUNT(*) INTO @col_exists 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_SCHEMA = 'hamburgueria' 
  AND TABLE_NAME = 'usuario' 
  AND COLUMN_NAME = 'dataAtualizacao';

SET @query = IF(@col_exists = 0, 
    'ALTER TABLE usuario ADD COLUMN dataAtualizacao DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP AFTER dataCriacao', 
    'SELECT "Coluna dataAtualizacao já existe" AS Status');
PREPARE stmt FROM @query;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

-- Inserir usuário administrador padrão (senha: admin123)
INSERT IGNORE INTO usuario (nome, login, senha, nivelAcesso) 
VALUES ('Administrador', 'admin', 'admin123', 'Gerente');

-- Inserir usuário atendente de teste (senha: atendente123)
INSERT IGNORE INTO usuario (nome, login, senha, nivelAcesso) 
VALUES ('Atendente Teste', 'atendente', 'atendente123', 'Atendente');

-- Verificar estrutura final da tabela
DESCRIBE usuario;

-- Verificar dados inseridos
SELECT * FROM usuario;
