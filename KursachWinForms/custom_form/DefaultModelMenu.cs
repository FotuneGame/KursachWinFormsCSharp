using Engine;
using Engine.Component;
using Engine.Math;
using Engine.Object;
using Engine.Utilits;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace KursachWinForms.CustomForm
{
    internal class DefaultModelMenu: GroupBox
    {
        public DefaultModelMenu(EngineRender engine, PictureBox window_draw,Point mouse_pos,double step_grid)
        {
            this.Name = "window_add_obj";
            this.Location = mouse_pos;
            int i = 0;
            string[] models = SpaceFolder.getDefaultModels();
            for (i = 0; i < models.Length; i++)
            {
                Button btn = new Button();
                btn.Name = models[i];
                btn.Text = models[i];
                btn.Size = new Size(100, 30);
                btn.Location = new Point(0, 30 * i);
                btn.BringToFront();
                btn.Click += (object sender_btn, EventArgs e_btn) => {
                    Transform obj_trans = new Transform(engine.MainCamera.position, new Vector3(step_grid, step_grid, step_grid), new Vector3(0, 0, 0));
                    Color obj_color = Color.Gray;
                    Model obj_model = new Model("./DefaultModel/" + btn.Text + ".obj");
                    Renderer obj_rend = new Renderer(obj_trans, obj_color, obj_model);
                    EObject obj = new EObject(obj_trans, obj_rend, btn.Text);
                    //удалить выпадающее окно
                    if (this.FindForm().Controls.ContainsKey("window_add_obj"))
                        this.FindForm().Controls.Remove(this.FindForm().Controls["window_add_obj"]);
                    engine.EObjects.Add(obj);
                    window_draw.Invalidate();// обновить отрисовку
                    this.Focus(); // вернуть фокус ввода на форму
                };
                this.Controls.Add(btn);
            }
            this.Size = new Size(100, 31 * (i));
        }
    }
}
