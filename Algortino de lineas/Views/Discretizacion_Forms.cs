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

namespace Algortino_de_lineas
{
    public partial class Discretizacion_Forms : Form
    {
        Timer drawTimer = new Timer();
        GraficarFigurasController grafico;
        int i;
        public Discretizacion_Forms()
        {
            InitializeComponent();
            GraficarFigurasController.inicilizarTablaPuntos(dataGridView1);
        }

        private void starCircle_Click(object sender, EventArgs e)
        {
            i = 1;
            grafico = new GraficarFigurasController(pictureBox1);

            if (!GraficarFigurasController.validateInput(new TextBox[] { radius })) 
                return;
            grafico.ReadDataCircunferencia(new TextBox[] { radius }); 
                
            dataGridView1.Rows.Clear();
            drawTimer.Interval = 100;  
            drawTimer.Tick -= DrawTimer_Tick; 
            drawTimer.Tick += DrawTimer_Tick; 
            drawTimer.Start();
        }
        private void DrawTimer_Tick(object sender, EventArgs e)
        {
            if (grafico == null) return;
            grafico.DrawNextStep();
            dataGridView1.Rows.Add(i++, grafico.punto.X.ToString("G"), grafico.punto.Y.ToString("G"));
            if (grafico.iterador >= grafico.puntos.Length)
            {
                drawTimer.Stop();
            }
        }
    }
}
