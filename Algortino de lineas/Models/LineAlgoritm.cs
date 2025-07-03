using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Algortino_de_lineas
{
    internal class LineAlgoritm
    {

        public static Point[] DDA_Line(Point start, Point end)
        {
            // Algoritmo para graficar líneas
            var Points = new List<Point>();

            int dX = end.X - start.X;  // Delta X
            int dY = end.Y - start.Y;  // Delta Y
            int steps = Math.Max(Math.Abs(dX), Math.Abs(dY));

            float xIncrement = (float)dX / steps;
            float yIncrement = (float)dY / steps;

            float x = start.X;
            float y = start.Y;

            for (int i = 0; i <= steps; i++)  // Also the last one
            {
                Points.Add(new Point((int)Math.Round(x), (int)Math.Round(y)));
                x += xIncrement;
                y += yIncrement;
            }

            return Points.ToArray();
        }

        public static Point[] Bresenham_Line(Point start, Point end)
        {
            // Algoritmo para graficar líneas
            var Points = new List<Point>();

            int x0 = start.X, y0 = start.Y;  // P1
            int x1 = end.X, y1 = end.Y;  // P2

            int dx = Math.Abs(x1 - x0);  // Delta X
            int dy = Math.Abs(y1 - y0);  // Delta Y

            int sx = (x0 < x1) ? 1 : -1;  // X Direction
            int sy = (y0 < y1) ? 1 : -1;  // Y Direction

            int err = dx - dy;

            while (true)
            {
                Points.Add(new Point(x0, y0));

                if (x0 == x1 && y0 == y1)
                    break;

                int e2 = 2 * err;  // Error precalculado

                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }

                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }

            return Points.ToArray();
        }

    }
}
