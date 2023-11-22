using Engine.Object;
using Engine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace KursachWinForms.CustomForm
{
    internal class PaintMenu: GroupBox
    {
        private double force = 0.1;
        private int radius = 20;
        private bool mode = false;

        private PictureBox paint_image;

        private Form main_form;
        private PictureBox window_draw;
        private EObject select_obj;
        EngineRender engine;
        public PaintMenu(Form main_form, EngineRender engine, PictureBox window_draw, EObject select_obj, Point point, Size size)
        {
            this.main_form = main_form;
            this.engine = engine;
            this.window_draw = window_draw;
            this.select_obj = select_obj;

            this.Name = "paint_menu";
            this.Location = point;
            this.Size = size;
            this.BackColor = Color.White;


            Button btn_mode = new Button();
            btn_mode.Text = "Режим просмотра";
            btn_mode.Size = new Size(150, 30);
            btn_mode.Location = new Point(this.Size.Width / 3 - btn_mode.Width / 3, 0);
            btn_mode.Click += (object sender, EventArgs e) =>
            {
                this.CheckMode();
                if(mode)
                    btn_mode.Text = "Режим просмотра";
                else
                    btn_mode.Text = "Режим деформации";
                main_form.ActiveControl = null;
                mode = !mode;
            };
            this.Controls.Add(btn_mode);


            Label tittle_radius = new Label();
            tittle_radius.Text = "Радиус: ("+radius+"px)";
            tittle_radius.Size = new Size(100, 30);
            tittle_radius.Location = new Point(10, 40);
            this.Controls.Add(tittle_radius);

            Button btn_radius_plus = new Button();
            btn_radius_plus.Text = "+";
            btn_radius_plus.Size = new Size(30, 20);
            btn_radius_plus.Location = new Point( 110, 40);
            btn_radius_plus.Click += (object sender, EventArgs e) =>
            {
                if (radius < 1000) radius++;
                tittle_radius.Text = "Радиус: (" + radius + "px)";
                if(paint_image!=null) paint_image.Size = new Size(radius, radius);
                main_form.ActiveControl = null;
            };
            this.Controls.Add(btn_radius_plus);

            Button btn_radius_minus = new Button();
            btn_radius_minus.Text = "-";
            btn_radius_minus.Size = new Size(30, 20);
            btn_radius_minus.Location = new Point(140, 40);
            btn_radius_minus.Click += (object sender, EventArgs e) =>
            {
                if (radius > 10) radius--;
                tittle_radius.Text = "Радиус: (" + radius + "px)";
                if (paint_image != null) paint_image.Size = new Size(radius, radius);
                main_form.ActiveControl = null;
            };
            this.Controls.Add(btn_radius_minus);


            Label tittle_force = new Label();
            tittle_force.Text = "Сила: (" + System.Math.Round(force, 1) + "px)";
            tittle_force.Size = new Size(100, 30);
            tittle_force.Location = new Point(10, 70);
            this.Controls.Add(tittle_force);

            Button tittle_force_plus = new Button();
            tittle_force_plus.Text = "+";
            tittle_force_plus.Size = new Size(30, 20);
            tittle_force_plus.Location = new Point(110, 70);
            tittle_force_plus.Click += (object sender, EventArgs e) =>
            {
                if (force < 100) force+=0.1;
                tittle_force.Text = "Сила: (" + System.Math.Round(force, 1) + "px)";
                main_form.ActiveControl = null;
            };
            this.Controls.Add(tittle_force_plus);

            Button tittle_force_minus = new Button();
            tittle_force_minus.Text = "-";
            tittle_force_minus.Size = new Size(30, 20);
            tittle_force_minus.Location = new Point(140, 70);
            tittle_force_minus.Click += (object sender, EventArgs e) =>
            {
                if (force > -100) force -= 0.1;
                tittle_force.Text = "Сила: (" + System.Math.Round(force, 1) + "px)";
                main_form.ActiveControl = null;
            };
            this.Controls.Add(tittle_force_minus);


        }

        public bool CheckMode()
        {
            if (paint_image == null)
            {
                if (window_draw.Controls.ContainsKey("cursor_paint"))
                    window_draw.Controls.Remove(window_draw.Controls["cursor_paint"]);
                paint_image = new PictureBox();
                paint_image.Name = "cursor_paint";
                paint_image.BackColor = Color.FromArgb(128, 128, 128, 128);

                window_draw.MouseMove += MouseMoveSelectedObj;
                paint_image.MouseClick += MouseClickForSelectVertix;
            }
            paint_image.Size = new Size(radius, radius);
            paint_image.BringToFront();
            
            return mode;
        }

        private void MouseMoveSelectedObj(object sender_paint, MouseEventArgs e_paint)
        {
            if (select_obj.renderer.select == false || !engine.EObjects.Contains(select_obj))
            {
                window_draw.Controls.Remove(window_draw.Controls["cursor_paint"]);
                paint_image = null;
                window_draw.MouseMove -= MouseMoveSelectedObj;
                return;
            }
            if (!window_draw.Controls.ContainsKey("cursor_paint") && mode)
                window_draw.Controls.Add(paint_image);
            else if (paint_image != null)
            {
                if (paint_image.Visible == false && mode)
                    paint_image.Visible = true;
                else if (paint_image.Visible == true && !mode)
                    paint_image.Visible = false;
                paint_image.Location = new Point(e_paint.X - paint_image.Size.Width / 2, e_paint.Y - paint_image.Size.Height / 2);
            }   
        }


        private void MouseClickForSelectVertix(object sender_paint, MouseEventArgs e_paint)
        {
            engine.EditSelectModel(select_obj, radius, force, this.paint_image.Location.X, this.paint_image.Location.Y);
            window_draw.Invalidate();
        }
    }
}
