using Engine.Object;
using Engine.Math;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace KursachWinForms.custom_form
{
    internal class ComponentMenu: GroupBox
    {
        private PictureBox window_draw;
        private EObject select_obj;


        public ComponentMenu(Form main_form, PictureBox window_draw, EObject select_obj,Point point, Size size)
        {
            this.window_draw = window_draw;
            this.select_obj = select_obj;

            this.Text = select_obj.name;
            this.Name = "component_menu";
            this.Location = point;
            this.Size = size;
            this.BackColor = Color.White;

            Label trans = new Label();
            trans.Text = "Трансформация";
            trans.Location = new Point(this.Size.Width/2-trans.Width/2, 30);
            this.Controls.Add(trans);


            Label pos = new Label();
            pos.Text = "Позиция (x/y/z):";
            pos.Location = new Point(0, 60);
            this.Controls.Add(pos);

            DoubleTextBox pos_x = new DoubleTextBox(main_form, this, select_obj.transform.position.x,"pos_x");
            pos_x.Size = new Size(50, 30);
            pos_x.Location = new Point(0 + this.Width/3 - pos_x.Width, 85);
            DoubleTextBox pos_y = new DoubleTextBox(main_form, this, select_obj.transform.position.y, "pos_y");
            pos_y.Size = new Size(50, 30);
            pos_y.Location = new Point(50 + this.Width / 3 - pos_y.Width, 85);
            DoubleTextBox pos_z = new DoubleTextBox(main_form, this, select_obj.transform.position.z, "pos_z");
            pos_z.Size = new Size(50, 30);
            pos_z.Location = new Point(100 + this.Width / 3 - pos_z.Width, 85);

            this.Controls.Add(pos_x);
            this.Controls.Add(pos_y);
            this.Controls.Add(pos_z);

            Label rot = new Label();
            rot.Text = "Поворот (x/y/z):";
            rot.Location = new Point(0, 110);
            this.Controls.Add(rot);

            DoubleTextBox rot_x = new DoubleTextBox(main_form, this, select_obj.transform.angle.x, "rot_x");
            rot_x.Size = new Size(50, 30);
            rot_x.Location = new Point(0 + this.Width / 3 - rot_x.Width, 135);
            DoubleTextBox rot_y = new DoubleTextBox(main_form, this, select_obj.transform.angle.y, "rot_y");
            rot_y.Size = new Size(50, 30);
            rot_y.Location = new Point(50 + this.Width / 3 - rot_y.Width, 135);
            DoubleTextBox rot_z = new DoubleTextBox(main_form, this, select_obj.transform.angle.z, "rot_z");
            rot_z.Size = new Size(50, 30);
            rot_z.Location = new Point(100 + this.Width / 3 - rot_z.Width, 135);

            this.Controls.Add(rot_x);
            this.Controls.Add(rot_y);
            this.Controls.Add(rot_z);

            Label scale = new Label();
            scale.Text = "Размеры (x/y/z):";
            scale.Location = new Point(0, 160);
            this.Controls.Add(scale);

            DoubleTextBox scale_x = new DoubleTextBox(main_form, this, select_obj.transform.size.x, "scale_x");
            scale_x.Size = new Size(50, 30);
            scale_x.Location = new Point(0 + this.Width / 3 - scale_x.Width, 185);
            DoubleTextBox scale_y = new DoubleTextBox(main_form, this, select_obj.transform.size.y, "scale_y");
            scale_y.Size = new Size(50, 30);
            scale_y.Location = new Point(50 + this.Width / 3 - scale_y.Width, 185);
            DoubleTextBox scale_z = new DoubleTextBox(main_form, this, select_obj.transform.size.z, "scale_z");
            scale_z.Size = new Size(50, 30);
            scale_z.Location = new Point(100 + this.Width / 3 - scale_z.Width, 185);

            this.Controls.Add(scale_x);
            this.Controls.Add(scale_y);
            this.Controls.Add(scale_z);


            Label render = new Label();
            render.Text = "Рендер объекта";
            render.Location = new Point(this.Size.Width / 2 - render.Width / 2, 230);
            this.Controls.Add(render);

            Button btn_color = new Button();
            btn_color.Size = new Size(150, 30);
            btn_color.Location = new Point(this.Size.Width / 3 - btn_color.Width / 3, 260);
            btn_color.Text = "Поменять цвет";
            btn_color.Click += (object sender, EventArgs e) =>
            {
                ColorDialog color_obj = new ColorDialog();
                color_obj.Color = select_obj.renderer.color;

                if (color_obj.ShowDialog() == DialogResult.OK)
                {
                    select_obj.renderer.color = color_obj.Color;
                    window_draw.Invalidate();
                }

            };
            this.Controls.Add(btn_color);

            Label path_l = new Label();
            path_l.Text = "Путь до модели:";
            path_l.Location = new Point(0, 300);
            this.Controls.Add(path_l);


            TextBox path = new TextBox();
            path.Size = new Size(this.Size.Width-path_l.Size.Width-20, 30);
            path.Location = new Point(path_l.Width, 300);
            path.KeyPress += (object sender, KeyPressEventArgs e) =>
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    main_form.ActiveControl = null;
                    select_obj.renderer.model.path = (sender as TextBox).Text;
                    if (!select_obj.renderer.model.read_obj())
                        path_l.Text = "Путь неверный:";
                    else
                        path_l.Text = "Путь до модели:";
                    path_l.Invalidate();
                    window_draw.Invalidate();
                    e.Handled = true;   
                }
            };
            this.Controls.Add(path);

            Label name = new Label();
            name.Size=new Size(120, 30);
            name.Text = "Новое имя объекта:";
            name.Location = new Point(this.Size.Width / 2 - name.Width / 2, 330);
            this.Controls.Add(name);

            TextBox name_box = new TextBox();
            name_box.Size = new Size(150, 30);
            name_box.Location = new Point(this.Size.Width / 3 - name_box.Width / 3, 360);
            name_box.KeyPress += (object sender, KeyPressEventArgs e) =>
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    main_form.ActiveControl = null;
                    select_obj.name = (sender as TextBox).Text;
                    this.Text = (sender as TextBox).Text;
                    this.Invalidate();
                    window_draw.Invalidate();
                    e.Handled = true;
                }
            };
            this.Controls.Add(name_box);

        }

        public void UpdateEObject(string key,double value)
        {
            switch (key)
            {
                case "pos_x":select_obj.transform.position.x = value; break;
                case "pos_y": select_obj.transform.position.y = value; break;
                case "pos_z": select_obj.transform.position.z = value; break;

                case "rot_x": select_obj.transform.angle = new Vector3(value, select_obj.transform.angle.y, select_obj.transform.angle.z); break;
                case "rot_y": select_obj.transform.angle = new Vector3(select_obj.transform.angle.x, value, select_obj.transform.angle.z); break;
                case "rot_z": select_obj.transform.angle = new Vector3(select_obj.transform.angle.x, select_obj.transform.angle.y, value); break;

                case "scale_x": select_obj.transform.size.x = value; break;
                case "scale_y": select_obj.transform.size.y = value; break;
                case "scale_z": select_obj.transform.size.z = value; break;

            }
            window_draw.Invalidate();
        }

    }
}
