using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AES
{
    public partial class Form1 : Form
    {

        byte[] Cifrado, decCifrado, bytesAGuardar;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCifrar_Click(object sender, EventArgs e)
        {
            try
            {
             
                string textoClaro = string.Empty;
                bool flag = false;

                textoClaro = rtUp.Text;

                String key = tbKey.Text;

                if (cbKey.Text.Contains("128"))
                {
                    if (tbKey.TextLength == 16)
                        flag = true;
                    else
                        MessageBox.Show("El tamaño de llave para 128 bits es de 16 caracteres.");
                }
                if (cbKey.Text.Contains("192"))
                {
                    if (tbKey.TextLength == 24)
                        flag = true;
                    else
                        MessageBox.Show("El tamaño de llave para 192 bits es de 24 caracteres.");
                }
                if (cbKey.Text.Contains("256"))
                {
                    if (tbKey.TextLength == 32)
                        flag = true;
                    else
                        MessageBox.Show("El tamaño de llave para 256 bits es de 32 caracteres.");
                }

                if (flag)
                { 
                    Cifrado = AES_Class.encrypt(Encoding.ASCII.GetBytes(textoClaro), Encoding.ASCII.GetBytes(key));
                    bytesAGuardar = Cifrado;
               
                    rtDown.Text = System.Text.Encoding.Default.GetString(Cifrado);
                }
                

            }
            catch (Exception ex)
            {
                
                throw new Exception (ex.Message);
            }
        }
        public bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
        {
            try
            {
                // Open file for reading
                System.IO.FileStream _FileStream =
                   new System.IO.FileStream(_FileName, System.IO.FileMode.Create,
                                            System.IO.FileAccess.Write);
                // Writes a block of bytes to this stream using data from
                // a byte array.
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                // close file stream
                _FileStream.Close();

                return true;
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}",
                                  _Exception.ToString());
            }

            // error occured, return false
            return false;
        }
        private void btnDescifrar_Click(object sender, EventArgs e)
        {

            bool flag = false;

            if (cbKey.Text.Contains("128"))
            {
                if (tbKey.TextLength == 16)
                    flag = true;
                else
                    MessageBox.Show("El tamaño de llave para 128 bits debe ser de 16 caracteres.");
            }
            if (cbKey.Text.Contains("192"))
            {
                if (tbKey.TextLength == 24)
                    flag = true;
                else
                    MessageBox.Show("El tamaño de llave para 192 bits debe ser de 24 caracteres.");
            }
            if (cbKey.Text.Contains("256"))
            {
                if (tbKey.TextLength == 32)
                    flag = true;
                else
                    MessageBox.Show("El tamaño de llave para 256 bits debe ser de 32 caracteres.");
            }

            if (flag)
            { 
                String textoCifrado = rtUp.Text;
                String key = tbKey.Text;
                byte[] bytesFromHexa = GetBytesFromFile(txtFile.Text);

                byte[] Respuesta = AES_Class.decrypt(bytesFromHexa, Encoding.ASCII.GetBytes(key));
                bytesAGuardar = Respuesta;
                rtDown.Text = System.Text.Encoding.Default.GetString(Respuesta);
            }
        }

         public static byte[] GetBytesFromFile(string fullFilePath)
        {
            // this method is limited to 2^32 byte files (4.2 GB)

            FileStream fs = File.OpenRead(fullFilePath);
            try
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                return bytes;
            }
            finally
            {
                fs.Close();
            }

        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txtFile.Text = openFileDialog1.FileName;
                    rtUp.Text = File.ReadAllText(@txtFile.Text);
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                string path = saveFileDialog1.FileName;


               
                ByteArrayToFile(@saveFileDialog1.FileName + ".txt", Cifrado);
                MessageBox.Show("Archivo guardado con éxito!.");

            }
        }
    }
}
