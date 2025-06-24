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
    public partial class Menu_Form : Form
    {
        Form fn;
        public Menu_Form()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (fn == null) { 
                 openChildForm(new DDA_Form1());
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
            openChildForm(new DDA_Form1());

            
        }

        private void bresenhamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new BresenHam_Form());
        }

        private void circunfenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new Discretizacion_Forms());
        }

       

        private void rellenoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openChildForm(new Form_Relleno());
        }
    }
}
