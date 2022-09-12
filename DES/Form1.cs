using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace DES
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string sKey;
        private void button1_Click(object sender, EventArgs e)
        {
            sKey = textBox1.Text;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string source = openFileDialog1.FileName;
                saveFileDialog1.Filter = "des files |*.des";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string destination = saveFileDialog1.FileName;
                    EnccryptFile(source, destination, sKey);
                }
            }
        }

        private void EnccryptFile(string source, string destination, string sKey)
        {
            FileStream fsInput = new FileStream(source,FileMode.Open,FileAccess.Read);
            FileStream fsEncrypted = new FileStream(destination, FileMode.Create, FileAccess.Write);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
           
            try
            {
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                ICryptoTransform desencrypt = DES.CreateEncryptor();
                CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);
                byte[] bytearrInput = new byte[fsInput.Length - 0];
                fsInput.Read(bytearrInput, 0, bytearrInput.Length);
                cryptostream.Write(bytearrInput, 0, bytearrInput.Length);
                cryptostream.Close();
            }
            catch 
            {

                MessageBox.Show("Error");
                return;
            }
            fsInput.Close();
            fsEncrypted.Close();
        }
        private void DeccryptFile(string source, string destination, string sKey)
        {
            FileStream fsInput = new FileStream(source, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypted = new FileStream(destination, FileMode.Create, FileAccess.Write);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            try
            {
                DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
                ICryptoTransform desencrypt = DES.CreateDecryptor();
                CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);
                byte[] bytearrInput = new byte[fsInput.Length - 0];
                fsInput.Read(bytearrInput, 0, bytearrInput.Length);
                cryptostream.Write(bytearrInput, 0, bytearrInput.Length);
                cryptostream.Close();
            }
            catch
            {

                MessageBox.Show("Error");
                return;
            }
            fsInput.Close();
            fsEncrypted.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            sKey = textBox1.Text;
            openFileDialog1.Filter = "des files |*.des";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string source = openFileDialog1.FileName;
                saveFileDialog1.Filter = "txt files |*.txt";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string destination = saveFileDialog1.FileName;
                    DeccryptFile(source, destination, sKey);
                }
            }
        }
    }
}
