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

namespace Algortino_de_lineas.Views
{
    public partial class Form_B_splines : Form
    {
        List<PointF> controlPoints;
        int? puntoSeleccionado = null; // Índice del punto seleccionado
        const float radioSeleccion = 8f;
        bool dentro;
        public Form_B_splines()
        {
            InitializeComponent();
            this.controlPoints = new List<PointF>();
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            this.dentro = false;
        }


        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (controlPoints.Count < 4) return;

            List<PointF> curvePoints = new List<PointF>();
            int steps = 100;
            float maxT = controlPoints.Count - 3;

            for (int i = 0; i < steps; i++)
            {
                float t = (maxT) * i / (steps - 1);
                curvePoints.Add(BSplinePoint(t, controlPoints));
            }

            // Dibuja la curva
            for (int i = 0; i < curvePoints.Count - 1; i++)
            {
                e.Graphics.DrawLine(Pens.Blue, curvePoints[i], curvePoints[i + 1]);
            }

            // Opcional: dibuja los puntos de control
            foreach (var pt in controlPoints)
                e.Graphics.FillEllipse(Brushes.Red, pt.X - 3, pt.Y - 3, 6, 6);
        }


        // Calcula un punto de la B-spline cúbica usando la base uniforme
        private PointF BSplinePoint(float t, List<PointF> pts)
        {
            int n = pts.Count - 1;
            // Para cúbica, necesitamos al menos 4 puntos
            if (n < 3) return PointF.Empty;

            // Encuentra el segmento correspondiente
            int k = (int)Math.Floor(t);
            if (k > n - 3) k = n - 3;

            float localT = t - k;

            // Puntos de control relevantes
            PointF p0 = pts[k];
            PointF p1 = pts[k + 1];
            PointF p2 = pts[k + 2];
            PointF p3 = pts[k + 3];

            // Fórmulas de la base B-spline cúbica uniforme
            float b0 = ((1 - localT) * (1 - localT) * (1 - localT)) / 6f;
            float b1 = (3 * localT * localT * localT - 6 * localT * localT + 4) / 6f;
            float b2 = (-3 * localT * localT * localT + 3 * localT * localT + 3 * localT + 1) / 6f;
            float b3 = (localT * localT * localT) / 6f;

            float x = b0 * p0.X + b1 * p1.X + b2 * p2.X + b3 * p3.X;
            float y = b0 * p0.Y + b1 * p1.Y + b2 * p2.Y + b3 * p3.Y;

            return new PointF(x, y);
        }



        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && dentro == false )
            {

                controlPoints.Add(new PointF(e.X, e.Y));
                pictureBox1.Invalidate();

            }
        }


        private  void pictureBox1_MouseDown(object sender, MouseEventArgs e) 
        {

            for (int i = 0; i < controlPoints.Count; i++)
            {
                float dx = controlPoints[i].X - e.X;
                float dy = controlPoints[i].Y - e.Y;
                if (dx * dx + dy * dy <= radioSeleccion * radioSeleccion)
                {
                    dentro = true;
                    puntoSeleccionado = i;
                    break;
                }
                else {
                    dentro = false;
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

            if (puntoSeleccionado.HasValue && e.Button == MouseButtons.Left)
            {
                controlPoints[puntoSeleccionado.Value] = new PointF(e.X, e.Y);
                pictureBox1.Invalidate();
            }
        }


        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            puntoSeleccionado = null;
        }

     
    }
}
