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
    public partial class Form_DiscretizacionCircunferencia : Form
    {
        Timer drawTimer = new Timer();
        GraficarFigurasController grafico;
  
        public Form_DiscretizacionCircunferencia()
        {
            InitializeComponent();
            GraficarFigurasController.inicilizarTablaPuntos(dataGridView1);
        }

        private void starCircle_Click(object sender, EventArgs e)
        {
  
            grafico = new GraficarFigurasController(pictureBox1);

            if (!GraficarFigurasController.validateInput(new TextBox[] { radius })) 
                return;
            grafico.ReadDataCircunferencia(new TextBox[] { radius }); 
                
            dataGridView1.Rows.Clear();
            drawTimer.Interval = 10;  
            drawTimer.Tick -= DrawTimer_Tick; 
            drawTimer.Tick += DrawTimer_Tick; 
            drawTimer.Start();
        }
        private void DrawTimer_Tick(object sender, EventArgs e)
        {
            if (grafico == null) return;
            grafico.DrawNextStep();
            dataGridView1.Rows.Add(grafico.iterador, grafico.punto.X, grafico.punto.Y);
            if (grafico.iterador >= grafico.puntos.Length)
            {
                drawTimer.Stop();
            }
        }
    }
}
