using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormPresetaionLayer;
using WinFormPresetaionLayer.Atualizar;
using WinFormPresetaionLayer.Pesquisar;

namespace WinFormsPresentationLayer
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
            PermitirAcesso();
        }

        private void PermitirAcesso()
        {
            switch (NivelAcesso.Papel)
            {

                case "Recepção":
                    clienteToolStripMenuItem.Enabled = false;
                    categoriaToolStripMenuItem.Enabled = false;
                    funcionarioToolStripMenuItem.Enabled = false;
                    modalidadeToolStripMenuItem.Enabled = false;

                    atualizarCategoriaToolStripMenuItem.Enabled = false;
                    atualizarClienteToolStripMenuItem.Enabled = false;
                    atualizarFuncionarioToolStripMenuItem.Enabled = false;
                    atualizarModalidadeToolStripMenuItem.Enabled = false;

                    pesquisarCategoriaToolStripMenuItem.Enabled = false;
                    pesquisarClienteToolStripMenuItem.Enabled = false;
                    pesquisarFuncionarioToolStripMenuItem.Enabled = false;
                    pesquisarModalidadeToolStripMenuItem.Enabled = false;
                    break;

                case "Professor":

                    estoqueToolStripMenuItem.Enabled = false;
                    funcionarioToolStripMenuItem.Enabled = false;
                    produtoToolStripMenuItem.Enabled = false;
                    categoriaToolStripMenuItem.Enabled = false;

                    atualizarEstoqueToolStripMenuItem.Enabled = false;
                    atualizarFuncionarioToolStripMenuItem.Enabled = false;
                    atualizarProdutoToolStripMenuItem.Enabled = false;
                    atualizarCategoriaToolStripMenuItem.Enabled = false;

                    pesquisarEstoqueToolStripMenuItem.Enabled = false;
                    pesquisarFuncionarioToolStripMenuItem.Enabled = false;
                    pesquisarProdutoToolStripMenuItem.Enabled = false;
                    pesquisarCategoriaToolStripMenuItem.Enabled = false;

                    break;
            }
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroDeClientes formCadastroDeClientes = new FormCadastroDeClientes();
            formCadastroDeClientes.Show();
        }

        private void categoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroDeCategoria formCadastroDeCategoria = new FormCadastroDeCategoria();
            formCadastroDeCategoria.Show();
        }

        private void estoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroEstoque formCadastroEstoque = new FormCadastroEstoque();
            formCadastroEstoque.Show();
        }

        private void funcionarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroDeFuncionario formCadastroDeFuncionario = new FormCadastroDeFuncionario();
            formCadastroDeFuncionario.Show();
        }

        private void modalidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroDeModalidade formCadastroDeModalidade = new FormCadastroDeModalidade();
            formCadastroDeModalidade.Show();
        }

        private void produtoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroDeProdutos formCadastroDeProdutos = new FormCadastroDeProdutos();
            formCadastroDeProdutos.Show();
        }

        private void atualizarCategoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAtualizarCategoria formAtualizarCategoria = new FormAtualizarCategoria();
            formAtualizarCategoria.Show();
        }

        private void atualizarClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAtualizarCliente formAtualizarCliente = new FormAtualizarCliente();
            formAtualizarCliente.Show();
        }

        private void atualizarEstoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAtualizarEstoque formAtualizarEstoque = new FormAtualizarEstoque();
            formAtualizarEstoque.Show();
        }

        private void atualizarFuncionarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAtualizarFuncionario formAtualizarFuncionario = new FormAtualizarFuncionario();
            formAtualizarFuncionario.Show();
        }

        private void atualizarModalidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAtualizarModalidade formAtualizarModalidade = new FormAtualizarModalidade();
            formAtualizarModalidade.Show();
        }

        private void atualizarProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAtualizarProduto formAtualizarProduto = new FormAtualizarProduto();
            formAtualizarProduto.Show();
        }

        private void pesquisarCategoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPesquisarCategoria formPesquisarCategoria = new FormPesquisarCategoria();
            formPesquisarCategoria.Show();
        }

        private void pesquisarClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPesquisarCliente formPesquisarCliente = new FormPesquisarCliente();
            formPesquisarCliente.Show();
        }

        private void pesquisarFuncionarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPesquisarFuncionario formPesquisarFuncionario = new FormPesquisarFuncionario();
            formPesquisarFuncionario.Show();
        }

        private void pesquisarEstoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPesquisarEstoque formPesquisarEstoque = new FormPesquisarEstoque();
            formPesquisarEstoque.Show();
        }

        private void pesquisarModalidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPesquisarModalidade formPesquisarModalidade = new FormPesquisarModalidade();
            formPesquisarModalidade.Show();
        }

        private void pesquisarProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPesquisarProduto formPesquisarProduto = new FormPesquisarProduto();
            formPesquisarProduto.Show();
        }

        private void sairToolStripMenuItem1_Click(object sender, EventArgs e)
        {           
            Close();
        }

        private void tocarUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormMenu formMenu = new FormMenu();
            FormLogin formLogin = new FormLogin();
            formMenu.Closed += (s, args) => this.Close();
            formLogin.Show();
        }

        private void formaDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCadastroFormaPagamento formCadastroFormaPagamento = new FormCadastroFormaPagamento();
            formCadastroFormaPagamento.Show();
        }

        private void formaDePagamentoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormAtualizarFormaPagamento formAtualizarFormaPagamento = new FormAtualizarFormaPagamento();
            formAtualizarFormaPagamento.Visible = true;
        }

        private void formaDePagamentoToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FormPesquisarFormaPagamento formPesquisarFormaPagamento = new FormPesquisarFormaPagamento();
            formPesquisarFormaPagamento.Visible = true;
        }

        private void estoqueToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormPesquisarEstoque formPesquisarEstoque = new FormPesquisarEstoque();
            formPesquisarEstoque.Visible = true;
        }
    }
}
