using Engine.Object;
using System.Collections.Generic;

namespace Engine
{
    internal interface IEngineRender
    {
        Camera MainCamera { get; }
        Light MainLight { get; }
        List<EObject> EObjects { get; set; }

        void ClearSelectEobjects();
        EObject Update(int mouse_x = -1, int mouse_y = -1); // обновляет и возвращает выделенный объект (лучше бы разделить)

    }
}
