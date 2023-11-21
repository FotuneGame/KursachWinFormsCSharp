using Engine.Math;
using System.Collections.Generic;

namespace Engine.Component
{
    internal interface IModel
    {
        string path { get; set; }
        List<Vector3> points { get; set; }
        List<Vector3> normals { get; set; }
        List<int[]> triangle { get; set; } //индексы точек

        bool read_obj(); //считываение из формата obj 
    }
}
