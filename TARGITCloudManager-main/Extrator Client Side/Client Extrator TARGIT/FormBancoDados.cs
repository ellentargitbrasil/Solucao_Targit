using ClientExtratorTARGIT;
using MongoDB.Bson;
using MongoDB.Driver;
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
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();

        }

        Mongo mongoDBConnection = new Mongo();
        SQLite SQLiteConnection = new SQLite();
        
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            if (txtCnpj.Text == "")
            {
                MessageBox.Show("Insira um CNPJ");
                return;
            }
            /*BsonDocument document = mongoDBConnection.GetData(txtCnpj.Text);
            if (document != null)
            {
                MessageBox.Show("Razão Social: " + document.GetValue("razao_social").AsString);
            } else if (document == null)
            {
                MessageBox.Show("CNPJ não cadastrado!");
            }*/

            // pegar o cnpj e escrever no sqlite

        }



        private void ellen_Click(object sender, EventArgs e)
        {
            int teste = SQLiteConnection.ReadSQLite();
            MessageBox.Show("A Ellen é chata pelo menos " + teste + " vezes ao dia");
        }


        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
