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
    public partial class FormPesquisarFormaPagamento : Form
    {
        private FormaPagamentoBLL formaPagamentoBLL = new FormaPagamentoBLL();
        private StandardValidation standardValidation = new StandardValidation();
        public FormPesquisarFormaPagamento()
        {
            InitializeComponent();
            BuscarFormasPagamento();
        }

        private void BuscarFormasPagamento()
        {
            DataResponse<FormaPagamento> dataResponse = formaPagamentoBLL.GetAll();

            dgvFormaPagamento.DataSource = dataResponse.Data;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (standardValidation.ValidationNullOrWhiteSpace(txtNome.Text))
            {
                DataResponse<FormaPagamento> dataResponse = formaPagamentoBLL.PesquisarPorNome(txtNome.Text);

                dgvFormaPagamento.DataSource = dataResponse.Data;

                BuscarFormasPagamento();
            } else
            {
                BuscarFormasPagamento();
            }
        }
    }
}
