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
    public partial class Form_Relleno : Form
    {
        public List<Point> Points;
        public bool startAction = false;
        public Relleno line;
        Timer drawTimer = new Timer();
        public Form_Relleno()
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
            Points = new List<Point>();

            drawTimer.Interval = 1000;
           
            drawTimer.Start();
        }

      

        private void starCircle_Click(object sender, EventArgs e)
        {
            line = new Relleno(pictureBox1);

            line.polygon(int.Parse(vertices.Text), int.Parse(longitud.Text));
            startAction = true;

             pictureBox1.Image = line.bufferBitmap;

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Points.Count < 1 && startAction)
            {
                int i = 1;
                foreach (Point punto in Relleno.Iterative_Flood_Fill(line.bufferBitmap, e.X, e.Y, Color.Blue))
                {
                    dataGridView1.Rows.Add(i++, punto.X, punto.Y);


                }
                pictureBox1.Image = line.bufferBitmap;


               
            }
                
            
        }
        private void DrawTimer_Tick(object sender, EventArgs e)
        {



        }


    }
}
