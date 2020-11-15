using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Registro
{

    public partial class frmCadastro : Form 
    {
        
        public frmCadastro() 
        {
            InitializeComponent();
        }
        public void CriarBancoDados()
        {
            try
            {
                DalHelper.CriarBancoSQLite();
                btnOk.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }

        public void CriarTabela()
        {
            try
            {
                DalHelper.CriarTabelaSQlite();
                btnOk.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }

        }

        public void IncluirDados()
        {
            try
            {
                Cliente cli = new Cliente();
                cli.Cnpj = txtCnpj.Text;
                cli.Ip = txtIp.Text;
                MessageBox.Show("Seus dados foram inseridos com sucesso!");
                DalHelper.Add(cli);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
        }
        void ExibirDados()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = DalHelper.GetCadastro();
                dgvDados.DataSource = dt; 

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
            if (!Valida())
            {
                MessageBox.Show("Informe os dados cliente a incluir");
                return;
            }
            try
            {
                Cliente cli = new Cliente();

                cli.Cnpj = txtCnpj.Text;
                cli.Ip = txtIp.Text;

                DalHelper.Add(cli);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro : " + ex.Message);
            }
            
            bool Valida()
            {
                if (string.IsNullOrEmpty(txtCnpj.Text) && string.IsNullOrEmpty(txtIp.Text))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
         
        }


        private void txtCnpj_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            CriarBancoDados();
            CriarTabela();
            IncluirDados();

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if(txtCnpj.Text == "" || txtIp.Text == "") 
            {
                MessageBox.Show("Os campos CNPJ e Ip não podem estar vazios");
            }
            else if(txtCnpj.Text =="" && txtIp.Text=="")
            {
                MessageBox.Show("Os campos CNPJ e Ip não podem estar vazios");
            }
            else
            {
                ExibirDados();
            }
           
        }

        
        private void dgvDados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvDados.Rows[e.RowIndex];
                txtCnpj.Text = row.Cells["Cnpj"].Value.ToString();
                txtIp.Text = row.Cells["Ip"].Value.ToString();
            }
        }

    }

}

