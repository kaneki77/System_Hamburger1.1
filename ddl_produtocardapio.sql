-- Script SQL para criação da tabela 'produtocardapio'
-- A tabela deve ser criada no banco de dados 'hamburgueria'

CREATE TABLE IF NOT EXISTS produtocardapio (
    id INT NOT NULL AUTO_INCREMENT,
    nome VARCHAR(100) NOT NULL,
    descricao VARCHAR(255),
    precoVenda DECIMAL(10, 2) NOT NULL,
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
    idCategoria INT NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (idCategoria) REFERENCES categoria(id)
);

-- Inserção de dados de teste
INSERT INTO produtocardapio (nome, descricao, precoVenda, ativo, idCategoria) VALUES
('X-Salada', 'Pão, hambúrguer, queijo, alface e tomate.', 15.00, TRUE, 1),
('X-Bacon', 'Pão, hambúrguer, queijo, bacon.', 18.00, TRUE, 1),
('Batata Frita Média', 'Porção média de batata frita.', 8.00, TRUE, 2),
('Coca-Cola Lata', 'Lata de 350ml.', 6.00, TRUE, 3);
