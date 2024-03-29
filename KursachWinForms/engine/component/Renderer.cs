﻿using Engine.Math;
using Engine.Object;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Engine.Component
{
    public class Renderer: IRenderer
    {
        // можно потом попробовать сделать с текстурой
        private Transform transofrm;
        public Color color { get; set; }
        public Model model { get; set; }
        public bool select { get; set; }



        public Renderer(Transform transform)
        {
            this.transofrm = transform;
            color = Color.Blue;
            model = new Model();
            select = false;
        }
        public Renderer(Transform transform, Color materialColor)
        {
            this.transofrm = transform;
            color = materialColor;
            model = new Model();
            select = false;
        }
        public Renderer(Transform transform, Color materialColor, Model modelObject)
        {
            this.transofrm = transform;
            color = materialColor;
            model = modelObject;
            select = false;
        }

        //все вершины видимые камере вершины в диапазоне кисти
        public List<int> GetListFacePoints(Camera camera, int[] pixel, int width, int height, int depth, int[] zbuffer,int radius,Vector2 mouse_pos)
        {
            Vector3 v0;
            List<int> result = new List<int>();
            for (int i = 0; i < model.points.Count; i++)
            {
                v0 = model.points[i] * transofrm.size * camera.zoom;
                transofrm.Rotate(v0);
                v0 = v0 + transofrm.position;
                //поворот объекта для камеры и затем перемещение относительно камеры
                v0 = v0 - new Vector3(camera.position.x, -camera.position.y, camera.position.z);
                transofrm.Rotate(v0, camera.angle);

                // преобразуем в координаты экрана
                int x0 = Convert.ToInt32((v0.x + 1.0) * width / 2.0);
                int y0 = Convert.ToInt32((v0.y + 1.0) * height / 2.0);
                int z0 = Convert.ToInt32((v0.z + 1) * depth / 2.0);

                //проверка на вхождение экранных координат вершины в диапозон кисти
                if (x0 < (mouse_pos.x - radius) || x0 > (mouse_pos.x + radius)) continue;
                if (y0 < (mouse_pos.y - radius) || y0 > (mouse_pos.y + radius)) continue;

                if ((x0 + y0 * width) >= 0 && (x0 + y0 * width) < width * height )
                {
                    pixel[Convert.ToInt32(x0 + y0 * width)] = Color.Green.ToArgb();
                    if (z0 == zbuffer[Convert.ToInt32(x0 + y0 * width)])
                    {
                        pixel[Convert.ToInt32(x0 + y0 * width)] = Color.Blue.ToArgb();
                        result.Add(i);
                    }
                }
            }
            return result;
        }

        //отрисовка модели линиями
        public void DrawLine(Camera camera, int[] pixel, int width, int height)
        {
            Vector3 v0, v1;
            for (int i = 0; i < model.triangle.Count; i++)
            {

                int[] face = model.triangle[i];
                for (int j = 0; j < 3; j++)
                {

                    v0 = model.points[face[j]] * transofrm.size * camera.zoom;
                    v1 = model.points[face[(j + 1) % 3]] * transofrm.size * camera.zoom;
                    transofrm.Rotate(v0);
                    transofrm.Rotate(v1);
                    v0 = v0 + transofrm.position * camera.zoom;
                    v1 = v1 + transofrm.position * camera.zoom;
                    //поворот объекта для камеры и затем перемещение относительно камеры
                    v0 = v0 - new Vector3(camera.position.x,-camera.position.y,camera.position.z);
                    v1 = v1 - new Vector3(camera.position.x, -camera.position.y, camera.position.z);
                    transofrm.Rotate(v0, camera.angle);
                    transofrm.Rotate(v1, camera.angle);

                    int x0 = Convert.ToInt32((v0.x + 1.0) * width / 2.0);
                    int y0 = Convert.ToInt32((v0.y + 1.0) * height / 2.0);
                    int x1 = Convert.ToInt32((v1.x + 1.0) * width / 2.0);
                    int y1 = Convert.ToInt32((v1.y + 1.0) * height / 2.0);

                    
                    // не работаем с линиями вне экрана если они вне 2-ого размера экрана
                    if ((x1 < -width || x1 > width* width) && (x0 < -width || x0 > width * width)) continue;
                    if ((y1 < -height || y1 > height* height) && (y0 < -height || y0 > height* height)) continue;
                    

                    //рисование линии
                    line(x0, y0, x1, y1, pixel, width, height);
                }
            }
        }

        //отрисовка модели
        public void Render(Camera camera, Light light, int[] pixel, int width, int height, int depth, int[] zbuffer)
        {
            //для реализации zbufer необходимо производить попиксельную отрисовку
            Vector3 v0, v1, v2;
            for (int point_i = 0; point_i < model.triangle.Count; point_i++)
            {

                int[] face = model.triangle[point_i];

                v0 = model.points[face[0]] * transofrm.size * camera.zoom;
                v1 = model.points[face[1]] * transofrm.size * camera.zoom;
                v2 = model.points[face[2]] * transofrm.size * camera.zoom;
                transofrm.Rotate(v0);
                transofrm.Rotate(v1);
                transofrm.Rotate(v2);
                v0 = v0 + transofrm.position * camera.zoom;
                v1 = v1 + transofrm.position * camera.zoom;
                v2 = v2 + transofrm.position * camera.zoom;
                //поворот объекта для камеры и затем перемещение относительно камеры
                v0 = v0 - new Vector3(camera.position.x, -camera.position.y, camera.position.z);
                v1 = v1 - new Vector3(camera.position.x, -camera.position.y, camera.position.z);
                v2 = v2 - new Vector3(camera.position.x, -camera.position.y, camera.position.z);
                transofrm.Rotate(v0, camera.angle);
                transofrm.Rotate(v1, camera.angle);
                transofrm.Rotate(v2, camera.angle);

                // преобразуем в координаты экрана

                int x0 = Convert.ToInt32((v0.x + 1.0) * width / 2.0);
                int y0 = Convert.ToInt32((v0.y + 1.0) * height / 2.0);
                int x1 = Convert.ToInt32((v1.x + 1.0) * width / 2.0);
                int y1 = Convert.ToInt32((v1.y + 1.0) * height / 2.0);
                int x2 = Convert.ToInt32((v2.x + 1.0) * width / 2.0);
                int y2 = Convert.ToInt32((v2.y + 1.0) * height / 2.0);
                int z0 = Convert.ToInt32((v0.z + 1) * depth / 2.0);
                int z1 = Convert.ToInt32((v1.z + 1) * depth / 2.0);
                int z2 = Convert.ToInt32((v2.z + 1) * depth / 2.0);


                // не работаем с  треугольником вне экрана
                if ((x1 < 0 || x1 > width) && (x0 < 0 || x0 > width) && (x2 < 0 || x2 > width)) continue;
                if ((y1 < 0 || y1 > height) && (y0 < 0 || y0 > height) && (y2 < 0 || y2 > height)) continue;

                //не работаем с вершинами поверх фото zbuffer
                if ((z1 < 0 || z1 > depth) && (z0 < 0 || z0 > depth) && (z2 < 0 || z2 > depth)) continue;
                if ((x0 + y0 * width) >= zbuffer.Length || (x1 + y1 * width) >= zbuffer.Length || (x2 + y2 * width) >= zbuffer.Length) continue;
                if ((x0 + y0 * width) < 0 || (x1 + y1 * width) < 0 || (x2 + y2 * width) < 0) continue;
                if (z1 < zbuffer[(int)x1 + y1 * width] && z0 < zbuffer[(int)x0 + y0 * width] && z2 < zbuffer[(int)x2 + y2 * width]) continue;


                Vector3 normal = (v2 - v0) ^ (v1 - v0);
                double intensity_light = (normal.normalize() * (light.vector).normalize()).getLength();
                // backface culling где освещение <0.02 !!!  не рисуем + пишем отрисовку используя zbuffer
                // Работаем только с координатами экрана
                if (intensity_light > 0.01)
                {
                    Color color_set = Color.FromArgb(color.A, Convert.ToInt32(color.R * intensity_light), Convert.ToInt32(color.G * intensity_light), Convert.ToInt32(color.B * intensity_light));
                    if(select) color_set = Color.FromArgb(255, Convert.ToInt32(255 * intensity_light),0, 0);
                    triangle(new Vector3(x0, y0, z0), new Vector3(x1, y1, z1), new Vector3(x2, y2, z2), pixel, zbuffer, width, height, color_set);
                }


            }
        }

        //отрисовка полигона
        private void triangle(Vector3 v0, Vector3 v1, Vector3 v2, int[] pixel, int[] zbuffer, int width, int height, Color color_set)
        {
            if (v0.y == v1.y && v0.y == v2.y) return; // не отрисовываем вырожденый треугольник
            if (v0.y > v1.y) (v0, v1) = (v1, v0);
            if (v0.y > v2.y) (v0, v2) = (v2, v0);
            if (v1.y > v2.y) (v1, v2) = (v2, v1);
            int total_height = Convert.ToInt32(v2.y - v0.y); // максимальная высота треугольника по y
            total_height = total_height > height ? height : total_height;
            for (int pix_y = 0; pix_y <= total_height; pix_y++)
            {
                // ВЫБИРАЕМ КАКУЮ ЧАСТЬ ТРЕУГОЛЬНИКА ОТРИСОВАТЬ
                bool second_half = (pix_y > v1.y - v0.y) || Convert.ToInt32(v1.y) == Convert.ToInt32(v0.y);
                int segment_height = second_half ? Convert.ToInt32(v2.y - v1.y) : Convert.ToInt32(v1.y - v0.y);
                double alpha = (double)pix_y / total_height;
                double beta = (double)(pix_y - (second_half ? v1.y - v0.y : 0)) / segment_height;// с проверкой на деление на 0
                Vector3 A = v0 + (v2 - v0) * alpha;
                Vector3 B = second_half ? v1 + (v2 - v1) * beta : v0 + (v1 - v0) * beta;
                if (A.x > B.x) (A, B) = (B, A);
                for (int pix_x = Convert.ToInt32(A.x); pix_x <= Convert.ToInt32(B.x); pix_x++)
                {
                    double phi = Convert.ToInt32(B.x) == Convert.ToInt32(A.x) ? 1 : (double)(pix_x - A.x) / (double)(B.x - A.x);

                    Vector3 P = A + (B - A) * phi;
                    int px = Convert.ToInt32(P.x);
                    int py = Convert.ToInt32(P.y);
                    int pz = Convert.ToInt32(P.z);

                    if (px < 0 || px > width) continue; // не работаем с частью треугольника вне экрана
                    int idx = px + py * width;
                    if (idx>=0 && idx < zbuffer.Length && zbuffer[idx] < pz)
                    {
                        zbuffer[idx] = pz;
                        pixel[idx] = color_set.ToArgb();
                    }
                }
            }
        }

        //отрисовка линии
        private void line(int x0, int y0, int x1, int y1, int[] pixel, int width, int height)
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
                    if (x >= 0 && y >= 0 && x < width && y < height && (y + x * width) < (width * height))
                    {
                        pixel[y + width * x] = color.ToArgb();
                    }
                }
                else
                {
                    if (x >= 0 && y >= 0 && x < width && y < height && (x + y * width) < (width * height))
                    {
                        pixel[x + y * width] = color.ToArgb();
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
