using Engine;
using Engine.Component;
using Engine.Math;
using Engine.Object;
using Engine.Utilits;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


namespace KursachWinForms
{
    public partial class MainForm : Form
    {

        public PictureBox window_draw;

        private int[] pixels;
        private EngineRender engine;
        private readonly int width, height, depth; // глубина прорисовки
        private double step_grid;//шаг сетки
        private char mode_camera; // 'p' - смена позиции камеры, 'r' -  поворота камеры, f (final) - конечный рендер


        public MainForm()
        {
            InitializeComponent();
            width = depth = height = this.Height;
            Start();
        }

        // создание объектов
        public void Start()
        {
            // камера по умолчанию на режиме поворота
            mode_camera = 'r';
            //шаг сетки по умолчанию
            step_grid = 0.1;

            //инициализация движка в этом окне
            window_draw = new PictureBox();
            window_draw.BackColor = Color.White; // цвет фона
            window_draw.Width = width;  // ширина элемента 
            window_draw.Height = height; // высота элемента 
            window_draw.Paint += new PaintEventHandler(window_Paint);
            window_draw.MouseClick += new MouseEventHandler(window_Click);
            

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.Controls.Add(window_draw); // добавляем PictureBox на форму

            //инициализируем движок набором пикселей выостой и шириной фото
            pixels = new int[window_draw.Width * window_draw.Height];
            engine = new EngineRender(pixels, window_draw.Width, window_draw.Height, depth, step_grid, window_draw.BackColor, true);

            engine.MainCamera.angle.set(0, 0, 0);
            label_step_grid.Invalidate();
            label_camera_rot.Invalidate();
        }




        // обновление отрисовки картинки с 3д сценой
        private void window_Paint(object sender, PaintEventArgs e)
        {
            engine.step_grid = step_grid;
            engine.Update();
            // Отображение массива пикселей на window_draw
            Bitmap bitmap = new Bitmap(width, height);
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
            bitmap.UnlockBits(bitmapData);

            e.Graphics.DrawImageUnscaled(bitmap, new Point(0, 0));
        }

        // проверка позиции мыши на 3д сцене
        private void window_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                if (!this.Controls.ContainsKey("window_add_obj"))
                {
                    GroupBox add_obj_box = new GroupBox();
                    add_obj_box.Name = "window_add_obj";
                    add_obj_box.Location = new Point(e.X, e.Y);
                    int i = 0;
                    string[] models = SpaceFolder.getDefaultModels();
                    for (i=0;i<models.Length; i++)
                    {
                        Button btn = new Button();
                        btn.Name = models[i];
                        btn.Text = models[i];
                        btn.Size = new Size(100, 30);
                        btn.Location = new Point(0, 30 * i);
                        btn.BringToFront();
                        btn.Click += (object sender_btn, EventArgs e_btn) => {
                            Transform obj_trans = new Transform(new Vector3(0, 0, 0), new Vector3(step_grid, step_grid, step_grid), new Vector3(0, 0, 0));
                            Color obj_color = Color.Gray;
                            Model obj_model = new Model("./DefaultModel/"+ btn.Text + ".obj");
                            Renderer obj_rend = new Renderer(obj_trans, obj_color, obj_model);
                            EObject obj = new EObject(obj_trans, obj_rend, btn.Text);
                            //удалить выпадающее окно
                            if (this.Controls.ContainsKey("window_add_obj"))
                                this.Controls.Remove(this.Controls["window_add_obj"]);
                            engine.EObjects.Add(obj);
                            window_draw.Invalidate();// обновить отрисовку
                            this.Focus(); // вернуть фокус ввода на форму
                        };
                        add_obj_box.Controls.Add(btn);
                    }
                    add_obj_box.Size = new Size(100, 31 * (i));
                    this.Controls.Add(add_obj_box);
                    add_obj_box.BringToFront();
                }
                else
                {
                    var add_obj_box = this.Controls["window_add_obj"];
                    add_obj_box.Location = new Point(e.X, e.Y);
                    add_obj_box.BringToFront();
                }
            }

            if (e.Button == MouseButtons.Left)
            {
                engine.ClearSelectEobjects();
                EObject select_obj = engine.Update(e.X,e.Y);
                if (select_obj != null)
                {
                    // если окно было то пересоздаем его
                    if (!this.Controls.ContainsKey("window_add_obj"))
                        this.Controls.Remove(this.Controls["setting_obj"]);
                   
                    GroupBox setting_obj = new GroupBox();
                    setting_obj.Text = select_obj.name;
                    setting_obj.Name = "setting_obj";
                    setting_obj.Location = new Point(width, 0);
                    setting_obj.Size = new Size(this.Width - width, height);
                    setting_obj.BackColor = Color.White;



                    this.Controls.Add(setting_obj);
                    setting_obj.BringToFront();

                }
                else
                {
                    engine.ClearSelectEobjects();
                    if (this.Controls.ContainsKey("setting_obj"))
                        this.Controls.Remove(this.Controls["setting_obj"]);
                }
                window_draw.Invalidate();
            }

            this.Focus(); // вернуть фокус ввода на форму
        }





    }
}
