using System.Drawing;

namespace Engine.Object
{
    internal interface IGrid
    {
        Color color { get; set; }
        void DrawGrid(int[] pixel, Camera camera, double step);
        void AnchorCamera(int[] pixel, Camera camera, double step);
    }
}
