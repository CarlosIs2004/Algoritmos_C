using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Algortino_de_lineas.Controllers;

namespace Algortino_de_lineas.Views
{
    public partial class Form_CohenSutherland : Form
    {
        GraficarFigurasController GraficosArea;
        PointF[] pointsArea;
        List<PointF> points;
        PointF[] nuevosPuntos;
        bool dibujar;

        
        public Form_CohenSutherland()
        {
            InitializeComponent();
            GraficosArea = new GraficarFigurasController(pictureBox1);
            points = new List<PointF>();
            nuevosPuntos = new PointF[] { };
            trackBar1.Minimum = 1;      
            trackBar1.Maximum = 50;
            this.dibujar = false;
        }

       


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int valor = (int)trackBar1.Value;
            pointsArea = GraficosArea.CalcularAreaVisible(valor);
           
        }



        

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                points.Add(new PointF(e.X, e.Y));
                
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (points.Count >= 2)
            {
                nuevosPuntos = GraficosArea.Graficarlinea(points, pointsArea);
                dibujar = true;
            }

            if (dibujar)
            {
               
                for (int i = 0; i < (nuevosPuntos.Length - 1); i+=2)
                {
                    //e.Graphics.DrawLine(new Pen(Color.FromArgb(100, 255, 0, 0), 2) , points[i], points[i + 1]);
                    e.Graphics.DrawLine(new Pen(Color.FromArgb(255, 0, 0, 0), 2), nuevosPuntos[i], nuevosPuntos[i + 1]);

                }
            }

        }
    }
}
