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

        public bool gizmo_draw;
        public readonly int width,height,depth;
        public double step_grid;
        public int[] zbuffer;
        private Grid grid;

        public EngineRender(int[] pixel, int width, int height, int depth,double step_grid,Color backgroud_color,bool gizmo_draw) {
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
            this.gizmo_draw = gizmo_draw;
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

        public void ClearSelectEobjects()
        {
            for (int i = 0; i < EObjects.Count; i++)
            {
                EObjects[i].renderer.select = false;
            }
        }


        public EObject Update(int mouse_x=-1, int mouse_y = -1)
        {
            clear_screen();
            alloc_zbuffer();
            if (gizmo_draw)
            {
                grid.DrawGrid(pixel, MainCamera, step_grid);
                grid.AnchorCamera(pixel, MainCamera, step_grid);
            }

            int z_max_value=int.MinValue;
            EObject selet_mouse_obj = null;
            for (int i = 0; i < EObjects.Count; i++)
            {
                //отрисовка сеткой
                if (gizmo_draw) EObjects[i].renderer.DrawLine(MainCamera, pixel, width, height);
                //отрисовка полигонов
                EObjects[i].renderer.Render(MainCamera,MainLight, pixel, width, depth, height,zbuffer);

                //смотрим по zbuffer какой объект выбран кликом мыши
                if (mouse_x >= 0 && mouse_y >= 0 && mouse_x < width && mouse_y < height)
                {
                    if (zbuffer[mouse_x + mouse_y * width] != int.MinValue)
                    {
                        if (z_max_value < zbuffer[mouse_x + mouse_y * width])
                        {
                            z_max_value = zbuffer[mouse_x + mouse_y * width];
                            selet_mouse_obj = EObjects[i];
                        }
                    }
                }
            }

            // перерисовать выбранный элемент
            if (gizmo_draw && selet_mouse_obj != null)
            {
                selet_mouse_obj.renderer.select = true;
                selet_mouse_obj.renderer.Render(MainCamera, MainLight, pixel, width, depth, height, zbuffer);
            }

            return selet_mouse_obj;
        }
        

    }
}
