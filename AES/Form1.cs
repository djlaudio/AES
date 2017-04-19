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

                String textoClaro = rtUp.Text;
                String key = tbKey.Text;
                //16 caracteres >> 128bits
                //24 caracteres >> 192bits
                //32 caracteres >> 256bits
                //Faltan hacer combo

                byte[] Cifrado = AES_Class.encrypt(Encoding.ASCII.GetBytes(textoClaro), Encoding.ASCII.GetBytes(key));
                rtDown.Text = System.Text.Encoding.Default.GetString(Cifrado);

           
            }
            catch (Exception ex)
            {
                
                throw new Exception (ex.Message);
            }
        }

        private void btnDescifrar_Click(object sender, EventArgs e)
        {
            String textoCifrado = rtUp.Text;
            String key = tbKey.Text;

            byte[] Respuesta = AES_Class.decrypt(Encoding.ASCII.GetBytes(textoCifrado), Encoding.ASCII.GetBytes(key));
            rtDown.Text = System.Text.Encoding.Default.GetString(Respuesta);

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }



    }
}
