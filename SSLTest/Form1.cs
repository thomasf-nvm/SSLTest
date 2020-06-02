using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace SSLTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = $"{Text} (.NET {Environment.Version})";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoRequest("GET");
        }

        private void DoRequest(string verb) 
        {
            button1.Enabled = button2.Enabled = false;
            body.Text = statusCode.Text = "";
            textBox1.Text = textBox1.Text.Trim();
            if (!textBox1.Text.StartsWith("https://"))
            {
                textBox1.Text = $"https://{textBox1.Text}";
            }

            try
            {
                var request = WebRequest.Create(textBox1.Text);
                request.Method = verb;
                request.Timeout = 10000;
                var response = (HttpWebResponse)request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());
                body.Text = reader.ReadToEnd();
                statusCode.Text = $"{(int)response.StatusCode} {response.StatusCode}";
            }
            catch (Exception ex)
            {
                body.Text = ex.GetBaseException().Message;
            }
            finally
            {
                button1.Enabled = button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DoRequest("POST");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // tls 1.2
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768; // tls 1.1
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)192; // tls 1.0
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)12288; // tls 1.3
            }
            catch (Exception ex)
            {
                body.Text = ex.Message;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)0; // system default
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)48; // SSL 3.0
        }
    }
}
