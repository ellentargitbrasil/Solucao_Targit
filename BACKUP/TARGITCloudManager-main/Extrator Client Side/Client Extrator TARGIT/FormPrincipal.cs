using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayerUI
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
            customizeDesign();
        }

        private void customizeDesign()
        {
            panelCadastroSubMenu.Visible = false;
            panelAcompSubMenu.Visible = false;
            panelHelpSubMenu.Visible = false;

        }

        private void hideSubMenu()
        {
            if (panelCadastroSubMenu.Visible == true)
                panelCadastroSubMenu.Visible = false;
            if (panelAcompSubMenu.Visible == true)
                panelAcompSubMenu.Visible = false;
            if (panelHelpSubMenu.Visible == true)
                panelHelpSubMenu.Visible = false;
        }

        private void showSubMenu(Panel subMenu)
        {
            if(subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            } else
            {
                subMenu.Visible = false;
            }

        }

        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelCadastro.Controls.Add(childForm);
            panelCadastro.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        #region CadastroSubMenu
        private void buttonCadastro_Click(object sender, EventArgs e)
        {
            showSubMenu(panelCadastroSubMenu);
        }

        private void buttonInfoCliente_Click(object sender, EventArgs e)
        {
            openChildForm(new Form3());
        }
        private void buttonBancoDados_Click(object sender, EventArgs e)
        {
            openChildForm(new Form2());
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }
        #endregion


        #region AcompanhamentoSubMenu
        private void buttonAcompanhamento_Click(object sender, EventArgs e)
        {
            showSubMenu(panelAcompSubMenu);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonLog_Click(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private void panelAcompSubMenu_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion

        private void buttonLog_Click_1(object sender, EventArgs e)
        {
            hideSubMenu();
        }

        private Form activeForm = null;

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        #region HelpSubMenu
        private void buttonHelp_Click(object sender, EventArgs e)
        {
            showSubMenu(panelHelpSubMenu);
        }

        private void buttonFAQ_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
