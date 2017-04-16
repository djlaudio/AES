using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCifrar_Click(object sender, EventArgs e)
        {
            try
            {

                String textoClaro = "Douglas Rodriguez Valverde";
                String key = "1234567890ABCDEF";

                byte[] Cifrado = AES_Class.encrypt(Encoding.ASCII.GetBytes(textoClaro), Encoding.ASCII.GetBytes(key));
                //MessageBox.Show("Este es el cifrado: " + Convert.ToString(Cifrado));
                rtGenerico.Text = System.Text.Encoding.Default.GetString(Cifrado);

            }
            catch (Exception ex)
            {
                
                throw new Exception (ex.Message);
            }
        }

        private void btnDescifrar_Click(object sender, EventArgs e)
        {

        }



    }
}
