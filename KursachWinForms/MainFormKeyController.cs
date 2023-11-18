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
            if(e.KeyCode == Keys.V) {
                if(mode_camera != 'f') mode_camera = 'f';
                else mode_camera = 'r';
            }
            if(e.KeyCode == Keys.M)
            {
                if (mode_camera != 'r')
                {
                    label_camera_mode.Text = "Режим(m): Поворот";
                    mode_camera = 'r';
                }
                else if (mode_camera != 'p')
                {
                    label_camera_mode.Text = "Режим(m): Движение";
                    mode_camera = 'p';
                }
            }
            if (mode_camera == 'r')
            {
                if (e.KeyCode == Keys.D)
                    engine.MainCamera.angle = engine.MainCamera.angle + new Vector3(0, 1, 0);
                if (e.KeyCode == Keys.A)
                    engine.MainCamera.angle = engine.MainCamera.angle + new Vector3(0, -1, 0);
                if (e.KeyCode == Keys.W)
                    engine.MainCamera.angle = engine.MainCamera.angle + new Vector3(1, 0, 0);
                if (e.KeyCode == Keys.S)
                    engine.MainCamera.angle = engine.MainCamera.angle + new Vector3(-1, 0, 0);
                if (e.KeyCode == Keys.Q)
                    engine.MainCamera.angle = engine.MainCamera.angle + new Vector3(0, 0, 1);
                if (e.KeyCode == Keys.E)
                    engine.MainCamera.angle = engine.MainCamera.angle + new Vector3(0, 0, -1);
            }
            else if (mode_camera == 'p')
            {
                if (e.KeyCode == Keys.D)
                    engine.MainCamera.position = engine.MainCamera.position + new Vector3(step_grid, 0, 0);
                if (e.KeyCode == Keys.A)
                    engine.MainCamera.position = engine.MainCamera.position + new Vector3(-step_grid, 0, 0);
                if (e.KeyCode == Keys.W)
                    engine.MainCamera.position = engine.MainCamera.position + new Vector3(0, step_grid, 0);
                if (e.KeyCode == Keys.S)
                    engine.MainCamera.position = engine.MainCamera.position + new Vector3(0, -step_grid, 0);
                if (e.KeyCode == Keys.Q)
                    engine.MainCamera.position = engine.MainCamera.position + new Vector3(0, 0, step_grid);
                if (e.KeyCode == Keys.E)
                    engine.MainCamera.position = engine.MainCamera.position + new Vector3(0, 0, -step_grid);
            }
            else if (mode_camera == 'f')
            {
                engine.ClearSelectEobjects();
                label_render.Text = "Рендер(v/m)";
                label_for_render.Text = "";
                label_camera_mode.Text = "";
                label_step_grid.Text = "";
                label_camera_rot.Text = "";
                label_camera_pos.Text = "";
                label_camera_zoom.Text = "";
                engine.gizmo_draw = false;
            }
            
            if(mode_camera != 'f')
            {
                if (e.KeyCode == Keys.Oemplus) step_grid += 0.1;
                if (e.KeyCode == Keys.OemMinus && System.Math.Round(step_grid, 1) > 0.1) step_grid -= 0.1;

                if (e.KeyCode == Keys.Z) engine.MainCamera.zoom += 0.1;
                if (e.KeyCode == Keys.X && System.Math.Round(engine.MainCamera.zoom, 1) > 0.1) engine.MainCamera.zoom -= 0.1;
                label_render.Text = "";
                label_for_render.Text = "Для рендера (v)";
                label_step_grid.Text = "Шаг сетки(+-): " + System.Math.Round(step_grid, 1);
                label_camera_rot.Text = "Угол камеры(ad/ws/qe)(x/y/z):" + engine.MainCamera.angle;
                label_camera_pos.Text = "Позиция камеры(ad/ws/qe)(x/y/z):" + engine.MainCamera.position;
                label_camera_zoom.Text = "Приближение(zx): " + System.Math.Round(engine.MainCamera.zoom, 1);
                engine.gizmo_draw = true;
            }

            // отчистка выбора пользователя
            if(e.KeyCode == Keys.Escape)
            {
                if (this.Controls.ContainsKey("window_add_obj"))
                    this.Controls.Remove(this.Controls["window_add_obj"]);
            }
            

            //обновить отрисовку графики 3д
            window_draw.Invalidate();
        }
    }
}
