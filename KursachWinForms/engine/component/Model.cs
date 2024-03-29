﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Engine.Math;

namespace Engine.Component
{
    public class Model: IModel
    {
        public string path { get; set; }
        public List<Vector3> points { get; set; }
        public List<Vector3> normals { get; set; }
        public List<int[]> triangle { get; set; } //индексы точек


        public Model(string path) {
            points = new List<Vector3>();
            normals = new List<Vector3>();
            triangle = new List<int[]>();
            this.path = path;
            read_obj();
        }

        public Model()
        {
            points = new List<Vector3>();
            normals = new List<Vector3>();
            triangle = new List<int[]>();
            path = null;
            read_obj();
        }

        public bool read_obj()
        {
            if (path == null || !HaveFile())
            {
                Console.WriteLine("Have not a model on format .obj");
                return false;
            }

            using(StreamReader sr = new StreamReader(path))
            {
                points.Clear();
                normals.Clear();
                triangle.Clear();
                Vector3 vct3_tmp;
                int[] tmp_trian;
                string line = sr.ReadLine();
                try
                {
                    while(line != null)
                    {
                        if (line!=null && line.Length >= 2)
                        {
                            if (line[0] == 'v'){
                                //нормали
                                if (line[1] == 'n')
                                {
                                    
                                    vct3_tmp = new Vector3();
                                    vct3_tmp.x = double.Parse(line.Split(' ')[1], CultureInfo.InvariantCulture);
                                    vct3_tmp.y = double.Parse(line.Split(' ')[2], CultureInfo.InvariantCulture);
                                    vct3_tmp.z = double.Parse(line.Split(' ')[3], CultureInfo.InvariantCulture);
                                    normals.Add(vct3_tmp);
                                }
                                // вершины
                                else if (line[1]==' ')
                                {
                                    
                                    vct3_tmp = new Vector3();
                                    vct3_tmp.x = double.Parse(line.Split(' ')[1], CultureInfo.InvariantCulture);
                                    vct3_tmp.y = -double.Parse(line.Split(' ')[2], CultureInfo.InvariantCulture);
                                    vct3_tmp.z = double.Parse(line.Split(' ')[3], CultureInfo.InvariantCulture);
                                    points.Add(vct3_tmp);
                                }
                            }else if(line[0] == 'f') {
                                
                                //индексы верши формирующие треугольник начинаются индексы с 1
                                string[] line_split = line.Split(' ');
                                if (line_split.Length == 4)
                                {
                                    tmp_trian = new int[3];
                                    tmp_trian[0] = Convert.ToInt32(line_split[1].Split('/')[0]) - 1;
                                    tmp_trian[1] = Convert.ToInt32(line_split[2].Split('/')[0]) - 1;
                                    tmp_trian[2] = Convert.ToInt32(line_split[3].Split('/')[0]) - 1;
                                    triangle.Add(tmp_trian);
                                }
                                else if(line_split.Length == 5)
                                {
                                    tmp_trian = new int[3];
                                    tmp_trian[0] = Convert.ToInt32(line_split[1].Split('/')[0]) - 1;
                                    tmp_trian[1] = Convert.ToInt32(line_split[2].Split('/')[0]) - 1;
                                    tmp_trian[2] = Convert.ToInt32(line_split[3].Split('/')[0]) - 1;
                                    triangle.Add(tmp_trian);
                                    tmp_trian = new int[3];
                                    tmp_trian[0] = Convert.ToInt32(line_split[1].Split('/')[0]) - 1;
                                    tmp_trian[1] = Convert.ToInt32(line_split[4].Split('/')[0]) - 1;
                                    tmp_trian[2] = Convert.ToInt32(line_split[3].Split('/')[0]) - 1;
                                    triangle.Add(tmp_trian);
                                }
                            }
                        }
                        line = sr.ReadLine();
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine("read_obj finish");
                return true;
            }
        }

        private bool HaveFile()
        {
            if (path != null && path.Length > 0)
            {
                FileInfo fileInf = new FileInfo(path);
                if (fileInf.Exists && Path.GetExtension(path) == ".obj") return true;
                else return false;
            }
            else { return false; }
        }

        //изменение положения вершин 
        public void EditVeritx(List<int> id_vertex,double force,Vector3 to)
        {
            foreach (int id in id_vertex)
            {
                points[id] = new Vector3( (points[id] -to).normalize() * force  +  points[id]);
            }
        }
    }
}
