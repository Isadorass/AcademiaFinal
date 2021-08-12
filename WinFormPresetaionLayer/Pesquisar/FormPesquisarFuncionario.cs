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
    public partial class FormPesquisarFuncionario : Form
    {
        private StandardValidation standardValidation = new StandardValidation();
        private FuncionarioBLL funcionarioBLL = new FuncionarioBLL();
        public FormPesquisarFuncionario()
        {
            InitializeComponent();
            BuscarFuncionario();
        }

        private void BuscarFuncionario()
        {
            DataResponse<Funcionario> dataResponse = funcionarioBLL.GetAll();

            dgvFuncionario.DataSource = dataResponse.Data;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (standardValidation.ValidationsNome(txtNome.Text))
            {
                DataResponse<Funcionario> dataResponse = funcionarioBLL.PesquisarPorNome(txtNome.Text);

                dgvFuncionario.DataSource = dataResponse.Data;

                BuscarFuncionario();
            }
            else
            {
                BuscarFuncionario();
            }
        }
    }
}
