using System;
using System.Windows.Forms;
using Hamburgueria.Domain;

namespace Hamburgueria.UI
{
    public partial class FormUsuario : Form
    {
        private readonly UsuarioService _usuarioService;
        private int? _usuarioIdSelecionado;

        public FormUsuario(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
            InitializeComponent();
        }

        private void FormUsuario_Load(object sender, EventArgs e)
        {
            // Verificar permissão (apenas Gerente pode gerenciar usuários)
            if (UsuarioLogado.NivelAcesso != "Gerente")
            {
                MessageBox.Show("Acesso negado. Apenas gerentes podem gerenciar usuários.", 
                    "Permissão Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            ConfigurarComboNivelAcesso();
            CarregarUsuarios();
        }

        private void ConfigurarComboNivelAcesso()
        {
            cmbNivelAcesso.Items.Clear();
            cmbNivelAcesso.Items.Add("Atendente");
            cmbNivelAcesso.Items.Add("Gerente");
            cmbNivelAcesso.SelectedIndex = 0;
        }

        private void CarregarUsuarios()
        {
            try
            {
                var usuarios = _usuarioService.GetAll();
                dgvUsuarios.DataSource = null;
                dgvUsuarios.DataSource = usuarios;

                // Ocultar coluna de senha hash
                if (dgvUsuarios.Columns["SenhaHash"] != null)
                    dgvUsuarios.Columns["SenhaHash"].Visible = false;

                // Configurar cabeçalhos
                if (dgvUsuarios.Columns["Id"] != null)
                    dgvUsuarios.Columns["Id"].HeaderText = "ID";
                if (dgvUsuarios.Columns["Nome"] != null)
                    dgvUsuarios.Columns["Nome"].HeaderText = "Nome";
                if (dgvUsuarios.Columns["Login"] != null)
                    dgvUsuarios.Columns["Login"].HeaderText = "Login";
                if (dgvUsuarios.Columns["NivelAcesso"] != null)
                    dgvUsuarios.Columns["NivelAcesso"].HeaderText = "Nível de Acesso";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar usuários: {ex.Message}", "Erro", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                var nome = txtNome.Text.Trim();
                var login = txtLogin.Text.Trim();
                var senha = txtSenha.Text;
                var nivelAcesso = cmbNivelAcesso.SelectedIndex == 0 ? NivelAcesso.Atendente : NivelAcesso.Gerente;

                _usuarioService.Adicionar(nome, login, senha, nivelAcesso);

                MessageBox.Show("Usuário adicionado com sucesso!", "Sucesso", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimparCampos();
                CarregarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao adicionar usuário: {ex.Message}", "Erro", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (!_usuarioIdSelecionado.HasValue)
            {
                MessageBox.Show("Selecione um usuário para atualizar.", "Atenção", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var nome = txtNome.Text.Trim();
                var login = txtLogin.Text.Trim();
                var novaSenha = txtSenha.Text; // Pode ser vazia se não quiser alterar
                var nivelAcesso = cmbNivelAcesso.SelectedIndex == 0 ? NivelAcesso.Atendente : NivelAcesso.Gerente;

                _usuarioService.Atualizar(_usuarioIdSelecionado.Value, nome, login, novaSenha, nivelAcesso);

                MessageBox.Show("Usuário atualizado com sucesso!", "Sucesso", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimparCampos();
                CarregarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar usuário: {ex.Message}", "Erro", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (!_usuarioIdSelecionado.HasValue)
            {
                MessageBox.Show("Selecione um usuário para remover.", "Atenção", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Não permitir remover o próprio usuário logado
            if (_usuarioIdSelecionado.Value == UsuarioLogado.Id)
            {
                MessageBox.Show("Você não pode remover o próprio usuário logado.", "Átenção", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var resultado = MessageBox.Show("Tem certeza que deseja remover este usuário?", 
                "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    _usuarioService.Remover(_usuarioIdSelecionado.Value);

                    MessageBox.Show("Usuário removido com sucesso!", "Sucesso", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LimparCampos();
                    CarregarUsuarios();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao remover usuário: {ex.Message}", "Erro", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvUsuarios.Rows[e.RowIndex];
                _usuarioIdSelecionado = Convert.ToInt32(row.Cells["Id"].Value);

                txtNome.Text = row.Cells["Nome"].Value.ToString();
                txtLogin.Text = row.Cells["Login"].Value.ToString();
                txtSenha.Clear(); // Não exibir a senha
                
                var nivelAcesso = row.Cells["NivelAcesso"].Value.ToString();
                cmbNivelAcesso.SelectedIndex = nivelAcesso == "Atendente" ? 0 : 1;
            }
        }

        private void LimparCampos()
        {
            _usuarioIdSelecionado = null;
            txtNome.Clear();
            txtLogin.Clear();
            txtSenha.Clear();
            cmbNivelAcesso.SelectedIndex = 0;
            txtNome.Focus();
        }
    }
}
