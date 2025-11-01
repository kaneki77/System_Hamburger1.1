using System.Windows.Forms;
using Hamburgueria.Domain;

namespace Hamburgueria.UI
{
    public partial class Form1 : Form
    {
        private readonly ClienteService _clienteService;
        private readonly CategoriaService _categoriaService;
        private readonly ProdutoCardapioService _produtoCardapioService;
        private readonly UsuarioService _usuarioService;

        public Form1(ClienteService clienteService, CategoriaService categoriaService, ProdutoCardapioService produtoCardapioService, UsuarioService usuarioService)
        {
            InitializeComponent();
            _clienteService = clienteService;
            _categoriaService = categoriaService;
            _produtoCardapioService = produtoCardapioService;
            _usuarioService = usuarioService;
            this.Text = "Menu Principal - " + UsuarioLogado.Nome + " (" + UsuarioLogado.NivelAcesso + ")";
            ConfigurarAcessoPorNivel();
        }

        private void ConfigurarAcessoPorNivel()
        {
            // Apenas Gerentes podem acessar o gerenciamento de usuários
            if (UsuarioLogado.NivelAcesso != "Gerente")
            {
                // Se houver um botão de usuários, desabilitar para Atendentes
                // Por enquanto, vamos apenas deixar preparado
            }
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

        private void btnUsuario_Click(object sender, System.EventArgs e)
        {
            // Apenas Gerentes podem acessar o gerenciamento de usuários
            if (UsuarioLogado.NivelAcesso != "Gerente")
            {
                MessageBox.Show("Acesso negado. Apenas Gerentes podem gerenciar usuários.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var formUsuario = new FormUsuario(_usuarioService);
            formUsuario.ShowDialog();
        }
    }
}
