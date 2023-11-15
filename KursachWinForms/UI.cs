using Engine.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KursachWinForms
{
    public partial class MainForm : Form
    {
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.A)
                engine.MainCamera.angle = engine.MainCamera.angle + new Vector3(0, 1, 0);
            if (e.KeyCode == Keys.D)
                engine.MainCamera.angle = engine.MainCamera.angle + new Vector3(0, -1, 0);
            if (e.KeyCode == Keys.W)
                engine.MainCamera.angle = engine.MainCamera.angle + new Vector3(1, 0, 0);
            if (e.KeyCode == Keys.S)
                engine.MainCamera.angle = engine.MainCamera.angle + new Vector3(-1, 0, 0);
            if (e.KeyCode == Keys.Q)
                engine.MainCamera.angle = engine.MainCamera.angle + new Vector3(0, 0, 1);
            if (e.KeyCode == Keys.E)
                engine.MainCamera.angle = engine.MainCamera.angle + new Vector3(0, 0, -1);

            window_draw.Invalidate();
            label2.Text = "Угол камеры:" + engine.MainCamera.angle;
        }
    }
}
