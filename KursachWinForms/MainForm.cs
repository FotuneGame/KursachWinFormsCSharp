using Engine;
using Engine.Component;
using Engine.Math;
using Engine.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace KursachWinForms
{
    public partial class MainForm : Form
    {

        public PictureBox window_draw;

        private int[] pixels;
        private EngineRender engine;
        private readonly int width, height, depth; // глубина прорисовки
        private const double step_grid=0.1;//шаг сетки


        public MainForm()
        {
            InitializeComponent();

            //инициализация движка в этом окне
            window_draw = new PictureBox();
            window_draw.BackColor = Color.White; // цвет фона
            // ДЕЛАЕМ ВСЕ ПРОСТРАНСТВО РАВНЫМ
            width = depth = this.Height = this.Width = height = this.Height;
            window_draw.Width = this.Width;  // ширина элемента 
            window_draw.Height = this.Height; // высота элемента 
            window_draw.Paint += new PaintEventHandler(window_Paint);
            
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.Controls.Add(window_draw); // добавляем PictureBox на форму

            //инициализируем движок набором пикселей выостой и шириной фото
            pixels = new int[window_draw.Width * window_draw.Height];
            engine = new EngineRender(pixels,window_draw.Width, window_draw.Height, depth, step_grid, window_draw.BackColor);

            Start();
        }

        private void camera_TextChanged(object sender, EventArgs e)
        {

        }

        private void window_Paint(object sender, PaintEventArgs e)
        {
            engine.Update();
            // Отображение массива пикселей на window_draw
            Bitmap bitmap = new Bitmap(width, height);
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
            bitmap.UnlockBits(bitmapData);

            e.Graphics.DrawImageUnscaled(bitmap, new Point(0, 0));
        }

        // создание объектов
        public void Start()
        {
            engine.MainCamera.angle.set(0, 0, 0);
            /*
            EObject Cube = new EObject("default");
            Cube.transform.position = new Vector3(0.1, -0.1, 0);
            Cube.transform.size = new Vector3(0.1, 0.1, 0.1);
            Cube.transform.angle = new Vector3(45, 45, 45);
            engine.EObjects.Add(Cube);
            */

            Transform obj_trans = new Transform(new Vector3(0,0,0), new Vector3(0.1, 0.1, 0.1), new Vector3(180, 180, 180));
            Color obj_color = Color.Red;
            Model obj_model = new Model("./DefaultModel/capy.obj");
            Renderer obj_rend = new Renderer(obj_trans, obj_color, obj_model);
            EObject obj = new EObject(obj_trans, obj_rend, "obj");
            engine.EObjects.Add(obj);

            label2.Invalidate();
        }
    }
}
