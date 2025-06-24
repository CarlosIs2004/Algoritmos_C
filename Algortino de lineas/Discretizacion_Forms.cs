using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Algortino_de_lineas
{
    public partial class Discretizacion_Forms : Form
    {
        Timer drawTimer = new Timer();

        DiscretizaciónCircunferencia line;
        int i;
        public Discretizacion_Forms()
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
        }

        private void starCircle_Click(object sender, EventArgs e)
        {
            i = 1;
            line = new DiscretizaciónCircunferencia(pictureBox1, float.Parse(radius.Text)); 
            line.initBuffer();
            line.calculaterCircunferencia();

            dataGridView1.Rows.Clear();
            drawTimer.Interval = 1000;  
            drawTimer.Tick -= DrawTimer_Tick; 
            drawTimer.Tick += DrawTimer_Tick; 
            drawTimer.Start();        

        }
        private void DrawTimer_Tick(object sender, EventArgs e)
        {
            if (line == null) return;

            line.DrawNextStep();
            dataGridView1.Rows.Add(i++, line.point.X, line.point.Y);
            dataGridView1.Rows.Add(i++, -line.point.X, line.point.Y);
            dataGridView1.Rows.Add(i++, line.point.X, -line.point.Y);
            dataGridView1.Rows.Add(i++, -line.point.X, -line.point.Y);
            dataGridView1.Rows.Add(i++, line.point.Y, line.point.X);
            dataGridView1.Rows.Add(i++, -line.point.Y, line.point.X);
            dataGridView1.Rows.Add(i++, line.point.Y, -line.point.X);
            dataGridView1.Rows.Add(i++, -line.point.Y, -line.point.X);
            if (line.currentStep >= line.points.Count)
            {
                drawTimer.Stop();
            }
        }
    }
}
