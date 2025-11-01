using System.Windows.Forms;
using Hamburgueria.Domain;

namespace Hamburgueria.UI
{
    public partial class Form1 : Form
    {
        private readonly ClienteService _clienteService;
        private readonly CategoriaService _categoriaService;
        private readonly ProdutoCardapioService _produtoCardapioService;

        public Form1(ClienteService clienteService, CategoriaService categoriaService, ProdutoCardapioService produtoCardapioService)
        {
            InitializeComponent();
            _clienteService = clienteService;
            _categoriaService = categoriaService;
            _produtoCardapioService = produtoCardapioService;
            this.Text = "Menu Principal";
        }

        private void btnCategoria_Click(object sender, System.EventArgs e)
        {
            var formCategoria = new FormCategoria(_categoriaService);
            formCategoria.ShowDialog();
        }

        private void btnProdutoCardapio_Click(object sender, System.EventArgs e)
        {
            var formProdutoCardapio = new FormProdutoCardapio(_produtoCardapioService, _categoriaService);
            formProdutoCardapio.ShowDialog();
        }
    }
}
