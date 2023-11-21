using Engine.Object;
using System.Drawing;

namespace Engine.Component
{
    internal interface IRenderer
    {
        Color color { get; set; }
        Model model { get; set; }
        bool select { get; set; }

        void DrawLine(Camera camera, int[] pixel, int width, int height);
        void Render(Camera camera, Light light, int[] pixel, int width, int height, int depth, int[] zbuffer);

    }
}
