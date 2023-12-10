using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Datas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            startDate.Value = monthCalendar1.SelectionStart;
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            int dia, mes, ano;
            string textoData;
            DateTime data;

            if (maskedTextBox1.MaskCompleted == true)
            {
                textoData = maskedTextBox1.Text;
                dia = int.Parse(textoData.Substring(0, 2));
                mes = int.Parse(textoData.Substring(3, 2));
                ano = int.Parse(textoData.Substring(6, 4));
                try
                {
                    data = DateTime.Parse(dia + "-" + mes + "-" + ano);
                    startDate.Value = data;
                    monthCalendar1.SelectionStart = data;
                }
                catch
                {
                    MessageBox.Show("Data incorreta!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
    }
}
