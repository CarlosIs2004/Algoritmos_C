using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Algortino_de_lineas
{
    internal class DiscretizacionCircunferencia
    {
       
       
        public static Point[] Bresenham_Circle(Point center, int r)
        {
            var Points = new List<Point>();
            int x = 0, y = r;  // (0, r)

            int decision = 3 - (2 * r);

            while (x <= y)
            {
                // Regla Octetos
                Points.Add(new Point(center.X + x, center.Y + y));
                Points.Add(new Point(center.X - x, center.Y + y));
                Points.Add(new Point(center.X - x, center.Y - y));
                Points.Add(new Point(center.X + x, center.Y - y));
                Points.Add(new Point(center.Y + y, center.X + x));
                Points.Add(new Point(center.Y - y, center.X + x));
                Points.Add(new Point(center.Y - y, center.X - x));
                Points.Add(new Point(center.Y + y, center.X - x));

                if (decision < 0)
                {
                    decision += 4 * x + 6;
                }
                else
                {
                    decision += 4 * (x - y) + 10;
                    y -= 1;
                }

                x += 1;
            }

            return Points.ToArray();
        }

        public static Point[] Bresenham_Ellipse(Point center, int rx, int ry)
        {
            var Points = new List<Point>();
            int x = 0, y = ry;
            int rx2 = rx * rx;
            int ry2 = ry * ry;
            int tworx2 = 2 * rx2;
            int twory2 = 2 * ry2;
            int px = 0;
            int py = tworx2 * y;

            // Región 1
            int p = (int)(ry2 - (rx2 * ry) + (0.25 * rx2));
            while (px < py)
            {
                Points.Add(new Point(center.X + x, center.Y + y));
                Points.Add(new Point(center.X - x, center.Y + y));
                Points.Add(new Point(center.X - x, center.Y - y));
                Points.Add(new Point(center.X + x, center.Y - y));

                x++;
                px += twory2;
                if (p < 0)
                {
                    p += ry2 + px;
                }
                else
                {
                    y--;
                    py -= tworx2;
                    p += ry2 + px - py;
                }
            }


            p = (int)(ry2 * (x + 0.5) * (x + 0.5) + rx2 * (y - 1) * (y - 1) - rx2 * ry2);
            while (y >= 0)
            {
                Points.Add(new Point(center.X + x, center.Y + y));
                Points.Add(new Point(center.X - x, center.Y + y));
                Points.Add(new Point(center.X - x, center.Y - y));
                Points.Add(new Point(center.X + x, center.Y - y));

                y--;
                py -= tworx2;
                if (p > 0)
                {
                    p += rx2 - py;
                }
                else
                {
                    x++;
                    px += twory2;
                    p += rx2 - py + px;
                }
            }

            return Points.ToArray();
        }







    }
}
