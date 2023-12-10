using System;
using System.CodeDom;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Sites
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void LabelMouseHover(object sender, EventArgs e)
        {
            linkLabel1.BackColor = Color.Orange;
            linkLabel2.BackColor = Color.Orange;
            linkLabel3.BackColor = Color.Orange;

        }
        private void LabelMouseLeave(object sender, EventArgs e)
        {
            linkLabel1.BackColor = Color.Empty;
            linkLabel2.BackColor = Color.Empty;
            linkLabel3.BackColor = Color.Empty;
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            webBrowser1.Navigate("https://fca.pt");            

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            webBrowser1.Navigate("https://www.microsoft.com/");
            
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            webBrowser1.Navigate("https://www.google.com");

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int larguraForm;
            larguraForm = this.Width;
            splitContainer1.Panel1MinSize = 200;
            splitContainer1.Panel2MinSize = larguraForm - 200;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
