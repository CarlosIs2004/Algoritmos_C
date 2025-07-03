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
    public partial class BresenHam_Form : Form
    {
        Timer drawTimer = new Timer();
        LineAlgoritm line;
     
        int i;
        public BresenHam_Form()
        {
            InitializeComponent();
            GraficarFigurasController.inicilizarTablaPuntos(dataGridView1);
            this.i = 1;
        }

        

        //private void button2_Click(object sender, EventArgs e)
        //{

        //    i = 1;
        //    PointF startPoint = new PointF(float.Parse(x0.Text), float.Parse(y0.Text));
        //    PointF endPoint = new PointF(float.Parse(x1.Text), float.Parse(y1.Text));
        //    line = new LineAlgoritm(pictureBox1, startPoint, endPoint);

        //    line.initBuffer();
        //    line.bresenhamCalculator();
        //    dataGridView1.Rows.Clear();
        //    drawTimer.Interval = 1000;  // 300 ms entre cada punto
        //    drawTimer.Tick -= DrawTimer_Tick; // Quitar si ya estaba agregado
        //    drawTimer.Tick += DrawTimer_Tick; // Agregar el evento
        //    drawTimer.Start();         // Iniciar animación

        //}
       
        //private void DrawTimer_Tick(object sender, EventArgs e)
        //{
        //    if (line == null) return;

        //    line.DrawNextStep();
        //    dataGridView1.Rows.Add(i++, line.p.X, line.p.Y);
        //    if (line.currentStep >= line.puntos.Count)
        //    {
        //        drawTimer.Stop();
        //    }
        //}


    }
}
