namespace Hamburgueria.Domain
{
    public class ProdutoCardapio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal PrecoVenda { get; set; }
        public bool Ativo { get; set; }
        public int IdCategoria { get; set; } // Chave estrangeira para Categoria
    }
}
