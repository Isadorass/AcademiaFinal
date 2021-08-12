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
    public partial class FormPesquisarModalidade : Form
    {
        private StandardValidation standardValidation = new StandardValidation();
        private ModalidadesBLL modalidadesBLL = new ModalidadesBLL();
        public FormPesquisarModalidade()
        {
            InitializeComponent();
            BuscarModalidades();
        }

        private void BuscarModalidades()
        {
            DataResponse<Modalidades> dataResponse = modalidadesBLL.GetAll();

            dgvModalidade.DataSource = dataResponse.Data;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (standardValidation.ValidationsDescricao(txtDescricao.Text))
            {
                DataResponse<Modalidades> dataResponse = modalidadesBLL.PesquisarPorDescricao(txtDescricao.Text);

                dgvModalidade.DataSource = dataResponse.Data;

                BuscarModalidades();
            }
            else
            {
                BuscarModalidades();
            }
        }
    }
}
