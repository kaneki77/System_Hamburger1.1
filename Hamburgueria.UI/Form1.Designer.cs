namespace Hamburgueria.UI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Menu Principal";
            this.btnCategoria = new System.Windows.Forms.Button();
            this.btnProdutoCardapio = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCategoria
            // 
            this.btnCategoria.Location = new System.Drawing.Point(12, 12);
            this.btnCategoria.Name = "btnCategoria";
            this.btnCategoria.Size = new System.Drawing.Size(150, 30);
            this.btnCategoria.TabIndex = 0;
            this.btnCategoria.Text = "Gerenciar Categorias";
            this.btnCategoria.UseVisualStyleBackColor = true;
            this.btnCategoria.Click += new System.EventHandler(this.btnCategoria_Click);
            // 
            // btnProdutoCardapio
            // 
            this.btnProdutoCardapio.Location = new System.Drawing.Point(12, 48);
            this.btnProdutoCardapio.Name = "btnProdutoCardapio";
            this.btnProdutoCardapio.Size = new System.Drawing.Size(150, 30);
            this.btnProdutoCardapio.TabIndex = 1;
            this.btnProdutoCardapio.Text = "Gerenciar Produtos";
            this.btnProdutoCardapio.UseVisualStyleBackColor = true;
            this.btnProdutoCardapio.Click += new System.EventHandler(this.btnProdutoCardapio_Click);
            // 
            // Form1
            // 
            this.Controls.Add(this.btnProdutoCardapio);
            this.Controls.Add(this.btnCategoria);
            this.Name = "Form1";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button btnCategoria;
        private System.Windows.Forms.Button btnProdutoCardapio;
    }
}
