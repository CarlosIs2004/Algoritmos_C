using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Algortino_de_lineas.Controllers;

namespace Algortino_de_lineas
{
    public partial class DDA_Form1 : Form
    {
      
        Timer drawTimer = new Timer();
        GraficarFigurasController grafico;
        List<Point> lines;
        int iter = 0;
        public DDA_Form1()
        {
            InitializeComponent();
            GraficarFigurasController.inicilizarTablaPuntos(dataGridView1);
            lines = new List<Point>(); 
            

        }
 

        private void DrawTimer_Tick(object sender, EventArgs e)
        {
            if (grafico == null) return;
            if (lines.Count % 2 == 0 && lines.Count > 0)
            {
                
                grafico.DrawNextStep2();
                dataGridView1.Rows.Add(grafico.iterador, grafico.punto.X, grafico.punto.Y);
                if (grafico.iterador >= grafico.puntos.Length)
                {
                    drawTimer.Stop();
                }
            }
        }


        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                lines.Add(new Point(e.X, e.Y ));
            }

            if (lines.Count % 2 == 0 && lines.Count > 0)
            {

                grafico = new GraficarFigurasController(pictureBox1);
                grafico.DibujarDDa(lines[iter], lines[iter + 1]);
                iter += 2;
                dataGridView1.Rows.Clear();
                drawTimer.Interval = 1;
                drawTimer.Tick -= DrawTimer_Tick;
                drawTimer.Tick += DrawTimer_Tick;
                drawTimer.Start();
            }
        }

       
    }
}
