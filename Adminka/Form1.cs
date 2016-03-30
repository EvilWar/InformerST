using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Adminka
{
    public partial class Form1 : Form
    {

        UdpClient udpClient1 = new UdpClient(6666);
        public Form1()
        {
            InitializeComponent();
            udpClient1.Connect(IPAddress.Broadcast, 6666);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tme = "";
            string txt = "";
            string tmp = "";

            if (numericUpDown1.Value != 0)
            {
                tme = "<time>" + numericUpDown1.Value.ToString()+"</time>";
            }

            if (textBox1.TextLength > 0)
            {
                txt = "<text>" + textBox1.Text + "</text>";
            }

            tmp = txt + tme;

            if (tmp.Length > 0)
            {
                Byte[] sendBytes = Encoding.UTF8.GetBytes(txt + tme);
                udpClient1.Send(sendBytes, sendBytes.Length);
            }
            else
            {
                MessageBox.Show("Нечего отправлять!");
            }

            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            udpClient1.Close();
        }
    }
}
