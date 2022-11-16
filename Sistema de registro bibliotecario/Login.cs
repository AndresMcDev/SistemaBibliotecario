using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_registro_bibliotecario
{
    
    public partial class Form1 : Form
    {
        FrmInterno logeo;
        public Form1()
        {
            InitializeComponent();
            
        }
    
        private void BtnIngreso_Click(object sender, EventArgs e)
        {
            if(TxtUser.Text != "" && TxtPass.Text != "")
            {
                if(TxtUser.Text == "Admin" && TxtPass.Text == "abc123")
                {
                    logeo = new FrmInterno();
                    logeo.Show();
                    this.Hide();
                }else{
                    MessageBox.Show("Usuario y contraseña incorrectos", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtUser.Clear();
                    TxtPass.Clear();
                    TxtUser.Focus();
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar el usuario y contraseña", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}
