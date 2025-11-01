using System;
using System.Collections.Generic;


namespace Hamburgueria.Domain
{
    public class ProdutoCardapioService
    {
        private readonly IProdutoCardapioRepository _produtoCardapioRepository;

        public ProdutoCardapioService(IProdutoCardapioRepository produtoCardapioRepository)
        {    }

        public void Adicionar(ProdutoCardapio produto)
        {
            // Regras de Negócio:
            if (string.IsNullOrWhiteSpace(produto.Nome))
            {
                throw new ArgumentException("O nome do produto é obrigatório.");
            }
            if (produto.PrecoVenda <= 0)
            {
                throw new ArgumentException("O preço de venda deve ser maior que zero.");
            }
            if (produto.IdCategoria <= 0)
            {
                throw new ArgumentException("A categoria do produto é obrigatória.");
            }

            _produtoCardapioRepository.Adicionar(produto);
        }

        public void Atualizar(ProdutoCardapio produto)
        {
            // Regras de Negócio:
            if (produto.Id <= 0)
            {
                throw new ArgumentException("O ID do produto é inválido para atualização.");
            }
            if (string.IsNullOrWhiteSpace(produto.Nome))
            {
                throw new ArgumentException("O nome do produto é obrigatório.");
            }
            if (produto.PrecoVenda <= 0)
            {
                throw new ArgumentException("O preço de venda deve ser maior que zero.");
            }
            if (produto.IdCategoria <= 0)
            {
                throw new ArgumentException("A categoria do produto é obrigatória.");
            }

            _produtoCardapioRepository.Atualizar(produto);
        }

        public void Remover(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("O ID do produto é inválido para remoção.");
            }
            _produtoCardapioRepository.Remover(id);
        }

        public ProdutoCardapio GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("O ID do produto é inválido.");
            }
            return _produtoCardapioRepository.GetById(id);
        }

        public List<ProdutoCardapio> GetAll()
        {
            return _produtoCardapioRepository.GetAll();
        }
    }
}
