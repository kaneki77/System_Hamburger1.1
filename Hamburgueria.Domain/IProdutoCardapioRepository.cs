using System.Collections.Generic;

namespace Hamburgueria.Domain
{
    public interface IProdutoCardapioRepository
    {
        void Adicionar(ProdutoCardapio produto);
        void Atualizar(ProdutoCardapio produto);
        void Remover(int id);
        ProdutoCardapio GetById(int id);
        List<ProdutoCardapio> GetAll();
    }
}
