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
using Algortino_de_lineas.Models;

namespace Algortino_de_lineas.Views
{
    public partial class Form_Sutherland_Hodgman : Form
    {
        GraficarFigurasController GraficosArea;
        PointF[] pointsArea;
        List<PointF> points;
        PointF[] nuevosPuntos;
        bool dibujar;
        bool capturarPuntos;
        public Form_Sutherland_Hodgman()
        {
            InitializeComponent();
            GraficosArea = new GraficarFigurasController(pictureBox1);
            points = new List<PointF>();
            nuevosPuntos = new PointF[] { };
            trackBar1.Minimum = 1;
            trackBar1.Maximum = 50;
            this.dibujar = true;
            this.capturarPuntos = true;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int valor = (int)trackBar1.Value;
            pointsArea = GraficosArea.CalcularAreaVisible(valor);
            
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Left && capturarPuntos)
            {
                points.Add(new PointF(e.X, e.Y));
                pictureBox1.Invalidate();
                
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (points.Count >= 3 ) {
                
                e.Graphics.DrawPolygon(Pens.Blue, points.ToArray());
                nuevosPuntos = RecorteGeometrico.SutherlandHodgmanClipping(points, pointsArea);
                e.Graphics.DrawPolygon(Pens.Red, nuevosPuntos);
            }
        }
    }
}
