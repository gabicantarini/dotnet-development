using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;


namespace Ficheiros
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            DirectoryInfo dirImagens = new DirectoryInfo("C:\\pictures"); 
            FileInfo[] ficheiros;
            ficheiros = dirImagens.GetFiles();
            
            int numFicheiros;
            numFicheiros = ficheiros.Length;
            numericUpDown1.Minimum = 1;
            numericUpDown1 .Maximum = numFicheiros;
            
            int numFichEscolhido;
            numFichEscolhido = (int)numericUpDown1.Value - 1;
            
            string caminhoFichEscolhido;
            caminhoFichEscolhido = ficheiros[numFichEscolhido].FullName;

            object filename = @"""C:\pictures\textoWord.docx""";
            Microsoft.Office.Interop.Word.Application AC = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();
            object readOnly = false;
            object isVisible = true;
            object missing = System.Reflection.Missing.Value;

            try
            {
                pictureBox1.BackgroundImage = Image.FromFile(caminhoFichEscolhido);
                toolTip1.SetToolTip(pictureBox1, caminhoFichEscolhido);

            }
            catch 
            {
                doc = AC.Documents.Open(ref filename);
                doc.Content.Select();
                doc.Content.Copy();
                richTextBox1.Paste();
                toolTip1.SetToolTip(richTextBox1, (string)filename);
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
