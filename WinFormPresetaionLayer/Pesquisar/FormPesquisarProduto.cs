using BussinesLogicalLayer;
using Entites;
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

namespace WinFormPresetaionLayer.Pesquisar
{
    public partial class FormPesquisarProduto : Form
    {
        private ProdutosBLL produtosBLL = new ProdutosBLL();
        private StandardValidation standardValidation = new StandardValidation();
        public FormPesquisarProduto()
        {
            InitializeComponent();
            BuscarProdutos();
        }

        private void BuscarProdutos()
        {
            DataResponse<Produtos> dataResponse = produtosBLL.GetAll();

            dgvProduto.DataSource = dataResponse.Data;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (standardValidation.ValidationsNome(txtNome.Text))
            {
                DataResponse<Produtos> dataResponse = produtosBLL.PesquisarPorNome(txtNome.Text);

                dgvProduto.DataSource = dataResponse.Data;

                BuscarProdutos();
            }
            else
            {
                BuscarProdutos();
            }
        }
    }
}
