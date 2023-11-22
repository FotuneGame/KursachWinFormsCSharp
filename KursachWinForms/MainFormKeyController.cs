using Engine.Math;

using KursachWinForms.Controller;

using System.Windows.Forms;

using System;

namespace KursachWinForms
{
    public partial class MainForm : Form
    {
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == (int)KeyValue.Render)
            {
                if (mode_camera != 'f') mode_camera = 'f';
                else
                {
                    label_camera_mode.Text = "Режим(m): Поворот";
                    mode_camera = 'r';
                }
            }
            if((int)e.KeyCode == (int)KeyValue.CameraMode)
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
                if ((int)e.KeyCode == (int)KeyValue.Right)
                    engine.MainCamera.angle = engine.MainCamera.angle + new Vector3(0, step_grid, 0);
                if ((int)e.KeyCode == (int)KeyValue.Left)
                    engine.MainCamera.angle = engine.MainCamera.angle + new Vector3(0, -step_grid, 0);
                if ((int)e.KeyCode == (int)KeyValue.Up)
                    engine.MainCamera.angle = engine.MainCamera.angle + new Vector3(step_grid, 0, 0);
                if ((int)e.KeyCode == (int)KeyValue.Down)
                    engine.MainCamera.angle = engine.MainCamera.angle + new Vector3(-step_grid, 0, 0);
                if ((int)e.KeyCode == (int)KeyValue.Forward)
                    engine.MainCamera.angle = engine.MainCamera.angle + new Vector3(0, 0, step_grid);
                if ((int)e.KeyCode == (int)KeyValue.Back)
                    engine.MainCamera.angle = engine.MainCamera.angle + new Vector3(0, 0, -step_grid);
            }
            else if (mode_camera == 'p')
            {
                if ((int)e.KeyCode == (int)KeyValue.Right)
                    engine.MainCamera.position = engine.MainCamera.position + new Vector3(step_grid, 0, 0);
                if ((int)e.KeyCode == (int)KeyValue.Left)
                    engine.MainCamera.position = engine.MainCamera.position + new Vector3(-step_grid, 0, 0);
                if ((int)e.KeyCode == (int)KeyValue.Up)
                    engine.MainCamera.position = engine.MainCamera.position + new Vector3(0, step_grid, 0);
                if ((int)e.KeyCode == (int)KeyValue.Down)
                    engine.MainCamera.position = engine.MainCamera.position + new Vector3(0, -step_grid, 0);
                if ((int)e.KeyCode == (int)KeyValue.Forward)
                    engine.MainCamera.position = engine.MainCamera.position + new Vector3(0, 0, step_grid);
                if ((int)e.KeyCode == (int)KeyValue.Back)
                    engine.MainCamera.position = engine.MainCamera.position + new Vector3(0, 0, -step_grid);
            }
            else if (mode_camera == 'f')
            {
                engine.ClearSelectEobjects();
                // если окно выбраного объекта удаляем его
                if (this.Controls.ContainsKey("component_menu"))
                    this.Controls.Remove(this.Controls["component_menu"]);

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
                //проекции горячими клавишами
                if ((int)e.KeyCode == (int)KeyValue.Projection_XY)
                    engine.MainCamera.angle = new Vector3(0, 0, 0);
                if ((int)e.KeyCode == (int)KeyValue.Projection_XZ)
                    engine.MainCamera.angle = new Vector3(90, 0, 0);
                if ((int)e.KeyCode == (int)KeyValue.Projection_YZ)
                    engine.MainCamera.angle = new Vector3(0, -90, 0);

                if ((int)e.KeyCode == (int)KeyValue.GridUp) step_grid += 0.1;
                if ((int)e.KeyCode == (int)KeyValue.GridDown && System.Math.Round(step_grid, 1) > 0.1) step_grid -= 0.1;

                if ((int)e.KeyCode == (int)KeyValue.ZoomUp) engine.MainCamera.zoom += step_grid;
                if ((int)e.KeyCode == (int)KeyValue.ZoomDown && System.Math.Round(engine.MainCamera.zoom, 1) > 0.1) engine.MainCamera.zoom -= step_grid;
                label_render.Text = "";
                label_for_render.Text = "Для рендера (v)";
                label_step_grid.Text = "Шаг сетки(+-): " + System.Math.Round(step_grid, 1);
                label_camera_rot.Text = "Угол камеры(ad/ws/qe)(x/y/z):" + engine.MainCamera.angle;
                label_camera_pos.Text = "Позиция камеры(ad/ws/qe)(x/y/z):" + engine.MainCamera.position;
                label_camera_zoom.Text = "Приближение(zx): " + System.Math.Round(engine.MainCamera.zoom, 1);
                engine.gizmo_draw = true;

            }

            // отчистка выбора пользователя
            if((int)e.KeyCode == (int)KeyValue.ClearAll)
            {
                engine.ClearSelectEobjects();
                if (this.Controls.ContainsKey("window_add_obj"))
                    this.Controls.Remove(this.Controls["window_add_obj"]);
                if (this.Controls.ContainsKey("component_menu"))
                    this.Controls.Remove(this.Controls["component_menu"]);
            }
            

            //обновить отрисовку графики 3д
            window_draw.Invalidate();
        }
    }
}
