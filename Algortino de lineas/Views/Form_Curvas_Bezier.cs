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
    public partial class Form_Curvas_Bezier : Form
    {
        List<PointF> points = new List<PointF>();
        int? puntoSeleccionado = null; // Índice del punto seleccionado
        const float radioSeleccion = 8f; // Sensibilidad para seleccionar el punto
        GraficarFigurasController figuras;
        PointF curva ;
        double valor ;
        public Form_Curvas_Bezier()
        {
            InitializeComponent();
            figuras = new GraficarFigurasController(pictureBox1);
            figuras.DrawGrid();
            
            trackBar1.Minimum = 0;
            trackBar1.Maximum = 100;
            trackBar1.SmallChange = 1;
            trackBar1.LargeChange = 1;
            trackBar1.TickFrequency = 1;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            this.valor = 0;
        }



        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && points.Count < 3)
            {

                points.Add(new PointF(e.X,e.Y));
                pictureBox1.Invalidate();

            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (points.Count >= 3)
            {
                
                e.Graphics.DrawLine(Pens.Black, points[0], points[1]);
                e.Graphics.DrawLine(Pens.Black, points[1], points[2]);
                PointF curva = CurvaBezierCuadratica(valor, points[0], points[1], points[2]);
                e.Graphics.FillRectangle(Brushes.Blue, curva.X, curva.Y, 3, 3);
            }


        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {

            for (int i = 0; i < points.Count; i++)
            {
                float dx = points[i].X - e.X;
                float dy = points[i].Y - e.Y;
                if (dx * dx + dy * dy <= radioSeleccion * radioSeleccion)
                {
                    puntoSeleccionado = i;
                    break;
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

            if (puntoSeleccionado.HasValue && e.Button == MouseButtons.Left)
            {
                points[puntoSeleccionado.Value] = new PointF(e.X, e.Y);
                pictureBox1.Invalidate();
            }
        }


        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            puntoSeleccionado = null;
        }

        public static PointF CurvaBezierCuadratica(double t, PointF p0, PointF p1, PointF p2)
        {
            float x = (float)(Math.Pow(1 - t, 2) * p0.X +
                              2 * (1 - t) * t * p1.X +
                              Math.Pow(t, 2) * p2.X);

            float y = (float)(Math.Pow(1 - t, 2) * p0.Y +
                              2 * (1 - t) * t * p1.Y +
                              Math.Pow(t, 2) * p2.Y);

            return new PointF(x, y);
        }

       

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            valor = trackBar1.Value / 100.0;
            textBox1.Text = valor.ToString("0.00");
            pictureBox1.Invalidate();
        }


        
    }
}
