using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using System.IO;

namespace TGBot.Classes.telegramBot
{
    internal class MakeGrapg
    {
        decimal[] data = new decimal[108];// входное, максимальноеб минимальное выходное


        public void DrawGraph(decimal[] i, decimal[] r)
        {
            data = i;
            
            Bitmap b = new Bitmap(1024, 800);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.Clear(Color.White);
                Dr(g, r);
            }
            try
            {
                b.Save(Path() + @"/1.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            }
            catch
            {
                Console.WriteLine("Ошибка создания картинки");
            }
        }

        void Dr(Graphics g, decimal[] r)
        {
            int stepX = (1024 - (10)) / (data.Length / 4) - 2;
            decimal ma = Math.Max(data.Max(), r.Max());
            decimal mi = Math.Min(data.Min(), r.Min());
            int x = 5;
            int xl;
            Pen p;
            for (int i = 0; i < data.Length; i += 4)
            {
                int yl0;
                int y;
                int yl1;
                int height;
                if (data[i] < data[i + 3])
                {
                    p = new Pen(Color.Green);
                    y = Convert.ToInt32(50 + (700 / (ma - mi)) * (ma - data[i + 3]));
                    yl0 = Convert.ToInt32(50 + (700 / (ma - mi)) * (ma - data[i + 1]));
                    yl1 = yl0 + Convert.ToInt32((700 / (ma - mi)) * (data[i + 1] - data[i + 2]));
                    height = Convert.ToInt32((700 / (ma - mi)) * Math.Abs(data[i + 3] - data[i]));
                }
                else
                {
                    p = new Pen(Color.Red);
                    y = Convert.ToInt32(50 + (700 / (ma - mi)) * (ma - data[i]));
                    yl0 = Convert.ToInt32(50 + (700 / (ma - mi)) * (ma - data[i + 1]));
                    yl1 = yl0 + Convert.ToInt32((700 / (ma - mi)) * Math.Abs(data[i + 1] - data[i + 2]));
                    height = Convert.ToInt32((700 / (ma - mi)) * Math.Abs(data[i + 3] - data[i]));
                }
                xl = x + stepX / 2;
                g.DrawLine(p, xl, yl0, xl, yl1);
                g.DrawRectangle(p, new Rectangle(x, y, stepX, height));
                x += stepX + 2;
            }

            x = 1024 - 6 - (r.Length / 4) * (stepX + 2);
            p = new Pen(Color.Black);
            for (int i = 0; i < r.Length; i += 4)
            {
                int y = Convert.ToInt32(50 + (700 / (ma - mi)) * (ma - Math.Max(r[i + 3], r[i])));
                int height = Convert.ToInt32((700 / (ma - mi)) * Math.Abs(r[i + 3] - r[i]));
                xl = x + stepX / 2 + 1;
                int yl0 = Convert.ToInt32(50 + (700 / (ma - mi)) * (ma - r[i + 1]));
                int yl1 = yl0 + Convert.ToInt32((700 / (ma - mi)) * Math.Abs(r[i + 1] - r[i + 2]));

                //Console.WriteLine($"{xl} {yl0} {xl} {yl1}");
                g.DrawLine(p, xl, yl0, xl, yl1);
                g.DrawRectangle(p, new Rectangle(x, y, stepX, height));
                x += stepX + 2;
            }


        }
        string Path()
        {
            string path = Directory.GetCurrentDirectory();
            return path;
        }
    }
}
