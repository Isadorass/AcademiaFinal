using BussinesLogicalLayer;
using Entities;
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
    public partial class FormPesquisarEstoque : Form
    {
        private StandardValidation standardValidation = new StandardValidation();
        private EstoqueBLL estoqueBLL = new EstoqueBLL();
        public FormPesquisarEstoque()
        {
            InitializeComponent();
            BuscarEstoques();
        }

        private void BuscarEstoques()
        {
            DataResponse<Estoque> dataResponse = estoqueBLL.GetAll();

            dgvEstoque.DataSource = dataResponse.Data;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (standardValidation.ValidationsDataMatricula(dtpDataEntrada.Text))
            {
                DataResponse<Estoque> dataResponse = estoqueBLL.PesquisarPorDataEntrada(dtpDataEntrada.Text);

                dgvEstoque.DataSource = dataResponse.Data;

                BuscarEstoques();
            }
            else
            {
                BuscarEstoques();
            }
        }
    }
}
