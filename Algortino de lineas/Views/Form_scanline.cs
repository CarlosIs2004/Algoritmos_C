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

namespace Algortino_de_lineas.Views
{
    public partial class Form_scanline : Form
    {
        public bool startAction = false;

        GraficarFigurasController graficoScanline;
        public Form_scanline()
        {
            InitializeComponent();
            GraficarFigurasController.inicilizarTablaPuntos(dataGridView1);
        }

        private void starCircle_Click(object sender, EventArgs e)
        {
            graficoScanline = new GraficarFigurasController(pictureBox1);
            if (!GraficarFigurasController.validateInput(new TextBox[] { vertices, longitud }))
                return;
            int i = 0;
            graficoScanline.ReadDataRellenoScanline(new TextBox[] { vertices, longitud });
            dataGridView1.Rows.Clear();
            foreach (Point punto in graficoScanline.puntos)
            {
                dataGridView1.Rows.Add(i++, punto.X, punto.Y);


            }
        }
    }
}
