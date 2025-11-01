using System;
using System.Windows.Forms;
using Hamburgueria.Domain;

namespace Hamburgueria.UI
{
    public partial class FormLogin : Form
    {
        private readonly UsuarioService _usuarioService;

        public FormLogin(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var login = txtLogin.Text.Trim();
                var senha = txtSenha.Text;

                if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(senha))
                {
                    MessageBox.Show("Por favor, preencha login e senha.", "Atenção", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var usuario = _usuarioService.Autenticar(login, senha);

                if (usuario == null)
                {
                    MessageBox.Show("Login ou senha inválidos.", "Erro de Autenticação", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSenha.Clear();
                    txtLogin.Focus();
                    return;
                }

                // Definir usuário logado na sessão
                UsuarioLogado.Definir(usuario);

                MessageBox.Show($"Bem-vindo, {usuario.Nome}!\nNível de acesso: {usuario.NivelAcesso}", 
                    "Login realizado com sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao realizar login: {ex.Message}", "Erro", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            txtLogin.Focus();
        }
    }
}
