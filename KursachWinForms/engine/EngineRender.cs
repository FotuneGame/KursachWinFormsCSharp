using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Windows.Forms;
using Engine.Component;
using Engine.Math;
using Engine.Object;

namespace Engine
{
    public class EngineRender
    {
        public Color backgroud_color;
        public int[] pixel;
        public Camera MainCamera;
        public Light MainLight;
        public List<EObject> EObjects;

        
        public readonly int width,height,depth;
        public double step_grid;
        public int[] zbuffer;
        private Grid grid;

        public EngineRender(int[] pixel, int width, int height, int depth,double step_grid,Color backgroud_color) {
            this.width = width;
            this.height = height;
            this.depth = depth;
            this.step_grid = step_grid;
            EObjects =new List<EObject>();
            MainCamera = new Camera();
            MainLight = new Light();
            this.pixel = pixel;
            grid= new Grid(width,height,depth);
            this.backgroud_color = backgroud_color;
        }

        private void alloc_zbuffer()
        {
            //генерим минус бесконечностью
            zbuffer = new int[width * height];
            for(int i = 0; i < width * height; i++)
            {
                zbuffer[i] = int.MinValue;
            }
        }

        private void clear_screen()
        {
            for (int i = 0; i < width * height; i++)
            {
                pixel[i] = backgroud_color.ToArgb();
            }
        }


        public void Update()
        {
            clear_screen();
            alloc_zbuffer();
            grid.DrawGrid(pixel,MainCamera,step_grid);
            grid.AnchorCamera(pixel,MainCamera,step_grid);
            for (int i=0;i<EObjects.Count;i++)
            {
                //отрисовка сеткой
                EObjects[i].renderer.DrawLine(MainCamera,pixel, width, height);
                //отрисовка полигонов
                EObjects[i].renderer.Render(MainCamera,MainLight, pixel, width, depth, height,zbuffer);
            }
        }

        

    }
}
