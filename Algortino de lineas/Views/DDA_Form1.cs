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

namespace Algortino_de_lineas
{
    public partial class DDA_Form1 : Form
    {
        Timer drawTimer = new Timer();
   
        LineAlgoritm line;
        int i;
        public DDA_Form1()
        {
            InitializeComponent();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;

            dataGridView1.RowHeadersVisible = false;

            dataGridView1.ColumnCount = 3;
            dataGridView1.Columns[0].Name = "#";
            dataGridView1.Columns[0].Width = 25;

            int widthColumn = (dataGridView1.Width - dataGridView1.Columns[0].Width) / 2;

            dataGridView1.Columns[1].Name = "X";
            dataGridView1.Columns[1].Width = widthColumn;
            dataGridView1.Columns[2].Name = "Y";
            dataGridView1.Columns[2].Width = widthColumn;
            i = 1;

        }


       
        private void DrawTimer_Tick(object sender, EventArgs e)
        {
            if (line == null) return;

            line.DrawNextStep();
            dataGridView1.Rows.Add(i++,line.p.X, line.p.Y);
            if (line.currentStep >= line.puntos.Count)
            {
                drawTimer.Stop(); 
            }
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            
                i = 1;
                PointF startPoint = new PointF(float.Parse(x0.Text), float.Parse(y0.Text));
                PointF endPoint = new PointF(float.Parse(x1.Text), float.Parse(y1.Text));
                line = new LineAlgoritm(pictureBox1, startPoint, endPoint);

                line.initBuffer();
                line.ddaCalculator();
                dataGridView1.Rows.Clear();
                drawTimer.Interval = 1000;  // 300 ms entre cada punto
                drawTimer.Tick -= DrawTimer_Tick; // Quitar si ya estaba agregado
                drawTimer.Tick += DrawTimer_Tick; // Agregar el evento
                drawTimer.Start();         // Iniciar animación

        }

    


     
    }
}
