using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Algortino_de_lineas.Controllers;

namespace Algortino_de_lineas.Views
{
    public partial class Form_Elipse : Form
    {
        Timer drawTimer = new Timer();
        int i;
        GraficarFigurasController graficoElipse;
        public Form_Elipse()
        {
            InitializeComponent();
            GraficarFigurasController.inicilizarTablaPuntos(dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            i = 1;
            graficoElipse = new GraficarFigurasController(pictureBox1);

            if (!GraficarFigurasController.validateInput(new TextBox[] { ejeMayor, ejeMenor }))
                return;
            graficoElipse.ReadDataElipse(new TextBox[] { ejeMayor, ejeMenor });

            dataGridView1.Rows.Clear();
            drawTimer.Interval = 100;
            drawTimer.Tick -= DrawTimer_Tick;
            drawTimer.Tick += DrawTimer_Tick;
            drawTimer.Start();
        }

       
        private void DrawTimer_Tick(object sender, EventArgs e)
        {
            if (graficoElipse == null) return;
            graficoElipse.DrawNextStep();
            dataGridView1.Rows.Add(i++, graficoElipse.punto.X.ToString("G"), graficoElipse.punto.Y.ToString("G"));
            if (graficoElipse.iterador >= graficoElipse.puntos.Length)
            {
                drawTimer.Stop();
            }
        }

       
    }
}
