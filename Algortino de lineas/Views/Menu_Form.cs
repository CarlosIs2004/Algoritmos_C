using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Algortino_de_lineas.Views;

namespace Algortino_de_lineas
{
    public partial class Menu_Form : Form
    {
        Form fn;
        public Menu_Form()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (fn == null) { 
                 openChildForm(new Form_DDA());
            }

        }
        private void openChildForm(object childForm)
        {
            if (this.panel1.Controls.Count > 0)
                this.panel1.Controls.RemoveAt(0);
            fn = childForm as Form;
            fn.TopLevel = false;
            fn.Dock = DockStyle.Fill;
            this.panel1.Controls.Add(fn);
            this.panel1.Tag = fn;
            fn.Show();

        }

        private void dDAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new Form_DDA());

            
        }

        private void bresenhamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new Form_BresenHam());
        }


  

        private void circunferenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new Form_DiscretizacionCircunferencia());
        }

        private void elipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new Form_Elipse());
        }

        private void floodFillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new Form_Relleno());
        }

        private void scanlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new Form_scanline());

        }

        private void coToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new Form_CohenSutherland());
        }

        private void sutherlandHodgmanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new Form_Sutherland_Hodgman());
        }


        private void curvasDeBézierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new Form_Curvas_Bezier());
        }

        private void bsplinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new Form_B_splines());
        }
    }
}
