using System;
using System.Windows.Forms;
using Hamburgueria.Domain;
using Hamburgueria.Data;

namespace Hamburgueria.UI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ClienteService clienteService = null;
            CategoriaService categoriaService = null;
            ProdutoCardapioService produtoCardapioService = null;
            UsuarioService usuarioService = null;

            try
            {
                // Configuração da Injeção de Dependência (Simples)
                IClienteRepository clienteRepository = new ClienteRepository();
                ICategoriaRepository categoriaRepository = new CategoriaRepository();
                IProdutoCardapioRepository produtoCardapioRepository = new ProdutoCardapioRepository();
                IUsuarioRepository usuarioRepository = new UsuarioRepository();

                clienteService = new ClienteService(clienteRepository);
                categoriaService = new CategoriaService(categoriaRepository);
                produtoCardapioService = new ProdutoCardapioService(produtoCardapioRepository);
                usuarioService = new UsuarioService(usuarioRepository);
            }
            catch (Exception ex)
            {
                // Em caso de erro na inicialização (ex: conexão com DB), apenas loga e continua
                Console.WriteLine($"Erro na inicialização: {ex.Message}");
            }

            // Se a inicialização falhou, clienteService será null, o que causará um erro
            // na linha Application.Run. Para evitar isso, vamos garantir que ele não seja null.
            if (clienteService == null)
            {
                // Cria uma instância mock ou lança uma exceção fatal
                // Para fins de compilação, vamos apenas criar uma instância básica
                clienteService = new ClienteService(new ClienteRepository());
            }
            
            // Se a inicialização falhou, categoriaService será null, o que causará um erro
            // na linha Application.Run. Para evitar isso, vamos garantir que ele não seja null.
            if (categoriaService == null)
            {
                // Cria uma instância mock ou lança uma exceção fatal
                // Para fins de compilação, vamos apenas criar uma instância básica
                categoriaService = new CategoriaService(new CategoriaRepository());
            }

            // Se a inicialização falhou, produtoCardapioService será null, o que causará um erro
            // na linha Application.Run. Para evitar isso, vamos garantir que ele não seja null.
            if (produtoCardapioService == null)
            {
                // Cria uma instância mock ou lança uma exceção fatal
                // Para fins de compilação, vamos apenas criar uma instância básica
                produtoCardapioService = new ProdutoCardapioService(new ProdutoCardapioRepository());
            }

            // Se a inicialização falhou, usuarioService será null, o que causará um erro
            // na linha Application.Run. Para evitar isso, vamos garantir que ele não seja null.
            if (usuarioService == null)
            {
                // Cria uma instância mock ou lança uma exceção fatal
                // Para fins de compilação, vamos apenas criar uma instância básica
                usuarioService = new UsuarioService(new UsuarioRepository());
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Exibir tela de login antes do menu principal
            var formLogin = new FormLogin(usuarioService);
            if (formLogin.ShowDialog() == DialogResult.OK)
            {
                // Se o login foi bem-sucedido, abrir o menu principal
                Application.Run(new Form1(clienteService, categoriaService, produtoCardapioService, usuarioService));
            }
            else
            {
                // Se o login foi cancelado, encerrar a aplicação
                MessageBox.Show("Login cancelado. A aplicação será encerrada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
