using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace Informer
{
    public partial class InfoForm : Form
    {
        UdpClient udpClient = new UdpClient(6666);
        //Creates an IPEndPoint to record the IP Address and port number of the sender. 
        // The IPEndPoint will allow you to read datagrams sent from any source.
        IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

        public InfoForm()
        {
            InitializeComponent();

            try
            {
                udpClient.BeginReceive(new AsyncCallback(recv), null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }

        //CallBack
        private void recv(IAsyncResult res)
        {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 6666);
            byte[] received = udpClient.EndReceive(res, ref RemoteIpEndPoint);

            //Process codes
            string recvText = Encoding.UTF8.GetString(received);

            int position = 0;
            int positionStart = 0;
            int positionEnd = 0;
            // Extract sentences from the string.
            position = recvText.IndexOf("<text>", 0);

            if (position >= 0)
            {
                positionStart = position + 6;
                positionEnd = recvText.IndexOf("</text>", positionStart);
                textBox1.Text = recvText.Substring(positionStart, positionEnd - positionStart).Trim();
                  
            }

            position = recvText.IndexOf("<time>", positionEnd);

            if (position >= 0)
            {
                positionStart = position + 6;
                positionEnd = recvText.IndexOf("</time>", positionStart);
                label1.Text = recvText.Substring(positionStart, positionEnd - positionStart).Trim();
            }
 
            FormMax();
            
            /*
            string pattern = @"(text|time)=(*)";
            String[] elements = Regex.Split(recvText, pattern);
            if (elements.Length > 0)
            {
                textBox1.Text = elements[0];
            }*/

            //MessageBox.Show();
            udpClient.BeginReceive(new AsyncCallback(recv), null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void FormMax()
        {
            this.WindowState = FormWindowState.Normal;
            this.Enabled = true;
            notifyIcon1.Visible = false;
        }

        private void FormMin()
        {
            this.WindowState = FormWindowState.Minimized;
            this.Enabled = false;
            notifyIcon1.Visible = true;
        }
    }
}
