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
    public partial class FormPesquisarCliente : Form
    {
        private StandardValidation standardValidation = new StandardValidation();
        private ClientesBLL clientesBLL = new ClientesBLL();
        public FormPesquisarCliente()
        {
            InitializeComponent();
            BuscarClientes();
        }

        private void BuscarClientes()
        {
            DataResponse<Clientes> dataResponse = clientesBLL.GetAll();

            dgvCliente.DataSource = dataResponse.Data;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (standardValidation.ValidationsNome(txtNome.Text))
            {
                DataResponse<Clientes> dataResponse = clientesBLL.PesquisarPorNome(txtNome.Text);

                dgvCliente.DataSource = dataResponse.Data;

                BuscarClientes();
            }
            else
            {
                BuscarClientes();
            }
        }
    }
}
