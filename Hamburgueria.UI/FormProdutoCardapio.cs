using System;
using System.Windows.Forms;
using Hamburgueria.Domain;

namespace Hamburgueria.UI
{
    public partial class FormProdutoCardapio : Form
    {
        private readonly ProdutoCardapioService _produtoService;
        private readonly CategoriaService _categoriaService;

        public FormProdutoCardapio(ProdutoCardapioService produtoService, CategoriaService categoriaService)
        {
            InitializeComponent();
            _produtoService = produtoService;
            _categoriaService = categoriaService;
            CarregarCategorias();
            CarregarProdutos();
        }

        private void CarregarCategorias()
        {
            try
            {
                var categorias = _categoriaService.GetAll();
                cmbCategoria.DataSource = categorias;
                cmbCategoria.DisplayMember = "Nome";
                cmbCategoria.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar categorias: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarProdutos()
        {
            try
            {
                var produtos = _produtoService.GetAll();
                dgvProdutos.DataSource = produtos;
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparCampos()
        {
            txtId.Clear();
            txtNome.Clear();
            txtDescricao.Clear();
            txtPrecoVenda.Clear();
            chkAtivo.Checked = true;
            cmbCategoria.SelectedIndex = -1;
            btnSalvar.Text = "Salvar";
            btnRemover.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCategoria.SelectedValue == null)
                {
                    MessageBox.Show("Selecione uma categoria.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var produto = new ProdutoCardapio
                {
                    Nome = txtNome.Text,
                    Descricao = txtDescricao.Text,
                    PrecoVenda = decimal.Parse(txtPrecoVenda.Text),
                    Ativo = chkAtivo.Checked,
                    IdCategoria = (int)cmbCategoria.SelectedValue
                };

                if (btnSalvar.Text == "Salvar")
                {
                    _produtoService.Adicionar(produto);
                    MessageBox.Show("Produto adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    produto.Id = int.Parse(txtId.Text);
                    _produtoService.Atualizar(produto);
                    MessageBox.Show("Produto atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CarregarProdutos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja remover este produto?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _produtoService.Remover(int.Parse(txtId.Text));
                    MessageBox.Show("Produto removido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregarProdutos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao remover produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void dgvProdutos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvProdutos.Rows[e.RowIndex];
                txtId.Text = row.Cells["Id"].Value.ToString();
                txtNome.Text = row.Cells["Nome"].Value.ToString();
                txtDescricao.Text = row.Cells["Descricao"].Value.ToString();
                txtPrecoVenda.Text = row.Cells["PrecoVenda"].Value.ToString();
                chkAtivo.Checked = (bool)row.Cells["Ativo"].Value;
                cmbCategoria.SelectedValue = row.Cells["IdCategoria"].Value;

                btnSalvar.Text = "Atualizar";
                btnRemover.Enabled = true;
            }
        }
    }
}
