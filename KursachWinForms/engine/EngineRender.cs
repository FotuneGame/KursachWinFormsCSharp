using System;
using System.Collections.Generic;
using System.Drawing;
using Engine.Object;
using Engine.Math;

namespace Engine
{
    public class EngineRender : IEngineRender
    {
        public Color backgroud_color;
        public int[] pixel;

        public bool gizmo_draw;
        public readonly int width, height, depth;
        public double step_grid;

        private int[] zbuffer;
        private Grid grid;

        public Camera MainCamera { get; }
        public Light MainLight { get; }
        public List<EObject> EObjects { get; set; }

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


        public EObject RenderAndSelect(int mouse_x=-1, int mouse_y = -1)
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

        public void EditSelectModel(EObject select_obj,int radius, double force, int mouse_x, int mouse_y)
        {
            if (gizmo_draw && select_obj != null)
            {
                List<int> view_points = select_obj.renderer.GetListFacePoints(MainCamera, pixel, width, depth, height, zbuffer,radius,new Vector2(mouse_x, mouse_y));
                select_obj.renderer.model.EditVeritx(view_points, force, select_obj.transform.position- MainCamera.position);
                RenderAndSelect();
            }
        }

    }
}
