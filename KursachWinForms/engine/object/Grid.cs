using Engine.Component;
using Engine.Math;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Engine.Object
{
    public class Grid
    {
        public Color color;
        private readonly int widht,height,depth;
        private Transform transform;
        double step;
        public Grid(int widht,int height,int depth,double step)
        {
            color = Color.Gray;
            this.widht = widht;
            this.height = height;
            this.depth = depth;
            this.step = step;
            transform = new Transform();
        }

        public Grid(int widht, int height, int depth, double step, Color color)
        {
            this.color = color;
            this.widht = widht;
            this.height = height;
            this.depth = depth;
            this.step = step;
            transform = new Transform();
        }

        public void DrawGrid(int[] pixel,Camera camera)
        {
            Vector3 v0 = new Vector3();
            Vector3 v1 = new Vector3();
            int x0, x1, y0, y1;

            for (int i = -50; i < 50; i++)
            {
                v0.set(10, step * i, 0);
                v1.set(-10, step * i, 0);
                transform.Rotate(v0, camera.angle);
                transform.Rotate(v1, camera.angle);

                x0 = Convert.ToInt32((v0.x + 1.0) * widht / 2.0);
                y0 = Convert.ToInt32((v0.y + 1.0) * height / 2.0);
                x1 = Convert.ToInt32((v1.x + 1.0) * widht / 2.0);
                y1 = Convert.ToInt32((v1.y + 1.0) * height / 2.0);

                if(i==0)Line(x0, y0, x1, y1, pixel, Color.Red);
                else Line(x0, y0, x1, y1, pixel, color);


                v0.set(step * i, 10, 0);
                v1.set(step * i, -10, 0);
                transform.Rotate(v0, camera.angle);
                transform.Rotate(v1, camera.angle);

                x0 = Convert.ToInt32((v0.x + 1.0) * widht / 2.0);
                y0 = Convert.ToInt32((v0.y + 1.0) * height / 2.0);
                x1 = Convert.ToInt32((v1.x + 1.0) * widht / 2.0);
                y1 = Convert.ToInt32((v1.y + 1.0) * height / 2.0);

                if (i == 0) Line(x0, y0, x1, y1, pixel, Color.Green);
                else Line(x0, y0, x1, y1, pixel, color);

                v0.set(0, step * i, 10);
                v1.set(0, step * i, -10);
                transform.Rotate(v0, camera.angle);
                transform.Rotate(v1, camera.angle);

                x0 = Convert.ToInt32((v0.x + 1.0) * widht / 2.0);
                y0 = Convert.ToInt32((v0.y + 1.0) * height / 2.0);
                x1 = Convert.ToInt32((v1.x + 1.0) * widht / 2.0);
                y1 = Convert.ToInt32((v1.y + 1.0) * height / 2.0);

                if (i == 0) Line(x0, y0, x1, y1, pixel, Color.Blue);
                else Line(x0, y0, x1, y1, pixel, color);

            }

        }

        //отрисовка линии
        private void Line(int x0, int y0, int x1, int y1, int[] pixel,Color color)
        {
            bool steep = false;
            if (System.Math.Abs(x0 - x1) < System.Math.Abs(y0 - y1))
            {
                (x0, y0) = (y0, x0);
                (x1, y1) = (y1, x1);
                steep = true;
            }
            if (x0 > x1)
            {
                (x0, x1) = (x1, x0);
                (y0, y1) = (y1, y0);
            }
            int dx = x1 - x0;
            int dy = y1 - y0;
            int derror2 = System.Math.Abs(dy) * 2;
            int error2 = 0;
            int y = y0;
            for (int x = x0; x <= x1; x++)
            {
                if (steep)
                {
                    if (x >= 0 && y >= 0 && x < widht && y < height && (y + x * widht) < (widht * height))
                    {
                        pixel[y + widht * x] = color.ToArgb();
                    }
                }
                else
                {
                    if (x >= 0 && y >= 0 && x < widht && y < height && (x + y * widht) < (widht * height))
                    {
                        pixel[x + y * widht] = color.ToArgb();
                    }
                }
                error2 += derror2;

                if (error2 > dx)
                {
                    y += (y1 > y0 ? 1 : -1);
                    error2 -= dx * 2;
                }
            }

        }

    }
}
