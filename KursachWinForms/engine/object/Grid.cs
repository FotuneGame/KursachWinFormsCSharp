using Engine.Component;
using Engine.Math;
using System;
using System.Drawing;


namespace Engine.Object
{
    public class Grid: IGrid
    {
        public Color color { get; set; }
        private readonly int widht,height,depth;
        private Transform transform;
        public Grid(int widht,int height,int depth)
        {
            color = Color.Gray;
            this.widht = widht;
            this.height = height;
            this.depth = depth;
            transform = new Transform();
        }

        public Grid(int widht, int height, int depth, Color color)
        {
            this.color = color;
            this.widht = widht;
            this.height = height;
            this.depth = depth;
            transform = new Transform();
        }

        //отрисовка сетки xz
        public void DrawGrid(int[] pixel,Camera camera,double step)
        {
            Vector3 v0 = new Vector3();
            Vector3 v1 = new Vector3();
            int step_load = 10;

            
            for (int i = -step_load; i <= step_load; i++)
            {
                v0.set(step_load * step * camera.zoom - camera.position.x, 0 + camera.position.y, step * i * camera.zoom - camera.position.z);
                v1.set(-step_load * step * camera.zoom - camera.position.x, 0 + camera.position.y, step * i * camera.zoom - camera.position.z);
                transform.Rotate(v0, camera.angle);
                transform.Rotate(v1, camera.angle);
                if (i == 0) Line(v0,v1, pixel, Color.Red);
                else Line(v0, v1, pixel, color);

                v0.set(step * i * camera.zoom - camera.position.x, 0 + camera.position.y, step_load * step * camera.zoom - camera.position.z);
                v1.set(step * i * camera.zoom - camera.position.x, 0 + camera.position.y, -step_load * step * camera.zoom - camera.position.z);
                transform.Rotate(v0, camera.angle);
                transform.Rotate(v1, camera.angle);
                if (i == 0) Line(v0, v1, pixel, Color.Blue);
                else Line(v0, v1, pixel, color);
            }

            v0.set(0 - camera.position.x, step_load * camera.zoom + camera.position.y, 0 - camera.position.z);
            v1.set(0 - camera.position.x, -step_load * camera.zoom + camera.position.y, 0 - camera.position.z);
            transform.Rotate(v0, camera.angle);
            transform.Rotate(v1, camera.angle);
            Line(v0, v1, pixel, Color.Green);

        }

        //отрисовка якоря камеры (точки вращения)
        public void AnchorCamera(int[] pixel, Camera camera, double step)
        {
            Vector3 v0 = new Vector3();
            Vector3 v1 = new Vector3();
            v0.set(1*step * camera.zoom, 0, 0);
            v1.set(-1* step * camera.zoom, 0,0);
            transform.Rotate(v0, camera.angle);
            transform.Rotate(v1, camera.angle);

            Line(v0,v1, pixel, Color.Red);

            v0.set(0,0,1 * step * camera.zoom);
            v1.set(0,0,-1 * step * camera.zoom);
            transform.Rotate(v0, camera.angle);
            transform.Rotate(v1, camera.angle);

            Line(v0, v1, pixel, Color.Blue);

            v0.set(0, 1 * step * camera.zoom, 0);
            v1.set(0, -1 * step * camera.zoom, 0);
            transform.Rotate(v0, camera.angle);
            transform.Rotate(v1, camera.angle);

            Line(v0, v1, pixel, Color.Green);
    }
        
        //отрисовка линии
        private void Line(Vector3 v0,Vector3 v1, int[] pixel,Color color)
        {
            int x0, x1, y0, y1;
            x0 = Convert.ToInt32((v0.x + 1.0) * widht / 2.0);
            y0 = Convert.ToInt32((v0.y + 1.0) * height / 2.0);
            x1 = Convert.ToInt32((v1.x + 1.0) * widht / 2.0);
            y1 = Convert.ToInt32((v1.y + 1.0) * height / 2.0);
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
