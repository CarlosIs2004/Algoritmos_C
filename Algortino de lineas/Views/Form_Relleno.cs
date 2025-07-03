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
    public partial class Form_Relleno : Form
    {

        public bool startAction = false;

        GraficarFigurasController grafico;

        public Form_Relleno()
        {
            InitializeComponent();
            GraficarFigurasController.inicilizarTablaPuntos(dataGridView1);

        }

      

        private void starCircle_Click(object sender, EventArgs e)
        {
            grafico = new GraficarFigurasController(pictureBox1);
            if (!GraficarFigurasController.validateInput(new TextBox[]{ vertices, longitud }) )
                return;

            grafico.ReadDataRellenoFlood(new TextBox[] { vertices, longitud });
            startAction = true;

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left  && startAction)
            {
                int i = 1;
                dataGridView1.Rows.Clear();
                foreach (Point punto in Relleno.Iterative_Flood_Fill(grafico.bufferBitmap, e.X, e.Y, Color.Blue))
                {
                    dataGridView1.Rows.Add(i++, punto.X, punto.Y);


                }
                pictureBox1.Image = grafico.bufferBitmap;


               
            }

        }
  


    }
}
